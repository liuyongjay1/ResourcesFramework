using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;
public class UIBase
{
    //Lua脚本
    private LuaTable tableIns;
    //挂在UI上的控件
    private LuaTable uiElement;
    private delegate void LuaFunc(LuaTable t);
    public delegate void OnPanelCreateFunc(LuaTable t, LuaTable uiEle);
    OnPanelCreateFunc OnPanelCreate;
    LuaFunc OnPanelShow;
    LuaFunc OnPanelClose;
    //调用Lua的OnPanelDestroy方法
    LuaFunc OnPanelDestroy;
    public void Open(LuaTable paramTable)
    {
        IsOpen = true;    
        //确保反复打开只加载一次
        if (LoadState == UILoadState.None)
        {
            LoadState = UILoadState.Loading;
            LoadLuaFile(paramTable);
            LoadUIPrefab();
        }
        else if (LoadState == UILoadState.Loaded)
        {
            if (paramTable != null)
                tableIns.Set("paramTab", paramTable);

            if (UIGameObject == null)
                LogManager.LogError("UIGameObject is null,uiName: " + UIName);
            PrepareShowUI();
        }
    }

    //Lua与Obj都存在，通知Lua创建
    void PrepareShowUI()
    {
        //该UI可能已被通知隐藏，要检查IsOpen
        if (OnPanelShow != null)
        {
            OnPanelShow(tableIns);
            LuaTable tab = LuaManager.Instance.LuaEnv.NewTable();
            tab.Set("UIName", UIName);
            LuaManager.Instance.CSSendEventToLua("UIEvent_OnShow", tab);

            SetCanvasShowHide();
        }
        else
            LogManager.LogError("OnPanelShow is null,uiName: " + UIName);
    }

    public void Close()
    {
        IsOpen = false;

        if (DestroyOnClose)
        {
            if (OnPanelDestroy != null)
                OnPanelDestroy(tableIns);
            //卸载资源
            ReleaseUI();
            ResetUIBase();
        }
        else
        {
            if (OnPanelClose != null)
            {
                LuaTable tab = LuaManager.Instance.LuaEnv.NewTable();
                tab.Set("UIName", UIName);
                LuaManager.Instance.CSSendEventToLua("UIEvent_OnClose", tab);
                OnPanelClose(tableIns);
            }
            
            if (LoadState == UILoadState.Loaded)
            {
                SetCanvasShowHide();
            }
        }
    }

    /// <summary>
    /// 隐藏UI相关操作
    /// 请注意：UI加载后该UI可能已是关闭状态
    /// </summary>
    void SetCanvasShowHide()
    {
        if (UICanvas.Length > 0)
            UICanvas[0].enabled = IsOpen;
        else
            LogManager.LogError("UICanvas .length == 0,uiName: " + UIName);
    }

    void ResetUIBase()
    {
        UICanvas = null;
        UIGameObject = null;
        LoadState = UILoadState.None;
        AllBtns = null;
        RectTrans = null;
        tableIns = null;
        uiElement = null;
        OnPanelCreate = null;
        OnPanelShow = null;
        OnPanelClose = null;
        OnPanelDestroy = null;
        HideOther = false;
        ResPath = "";
        SortOrder = 0;
        UIName = "";
    }

    private void LoadLuaFile(LuaTable paramTable)
    {
        LuaTable tab = LuaManager.Instance.GetLuaTable(ResPath);
        if (tableIns == null)
        {
            //调用Class里的new方法，创建一个新的table
            System.Func<LuaTable> NewTableFunc = tab.Get<System.Func<LuaTable>>("new");
            tableIns = NewTableFunc();
        }
        if (tableIns == null)
        {
            LogManager.LogError("tableIns is null,ResPath:" + ResPath);
            return;
        }
        OnPanelCreate = tableIns.Get<OnPanelCreateFunc>("OnPanelCreate");
        OnPanelShow = tableIns.Get<LuaFunc>("OnPanelShow");
        OnPanelClose = tableIns.Get<LuaFunc>("OnPanelClose");
        OnPanelDestroy = tableIns.Get<LuaFunc>("OnPanelDestroy");
        tableIns.Set("UIBase", this);
        if (paramTable != null)
            tableIns.Set("paramTab", paramTable);
    }
    private void LoadUIPrefab()
    {
        string panelPath = "Prefabs/" + ResPath + ".prefab";
        GameObjectLoader loader = new GameObjectLoader();
        loader.LoadAsync(panelPath, LoadUIPrefabCallBack, typeof(GameObject), false);
        //loader要存下来，卸载时使用0
        //ObjAsset = LoadAssetUtility.LoadGameObject(panelPath, LoadUIPrefabCallBack);
    }

