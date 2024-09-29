using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using XLua;
//一个UI对应一个UIBase
//UIManager自己维护UI物体的加载释放，和ResourceManager不耦合
public class UIManager : Singleton<UIManager>
{
    //已显示UI存档
    private Dictionary<string, UIBase> UIList = new Dictionary<string, UIBase>();
    private Camera UICamera;
    private Transform Layer0Root;
    private Transform Layer1Root;
    private Transform Layer2Root;
    private Transform Layer3Root;
    private Transform Layer4Root;
    private Canvas CanvasRoot;
    private RectTransform _RectTransform;
    private GameObject CanvasObj;
    private LuaTable LuaUIManager;
    private int Layer0SortOrder = 0;
    private int Layer1SortOrder = 2000;
    private int Layer2SortOrder = 4000;
    private int Layer3SortOrder = 6000;
    private int Layer4SortOrder = 8000;
    private int sortOrderOffset = 200;
    private UIBase ui_Loading;
    private delegate void LuaFunc();

    [LuaCallCSharp]
    public UIBase ShowUI(string name, LuaTable paramTable = null)
    {
        //Debug.LogError("OpenUI: " + name);
        UIBase ui = null;
        //有缓存
        if (UIList.TryGetValue(name, out ui))
        {
            if (ui.HideOther)
            {
                HideOther(ui);
            }
            ui.Open(paramTable);
            return null;
        }
        //无缓存
        CSTable_UIConfig uiConfig = TableManager.Instance.GetTable<CSTable_UIConfig>();
        foreach (Row_UIConfig row in uiConfig.GetAllRowData<Row_UIConfig>())
        {
            if (row.Key == name)
            {
                ui = new UIBase();
                ui.UIName = name;
                ui.ResPath = row.ResPath;
                ui.LayerIndex = row.Layer;
                ui.DestroyOnClose = row.DestroyOnClose == 1;
                if (ui.LayerIndex == 0)
                    ui.SortOrder = Layer0SortOrder += sortOrderOffset;
                else if(ui.LayerIndex == 1)
                    ui.SortOrder = Layer1SortOrder += sortOrderOffset;
                else if (ui.LayerIndex == 2)
                    ui.SortOrder = Layer2SortOrder += sortOrderOffset;
                else if (ui.LayerIndex == 3)
                    ui.SortOrder = Layer3SortOrder += sortOrderOffset;
                else if (ui.LayerIndex == 4)
                    ui.SortOrder = Layer4SortOrder += sortOrderOffset;
            }
        }
        if (ui == null)
        {
            LogManager.LogError("UI Not Exist in uidefine,name: " + name);
            return null;
        }
        if (ui.HideOther)
        {
            HideOther(ui);
        }

        ui.Open(paramTable);
        LogManager.LogUIInfo(string.Format("UIManager.ShowUI() uiName:-->{0}",name));
        UIList.Add(ui.UIName, ui);
        return ui;
    }
    
    [LuaCallCSharp]
    public void CloseUI(string uiName)
    {
        UIBase ui = null;
        if (!UIList.TryGetValue(uiName, out ui))
        {
            LogManager.LogError("CloseUI Not Exist,name: " + uiName);
            return;
        }
        ui.Close();
        LogManager.LogUIInfo(string.Format("UIManager.CloseUI() uiName:-->{0}", uiName));
        if (ui.DestroyOnClose)
        {
            ResetLayerIndex(ui);
            UIList.Remove(uiName);
            ui = null;
        }
    }

    [LuaCallCSharp]
    public bool GetUIShowState(string uiName)
    {
        UIBase ui = null;
        if (!UIList.TryGetValue(uiName, out ui))
        {
            return false;
        }
        return ui.IsOpen == true;
    }
    /// <summary>
    /// 加载UICanvasRoot，在Lua UIManager中调用
    /// </summary>
    /// <param name="instance"></param>
    [LuaCallCSharp]
    public void LoadCanvasRoot(LuaTable instance)
    {
        LuaUIManager = instance;
        string panelPath = "Prefabs/UIPanel/Canvas.prefab";
        //LoadAssetUtility.LoadGameObject(panelPath, LoadCanvasCallBack);

        GameObjectLoader loader = new GameObjectLoader();
        loader.LoadAsync(panelPath, LoadCanvasCallBack, typeof(GameObject), true);
    }

    private void LoadCanvasCallBack(AssetLoaderBase loader, bool state)
    {
        GameObjectLoader ObjLoader = (GameObjectLoader)loader;
        if (ObjLoader == null)
        {
            LogManager.LogError("ObjLoader is null,loadpath: " + loader.GetResEditorPath());
            return;
        }
        if (state == false)
        {
            LogManager.LogError("ObjLoader Load state is false resPath: " + loader.GetResEditorPath());
            return;
        }
        CanvasObj = ObjLoader.GetGameObject();
        if (CanvasObj == null)
        {
            LogManager.LogError("LoadCanvasCallBack is null");
            return;
        }
        CanvasObj.SetActive(true);
        //Root不卸载
        GameObject.DontDestroyOnLoad(CanvasObj);
        CanvasRoot = CanvasObj.GetComponent<Canvas>();
        _RectTransform = CanvasObj.GetComponent<RectTransform>();
        Layer0Root = CanvasObj.transform.Find("UIRoot/Layer0");
        Layer1Root = CanvasObj.transform.Find("UIRoot/Layer1");
        Layer2Root = CanvasObj.transform.Find("UIRoot/Layer2");
        Layer3Root = CanvasObj.transform.Find("UIRoot/Layer3");
        Layer4Root = CanvasObj.transform.Find("UIRoot/Layer4");
        UICamera = CanvasObj.transform.Find("UICamera").GetComponent<Camera>();

        LuaFunc OnRootLoaded = LuaUIManager.Get<LuaFunc>("OnRootLoaded");
        OnRootLoaded();
    }

