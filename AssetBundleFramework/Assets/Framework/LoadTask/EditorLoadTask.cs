#if UNITY_EDITOR

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EditorLoadTask:LoadTaskBase
{
    /*
     * 使用方式:
     * "Works/Res/Prefabs/UIPanel/Canvas.prefab"
     * 要带着文件扩展名
     */
    public override void DoTask()
    {
        string ResEditorPath = string.Format(GameResTool.EditorPathFormat,_resEditorPath);

        if (_targetType != typeof(Scene))
        {
            _target = AssetDatabase.LoadAssetAtPath(ResEditorPath, _targetType);
            if (_target == null)
            {
                LogManager.LogError("Editor Load Task _target is null,EditorPath: " + ResEditorPath);
                return;
            }
        }
        if (GameSetting.Instance.LoadBundleAsync)//模拟异步下一帧加载
            _loadState = TaskLoadState.LoadingAsset;
        else
        {
            //同步加载，直接通知结果
            _loadState = _target == null ? TaskLoadState.LoadAssetFailed : TaskLoadState.LoadAssetSuccess;
            _callback?.Invoke(_target, _target != null);
        }
    }

  
    public override void Tick()
    {
        if (_loadState != TaskLoadState.LoadingAsset)
            return;
        if (_targetType == typeof(Scene))
        {
            //EditorLoadTask 检查场景是否存在
            Scene scene = UnityEditor.SceneManagement.EditorSceneManager.GetSceneByPath("Assets/" + _resEditorPath);
            if (scene == null) {
                _loadState = TaskLoadState.LoadAssetFailed;
                //通知ResourceLoader加载成功或失败
                _callback?.Invoke(null, false);
            }
            else
            {
                _loadState = TaskLoadState.LoadAssetSuccess;
                //通知ResourceLoader加载成功或失败
                _callback?.Invoke(null, true);
            }
        }
        else
        {
            _loadState = _target == null ? TaskLoadState.LoadAssetFailed : TaskLoadState.LoadAssetSuccess;
            if (_loadState == TaskLoadState.LoadAssetFailed)
                LogManager.LogError($"Failed to load asset object : path:{_resEditorPath}, type:{_targetType.ToString()}");
            //通知Loader加载结果
            _callback?.Invoke(_target, _target != null);
        }
        if (_autoRelease)
        {
            LoadTaskManager.Instance.ReleaseLoader(this);
        }
    }

    public override void ReleaseTask()
    {
        _loadState = TaskLoadState.None;
    }

    public override void UnLoadBundle()
    {
    }
}
#endif
