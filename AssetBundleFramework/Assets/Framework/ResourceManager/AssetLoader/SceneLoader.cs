using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneLoader : AssetLoaderBase
{
    private bool isAddMode;
    protected override void OnLoadTaskFinish(Object asset, bool result)
    {
        if (result == true)
        {
            LoadSceneParameters parameters = new LoadSceneParameters();
            if (isAddMode)
                parameters.loadSceneMode = LoadSceneMode.Additive;
            else
                parameters.loadSceneMode = LoadSceneMode.Single;

#if UNITY_EDITOR
            if (GameSetting.Instance == null || !GameSetting.Instance.AssetbundleMode)//不用AB，直接加载
            {
                UnityEditor.SceneManagement.EditorSceneManager.LoadSceneAsyncInPlayMode("Assets/Works/Res/" + _resEditorPath, parameters);
                return;
            }
#endif  
            //AB模式
            //string sceneName = System.IO.Path.GetFileName(_resEditorPath);
            string sceneName = System.IO.Path.GetFileNameWithoutExtension(_resEditorPath);
            SceneManager.LoadSceneAsync(sceneName, parameters);
        }

    }

    public void SetSceneLoadMode(bool mode)
    {
        isAddMode = mode;
    }
}
