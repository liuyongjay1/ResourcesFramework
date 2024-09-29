using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
#if UNITY_EDITOR
using System.Linq;
#endif
using UnityEngine;
using XLua;

public class LoadTaskManager:Singleton<LoadTaskManager>
{
#if UNITY_EDITOR
    public Dictionary<string, EditorLoadTask> _allEditorLoadTask = new Dictionary<string, EditorLoadTask>();
#endif
    public ConcurrentDictionary<string, ABLoadTask> _allABLoadTask = new ConcurrentDictionary<string, ABLoadTask>();
    /// <summary>
    /// 初始化接口
    /// </summary>
    /// <param name="root"></param>
	public void Init()
    {
    }

    public void CreateLoadTask(AssetLoaderBase loader, Action<UnityEngine.Object, bool> callback,Type targetType, bool autoRelease = false)
    {
        //资源的编辑器路径
        string ResEditorPath = loader.GetResEditorPath();
        //只有编辑器才要考虑是否用AB
#if UNITY_EDITOR
        if (!GameSetting.Instance.AssetbundleMode)//不用AB，直接加载
        {
            EditorLoadTask task = null;
            if (_allEditorLoadTask.ContainsKey(ResEditorPath))
            {
                task = _allEditorLoadTask[ResEditorPath];
            }
            else
            {
                 task = new EditorLoadTask();
                _allEditorLoadTask.Add(ResEditorPath, task);
            }
            task.StartTask(ResEditorPath, callback, targetType);

            return;
        }
#endif
        {
            string bundlePath = GameResTool.GetBundlePathByEditorPath(ResEditorPath);
            ABLoadTask task = GetABLoadTask(bundlePath, out bool bundleExist);
            task.StartTask(ResEditorPath, callback, targetType);
        }
    }



    //资源路径是bundle路径，不是编辑器资源路径
    public ABLoadTask GetABLoadTask(string bundlePath, out bool bundleExist)
    {
        ABLoadTask task;
        if (_allABLoadTask.TryGetValue(bundlePath, out task))
            bundleExist = true;
        else
        {
            //Debug.Log("GetABLoadTask,不存在，创建 path: " + bundlePath);
            task = new ABLoadTask(bundlePath);
            _allABLoadTask.TryAdd(bundlePath, task);
            bundleExist = false;
        }
        return task;
    }

    public void Tick(float deltaTime)
    {
#if UNITY_EDITOR
        for (int i = 0; i < _allEditorLoadTask.Count; i++)
        {
            var item = _allEditorLoadTask.ElementAt(i);
            item.Value.Tick();
        }
#endif
        foreach (var task in _allABLoadTask)
        {
            task.Value.Tick();
        }
    }

    public void ReleaseLoader(LoadTaskBase targetTask)
    {
#if UNITY_EDITOR
        if (targetTask is EditorLoadTask)
        {
            _allEditorLoadTask.Remove(targetTask.GetResEditorPath());
        }
#endif
        if (targetTask is ABLoadTask)
        {
            ABLoadTask task = (ABLoadTask)targetTask;
            task.ReleaseTask();

            _allABLoadTask.TryRemove(task.GetABPath(), out ABLoadTask removeAB);
        }
    }

    //获取一级AB包列表，
    public List<ABLoadTask> GetABDependsList()
    {
        List<ABLoadTask> FirstLevel = new List<ABLoadTask>();
        foreach (var task in _allABLoadTask)
        {
            if (task.Value.GetResEditorPath() != "")
                FirstLevel.Add(task.Value);
        }
        return FirstLevel;
    }

    [LuaCallCSharp]
    public void ReleaseAllTask()
    {
        AssetBundle.UnloadAllAssetBundles(false);
#if UNITY_EDITOR
        foreach (var task in _allEditorLoadTask)
        {
            task.Value.ReleaseTask();
        }
#endif
        ConcurrentDictionary<string, ABLoadTask> temp = new ConcurrentDictionary<string, ABLoadTask>();
        foreach (var task in _allABLoadTask)
        {
            // AssetBundle.UnloadAllAssetBundles并不能释放正在异步加载的任务，所以正在加载的任务不清空
            if (task.Value.GetTaskLoadState() == TaskLoadState.LoadingBundle)
            {
                temp.TryAdd(task.Key,task.Value);
            }
        }
        _allABLoadTask.Clear();
        _allABLoadTask = temp;
    }
}
