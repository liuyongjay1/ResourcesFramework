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
     * ʹ�÷�ʽ:
     * "Works/Res/Prefabs/UIPanel/Canvas.prefab"
     * Ҫ�����ļ���չ��
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
        if (GameSetting.Instance.LoadBundleAsync)//ģ���첽��һ֡����
            _loadState = TaskLoadState.LoadingAsset;
        else
        {
            //ͬ�����أ�ֱ��֪ͨ���
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
            //EditorLoadTask ��鳡���Ƿ����
            Scene scene = UnityEditor.SceneManagement.EditorSceneManager.GetSceneByPath("Assets/" + _resEditorPath);
            if (scene == null) {
                _loadState = TaskLoadState.LoadAssetFailed;
                //֪ͨResourceLoader���سɹ���ʧ��
                _callback?.Invoke(null, false);
            }
            else
            {
                _loadState = TaskLoadState.LoadAssetSuccess;
                //֪ͨResourceLoader���سɹ���ʧ��
                _callback?.Invoke(null, true);
            }
        }
        else
        {
            _loadState = _target == null ? TaskLoadState.LoadAssetFailed : TaskLoadState.LoadAssetSuccess;
            if (_loadState == TaskLoadState.LoadAssetFailed)
                LogManager.LogError($"Failed to load asset object : path:{_resEditorPath}, type:{_targetType.ToString()}");
            //֪ͨLoader���ؽ��
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
