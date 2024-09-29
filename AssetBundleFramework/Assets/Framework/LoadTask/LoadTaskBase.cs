using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TaskLoadState
{
    None = 0,
    LoadingBundle = 1,
    LoadBundleSuccess = 2,
    LoadingAsset = 3,//LoadingAsset״̬ //ABģʽ����AB��������ȫ���������,Editorģʽ��һ֡����
    LoadAssetSuccess = 4,
    LoadAssetFailed = 5,
    UnLoadBundle = 6,
}

/*
 * ͬһ����ַֻ���ܴ���һ��Task
 */
public abstract class LoadTaskBase
{
    //�༭����Դ·��
    protected string _resEditorPath;
    //����״̬
    protected TaskLoadState _loadState;
    //AssetLoaderע��ص�
    protected Action<UnityEngine.Object, bool> _callback;
    //Ŀ����Դ����
    protected Type _targetType;
    //���غ���Դ
    protected UnityEngine.Object _target;
    //���غ��Ƿ��Զ�ж��
    protected bool _autoRelease;

    public void StartTask(string loadResPath,Action<UnityEngine.Object, bool> callback,Type targetType, bool autoRelease = false)
    {
        //�������������
        if (_loadState == TaskLoadState.LoadAssetSuccess || _loadState == TaskLoadState.LoadAssetFailed)
        {
            callback(_target, _loadState == TaskLoadState.LoadAssetSuccess);
            return;
        }
        //���������Ѵ��ڵ�δ��ɣ����AssetLoader�ص�
        if (_loadState != TaskLoadState.None)
        {
            _callback += callback;
            return;
        }
        _callback += callback;
        _resEditorPath = loadResPath;
        _targetType = targetType;
        _loadState = TaskLoadState.LoadingBundle;
        _autoRelease = autoRelease;
        DoTask();
    }

    /// <summary>
    /// ��������ȥ�������
    /// </summary>
    public abstract void DoTask();
    public abstract void Tick();
    public abstract void ReleaseTask();
    public abstract void UnLoadBundle();

    //��ȡ��Դ�༭��·��
    public string GetResEditorPath()
    {
        return _resEditorPath;
    }

    public TaskLoadState GetTaskLoadState()
    {
        return _loadState;
    }
}
