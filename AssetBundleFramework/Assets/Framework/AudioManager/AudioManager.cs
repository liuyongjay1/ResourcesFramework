using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager: Singleton<AudioManager>
{
    private Vector3 rootPosition;
    private AudioSource _AudioSource;
    private GameObject _RootObj;
    public void Init(GameObject root)
    {
        root.AddComponent<AudioListener>();
        _AudioSource = root.AddComponent<AudioSource>();
        rootPosition = root.transform.position;
    }
    public void PlayAudio(string audioPath)
    {
        LoadAssetUtility.LoadAudioAsset(audioPath, LoadAudioCallback);
    }
    void LoadAudioCallback(AssetLoaderBase loader, bool state)
    {
        if (state == true)
        {
            AudioLoader audio = (AudioLoader)loader;
            _AudioSource.clip = audio.GetAudioClip();
            _AudioSource.Play();

        }
    }
    public void StopAudio()
    {
        _AudioSource.Stop();
    }

    public void PlayAudioAtPoint(string audioPath,float playTime)
    {
        LoadAssetUtility.LoadAudioAsset(audioPath, LoadAudioAtPointCallback);
    }

   
    void LoadAudioAtPointCallback(AssetLoaderBase loader, bool state)
    {
        if (state == true)
        {
            AudioLoader audio = (AudioLoader)loader;
            AudioSource.PlayClipAtPoint(audio.GetAudioClip(), rootPosition);
        }
    }
}