    /// <summary>
    /// UI加载后要插入Canvas,UI分级也在这里插入
    /// </summary>
    /// <param name="showUI"></param>
    public void InsertUI(UIBase ui)
    {
        if(ui.LayerIndex == 0)
            ui.UIGameObject.transform.SetParent(Layer0Root);
        else if (ui.LayerIndex == 1)
            ui.UIGameObject.transform.SetParent(Layer1Root);
        else if (ui.LayerIndex == 2)
            ui.UIGameObject.transform.SetParent(Layer2Root);
        else if (ui.LayerIndex == 3)
            ui.UIGameObject.transform.SetParent(Layer3Root);
        else if (ui.LayerIndex == 4)
            ui.UIGameObject.transform.SetParent(Layer4Root);

        ui.RectTrans.localScale = Vector3.one;
        ui.RectTrans.anchorMin = Vector2.zero;
        ui.RectTrans.anchorMax = Vector2.one;
        ui.RectTrans.offsetMin = new Vector2(0, 0);
        ui.RectTrans.offsetMax = new Vector2(0, 0);
    }

    public void ResetLayerIndex(UIBase desUI)
    {
        foreach (UIBase ui in UIList.Values)
        {
            if (ui.LayerIndex == desUI.LayerIndex && ui.SortOrder > desUI.SortOrder)
            {
                ui.SortOrder -= sortOrderOffset;

                if (ui.LayerIndex == 0)
                    Layer0SortOrder -= sortOrderOffset;
                else if (ui.LayerIndex == 1)
                    Layer1SortOrder -= sortOrderOffset;
                else if (ui.LayerIndex == 2)
                    Layer2SortOrder -= sortOrderOffset;
                else if (ui.LayerIndex == 3)
                    Layer3SortOrder -= sortOrderOffset;
                else if (ui.LayerIndex == 4)
                    Layer4SortOrder -= sortOrderOffset;
            }
        }
    }
    /// <summary>
    /// 某些UI配表需要隐藏其他UI
    /// </summary>
    /// <param name="showUI"></param>
    public void HideOther(UIBase showUI)
    {
        foreach (UIBase ui in UIList.Values)
        {
            if (showUI != ui)
                ui.Close();
        }
    }

    public Camera GetUICamera()
    {
        if (UICamera)
            return UICamera;
        return null;
    }
        
    //获取根画布
    public RectTransform GetCnvasTransform()
    {
        if (_RectTransform == null)
        {
            LogManager.LogError("_RectTransform is null");
            return null;
        }
        return _RectTransform;
    }
        
    //获取根画布尺寸
    public Vector2 GetCanvasSize()
    {
        if (_RectTransform == null)
        {
            LogManager.LogError("_RectTransform is null");
            return Vector2.zero;
        }
        //return _RectTransform.rect.size
        return _RectTransform.sizeDelta;
    }

    /// <summary>
    /// 锁定所有按钮，只能点击这一个，用于新手引导等功能
    /// </summary>
    /// <param name="uiName"></param>
    /// <param name="btnID"></param>
    [LuaCallCSharp]
    public void FocusButton(string uiName, int btnID)
    {
        foreach (var ui in UIList)
        {
            ui.Value.OnFocusButton(ui.Key == uiName, btnID);
        }
    }

    /// <summary>
    /// 取消锁定所有按钮，恢复到锁定前状态
    /// </summary>
    [LuaCallCSharp]
    public void CancleFocusButton()
    {
        foreach (var ui in UIList)
        {
            ui.Value.OnCancleFocusButton();
        }
    }

    /// <summary>
    /// 设置按钮可点击状态
    /// </summary>
    /// <param name="uiName"></param>
    /// <param name="btnID"></param>
    /// <param name="isActive"></param>
    [LuaCallCSharp]
    public void SetBtnActive(string uiName, int btnID, bool isActive)
    {
        UIBase ui = null;
        if (UIList.TryGetValue(uiName, out ui))
        {
            ui.OnSetBtnActive(btnID, isActive);
        }
    }

    /// <summary>
    /// 获取按钮可点击状态
    /// </summary>
    /// <param name="uiName"></param>
    /// <param name="btnID"></param>
    /// <param name="isActive"></param>
    [LuaCallCSharp]
    public bool GetBtnActiveState(string uiName, int btnID)
    {
        UIBase ui = null;
        if (UIList.TryGetValue(uiName, out ui))
        {
            return ui.OnGetBtnActiveState(btnID);
        }
        return false;
    }

    /// <summary>
    /// 释放所有UI预设，但不包括LoadingUI，一般重置游戏时使用
    /// </summary>
    public void ReleaseAllUI()
    {
        LogManager.LogProcedure("===ReleaseAllUI===");
        foreach (var ui in UIList)
        {
            if(ui.Value != ui_Loading)
                ui.Value.ReleaseUI();
        }
        UIList.Clear();
        UIList.Add(ui_Loading.UIName, ui_Loading);
    }

    [LuaCallCSharp]
    public void CloseAllUI()
    {
        foreach (var ui in UIList)
        {
            ui.Value.Close();
        }
    }

}
