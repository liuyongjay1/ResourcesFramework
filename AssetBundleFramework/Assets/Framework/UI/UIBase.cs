using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;
public class UIBase
{
    //Lua�ű�
    private LuaTable tableIns;
    //����UI�ϵĿؼ�
    private LuaTable uiElement;
    private delegate void LuaFunc(LuaTable t);
    public delegate void OnPanelCreateFunc(LuaTable t, LuaTable uiEle);
    OnPanelCreateFunc OnPanelCreate;
    LuaFunc OnPanelShow;
    LuaFunc OnPanelClose;
    //����Lua��OnPanelDestroy����
    LuaFunc OnPanelDestroy;
    public void Open(LuaTable paramTable)
    {
        IsOpen = true;    
        //ȷ��������ֻ����һ��
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

    //Lua��Obj�����ڣ�֪ͨLua����
    void PrepareShowUI()
    {
        //��UI�����ѱ�֪ͨ���أ�Ҫ���IsOpen
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
            //ж����Դ
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
    /// ����UI��ز���
    /// ��ע�⣺UI���غ��UI�������ǹر�״̬
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
            //����Class���new����������һ���µ�table
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
        //loaderҪ��������ж��ʱʹ��0
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
        //��Ǽ��سɹ�
        LoadState = UILoadState.Loaded;
        UIElement ele = UIGameObject.GetComponent<UIElement>();
        AllBtns = UIGameObject.GetComponentsInChildren<UIButton>(true);
        for (int i = 0; i < AllBtns.Length; i++)
        {
            AllBtns[i].Init(this);
        }
        if (ele == null)
            LogManager.LogError("Load UIPrefab UIElement Component is Null,please check resPath: " + ResPath);
        //�洢Ԥ�����л����
        uiElement = LuaManager.Instance.LuaEnv.NewTable();
        ele.ApplyElementToLua(uiElement);
        //��ȡ��UI����Canvas���
        UICanvas = UIGameObject.GetComponentsInChildren<Canvas>(true);
        if (UICanvas.Length == 0)
            LogManager.LogError("UICanvas .length == 0,uiName: " + UIName);
        //��ע�⣬OnPanelCreate�������һ�Σ��͵�ǰ����״̬�޹�
        if (OnPanelCreate != null)
        {
            LuaTable tab = LuaManager.Instance.LuaEnv.NewTable();
            tab.Set("UIName", UIName);
            LuaManager.Instance.CSSendEventToLua("UIEvent_OnCreate", tab);
            OnPanelCreate(tableIns, uiElement);
        }
        //��ע�⣺UI���غ��UI�������ǹر�״̬
        SetCanvasShowHide();
        //UI��ʾ����
        RectTrans = UIGameObject.GetComponent<RectTransform>();
        //��ʾUIǰ�������UI�������Ч����
        ResetCanvasSortingOrder();
        //׼�������������꣬������ʾUI
        if (IsOpen)
            PrepareShowUI();

        UIManager.Instance.InsertUI(this);
    }

    /// <summary>
    /// ����UI��SortingOrder��childCanvas[0]�Ǹ�UI��RootCanvas
    /// </summary>
    public void ResetCanvasSortingOrder()
    {
        //Ԥ��Canvas
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
    /// �������а�ť��ֻ�ܵ���һ��
    /// </summary>
    /// <param name="isFocusUI">����Ŀ���Ǳ�UI</param>
    /// <param name="btnId">��ťID</param>
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
    /// ȡ�����а�ť�������ָ�������ǰ״̬
    /// </summary>
    public void OnCancleFocusButton()
    {
        for (int i = 0; i < AllBtns.Length; i++)
        {
            AllBtns[i].OnUnlockBtn();
        }
    }
    /// <summary>
    /// Ԥ���Loader
    /// </summary>
    private GameObjectLoader ObjAsset;

    ///
    ///Ԥ���Ƿ���سɹ�
    ///
    private UILoadState LoadState;

    /// <summary>
    /// ��ʾ״̬,OpenUIΪtrue,CloseUIΪFalse
    /// </summary>
    public bool IsOpen;

    public RectTransform RectTrans;
    /// <summary>
    /// ��ʾ״̬,OpenUIΪtrue,CloseUIΪFalse
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
    /// ����ʱ�Ƿ����
    /// </summary>
    public bool DestroyOnClose
    {
        get; set;
    }
    /// <summary>
    /// ����ʱ�Ƿ����
    /// </summary>
    public bool HideOther
    {
        get; set;
    }
    /// <summary>
    /// Ԥ���lua�ű�·��
    /// </summary>
    public string ResPath
    {
        get; set;
    }
    /// <summary>
    /// SoringOrder����
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
    /// UIԤ��
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