    private void LoadUIPrefabCallBack(AssetLoaderBase loader, bool state)
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
        UIGameObject = ObjLoader.GetGameObject();
        //标记加载成功
        LoadState = UILoadState.Loaded;
        UIElement ele = UIGameObject.GetComponent<UIElement>();
        AllBtns = UIGameObject.GetComponentsInChildren<UIButton>(true);
        for (int i = 0; i < AllBtns.Length; i++)
        {
            AllBtns[i].Init(this);
        }
        if (ele == null)
            LogManager.LogError("Load UIPrefab UIElement Component is Null,please check resPath: " + ResPath);
        //存储预设序列化组件
        uiElement = LuaManager.Instance.LuaEnv.NewTable();
        ele.ApplyElementToLua(uiElement);
        //获取该UI所有Canvas组件
        UICanvas = UIGameObject.GetComponentsInChildren<Canvas>(true);
        if (UICanvas.Length == 0)
            LogManager.LogError("UICanvas .length == 0,uiName: " + UIName);
        //请注意，OnPanelCreate必须调用一次，和当前显隐状态无关
        if (OnPanelCreate != null)
        {
            LuaTable tab = LuaManager.Instance.LuaEnv.NewTable();
            tab.Set("UIName", UIName);
            LuaManager.Instance.CSSendEventToLua("UIEvent_OnCreate", tab);
            OnPanelCreate(tableIns, uiElement);
        }
        //请注意：UI加载后该UI可能已是关闭状态
        SetCanvasShowHide();
        //UI显示排序
        RectTrans = UIGameObject.GetComponent<RectTransform>();
        //显示UI前必须完成UI排序和特效排序
        ResetCanvasSortingOrder();
        //准备工作都已做完，可以显示UI
        if (IsOpen)
            PrepareShowUI();

        UIManager.Instance.InsertUI(this);
    }

    /// <summary>
    /// 设置UI的SortingOrder，childCanvas[0]是该UI的RootCanvas
    /// </summary>
    public void ResetCanvasSortingOrder()
    {
        //预设Canvas
        int depth = SortOrder;
        for (int i = 0; i < UICanvas.Length; i++)
        {
            UICanvas[i].overrideSorting = true;
            UICanvas[i].sortingOrder = depth;
            depth += 5;
        }
    }

    public void ReleaseUI()
    {
        if (ObjAsset != null)
            ObjAsset.Release();
        ObjAsset = null;
    }
    /// <summary>
    /// 锁定所有按钮，只能点这一个
    /// </summary>
    /// <param name="isFocusUI">锁定目标是本UI</param>
    /// <param name="btnId">按钮ID</param>
    public void OnFocusButton(bool isFocusUI, int btnId)
    {
        if (!isFocusUI)
        {
            for (int i = 0; i < AllBtns.Length; i++)
            {
                AllBtns[i].OnLockBtn(false);
            }
        }
        else
        {
            for (int i = 0; i < AllBtns.Length; i++)
            {
                AllBtns[i].OnLockBtn(AllBtns[i].buttonID == btnId);
            }
        }
    }

    public void OnSetBtnActive(int btnId, bool isActive)
    {
        for (int i = 0; i < AllBtns.Length; i++)
        {
            if (AllBtns[i].buttonID == btnId)

                AllBtns[i].OnSetBtnActive(isActive);
        }
    }


    public bool OnGetBtnActiveState(int btnId)
    {
        for (int i = 0; i < AllBtns.Length; i++)
        {
            if (AllBtns[i].buttonID == btnId)

                return AllBtns[i].GetBtnActiveState();
        }
        return false;
    }

    /// <summary>
    /// 取消所有按钮锁定，恢复到锁定前状态
    /// </summary>
    public void OnCancleFocusButton()
    {
        for (int i = 0; i < AllBtns.Length; i++)
        {
            AllBtns[i].OnUnlockBtn();
        }
    }
    /// <summary>
    /// 预设的Loader
    /// </summary>
    private GameObjectLoader ObjAsset;

    ///
    ///预设是否加载成功
    ///
    private UILoadState LoadState;

    /// <summary>
    /// 显示状态,OpenUI为true,CloseUI为False
    /// </summary>
    public bool IsOpen;

    public RectTransform RectTrans;
    /// <summary>
    /// 显示状态,OpenUI为true,CloseUI为False
    /// </summary>
    public Canvas[] UICanvas
    {
        get; private set;
    }
    public UIButton[] AllBtns
    {
        get; private set;
    }
    /// <summary>
    /// 隐藏时是否回收
    /// </summary>
    public bool DestroyOnClose
    {
        get; set;
    }
    /// <summary>
    /// 隐藏时是否回收
    /// </summary>
    public bool HideOther
    {
        get; set;
    }
    /// <summary>
    /// 预设和lua脚本路径
    /// </summary>
    public string ResPath
    {
        get; set;
    }
    /// <summary>
    /// SoringOrder排序
    /// </summary>
    public int SortOrder
    {
        get; set;
    }
    public int LayerIndex
    {
        get; set;
    }
    public string UIName
    {
        get; set;
    }
    /// <summary>
    /// UI预设
    /// </summary>
    public GameObject UIGameObject
    {
        get; private set;
    }

    private enum UILoadState
    {
        None = 0,
        Loading = 1,
        Loaded = 2,
    }
}
