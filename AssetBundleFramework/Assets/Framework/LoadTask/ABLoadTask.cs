using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ABLoadTask:LoadTaskBase
{
    public ABLoadTask(string bundlePath)
    {
        _bundleLoadPath = bundlePath;
    }

    //��AB��������AB���б�
    private List<ABLoadTask> _depends = new List<ABLoadTask>(10);
    //AB��·��
    private string _bundleLoadPath;
	//AB����������
    private AssetBundleCreateRequest _bundleRequest;
    //AB����Դ��������
    private AssetBundleRequest _assetRequest;
    //AB��
    private AssetBundle _bundle;
	//���ô���
	private int RefCount;
    //��Դ��
    protected string _assetName;

    public override void DoTask()
    {
        //_assetName����չ��������.prefab .jpg .mp3 .txt
        _assetName = System.IO.Path.GetFileName(_resEditorPath);
        StartLoadBundle(_bundleLoadPath);
    }

    public void StartLoadBundle(string localBundlePath)
    {
        string fullPath = AssetBundleManager.Instance.GetBundleLoadPath(localBundlePath);
        if (GameSetting.Instance.LoadBundleAsync)//�첽����Bundle
        {
            LogManager.LogProcedure("StartLoadBundle Async:" + fullPath);
            _bundleRequest = AssetBundle.LoadFromFileAsync(fullPath);
            _bundleRequest.completed += LoadBundleComplete;
            _loadState = TaskLoadState.LoadingBundle;
        }
        else//ͬ������Bundle
        {
            LogManager.LogProcedure("StartLoadBundle:" + fullPath);
            _bundle = AssetBundle.LoadFromFile(fullPath);
            _loadState = TaskLoadState.LoadBundleSuccess;

            LoadBundleAsset();
        }
        SetDependencies();
    }

    //�첽����Bundle�ص�
    private void LoadBundleComplete(AsyncOperation op)
    {
        if (op.isDone)
        {
            //ע�⣬���������AssetBundle.UnloadAllAssetBundles(false);�ͷ������Ѽ���AB�����ǲ����ж��첽���ص�
            if (_loadState == TaskLoadState.UnLoadBundle)//��ע��
            {
                _bundleRequest.assetBundle.Unload(false);
                return;
            }
            //LogManager.LogInfo("LoadBundleComplete,path:" + _bundleLoadPath);
            _bundle = _bundleRequest.assetBundle;
            //string[] assetNames = _bundle.GetAllAssetNames();
            //for (int i = 0; i < assetNames.Length; i++)
            //{
            //    Debug.LogError("AssetName:" + assetNames[i]);
            //}
            _loadState = TaskLoadState.LoadBundleSuccess;
        }
    }

    //��������Loader
    public void SetDependencies()
    {
        string[] allDepends = AssetBundleManager.Instance.GetBundleDepends(_bundleLoadPath);
        for (int i = 0; i < allDepends.Length; i++)
        {
            string dependBundlePath = allDepends[i];
            //LogManager.LogProcedure($"_bundleLoadPath:{_bundleLoadPath} ��������: {allDepends[i]}");
            //��ȡAB����
            ABLoadTask task = LoadTaskManager.Instance.GetABLoadTask(dependBundlePath, out bool bundleExist);
            //��������+1
            task.Reference();
            //�ϼ���¼������
            _depends.Add(task);
            //������������ַ
            task._bundleLoadPath = dependBundlePath;
            //�����������ڣ���������
            if (bundleExist == false)
                task.StartLoadBundle(dependBundlePath);
        }
    }

	//�ݹ����������Ƿ�ȫ�����
	public bool IsDone()
	{
        if (_depends.Count == 0)//������֤������ײ��Bundle
            return _loadState == TaskLoadState.LoadBundleSuccess;
        else
        {
            //��鱾��������������ֻҪ��һ��δ��ɣ���ֱ�ӷ���δ���
            for (int i = 0; i < _depends.Count; i++)
            {
                if (_depends[i].IsDone() == false)
                {
                    return false;
                }
                return true;
            }
        }
        return false;
	}

    /// <summary>
    /// ���ã����ü����ݼӣ�
    /// </summary>
    public void Reference()
    {
        RefCount++;
    }

    /// <summary>
    /// �ͷţ����ü����ݼ���
    /// </summary>
    public void DeReference()
    {
        RefCount--;
    }

    public int GetRefCount()
    {
        return RefCount;
    }

    //ж�ش˰�,�ݹ�ɾ������
    public override void ReleaseTask()
    {
        _loadState = TaskLoadState.UnLoadBundle;
        _bundleRequest.assetBundle.Unload(false);
    }

    //ж�ش˰�,�ݹ�ɾ������
    public override void UnLoadBundle()
    {
        _loadState = TaskLoadState.UnLoadBundle;
        for (int i = 0; i < _depends.Count; i++)
        {
            _depends[i].DeReference();
            _depends[i].UnLoadBundle();
        }
        //��ע�⣬��������RefCount������һֱΪ0
        if (RefCount <= 0)
        {
            ReleaseTask();
        }
    }

    public void LoadBundleAsset()
    {
        //_assetName���ձ�ʾ���������ص�AB������������
        if (_loadState == TaskLoadState.LoadBundleSuccess && !string.IsNullOrEmpty(_assetName))
        {
            if (IsDone() == true)//�ݹ��������Ƿ����
            {
                if (_targetType == typeof(Scene))
                {
                    _loadState = TaskLoadState.LoadAssetSuccess;
                    _callback?.Invoke(null, true);
                }
                else
                {
                    _loadState = TaskLoadState.LoadingAsset;
                    if (GameSetting.Instance.LoadAssetAsync)
                    {
                        if (_targetType == null)
                            _assetRequest = _bundle.LoadAssetAsync(_assetName);
                        else
                            _assetRequest = _bundle.LoadAssetAsync(_assetName, _targetType);
                        _assetRequest.completed += LoadAssetCallback;
                    }
                    else
                    {
                        if (_targetType == null)
                            _target = _bundle.LoadAsset(_assetName);
                        else
                            _target = _bundle.LoadAsset(_assetName, _targetType);
                        NotifyResult();
                    }
                }
            }
        }
    }
    public override void Tick()
	{
        if(_loadState == TaskLoadState.LoadBundleSuccess)
            LoadBundleAsset();
    }

    /// <summary>
    /// �첽����Asset��Դ�ص�
    /// </summary>
    /// <param name="op"></param>
    private void LoadAssetCallback(AsyncOperation op)
    {
        if (op.isDone)
        {
            //LogManager.LogProcedure("LoadAssetCallback,assetName:" + _assetName);
            _target = _assetRequest.asset;
            NotifyResult();
        }
    }

    //����Asset��֪ͨ���ؽ��
    private void NotifyResult()
    {
        _loadState = _target == null ? TaskLoadState.LoadAssetFailed:TaskLoadState.LoadAssetSuccess;
        // ֪ͨLoader���ؽ��
        _callback?.Invoke(_target, _target != null);

        if (_autoRelease)
        {
            LoadTaskManager.Instance.ReleaseLoader(this);
        }
    }

    public List<ABLoadTask> GetABDepends()
    {
        return _depends;
    }

    public string GetABPath()
    {
        return _bundleLoadPath;
    }
}
