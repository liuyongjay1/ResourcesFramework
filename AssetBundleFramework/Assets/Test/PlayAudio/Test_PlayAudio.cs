using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_PlayAudio : MonoBehaviour
{
    public AudioSource audioComponent;
    // Start is called before the first frame update
    void Start()
    {
        AudioLoader loader = LoadAssetUtility.LoadAudioAsset("Works/Res/Audio/HeartBeat.mp3", LoadAudioCallback);
    }

    void LoadAudioCallback(AssetLoaderBase loader, bool state)
    {
        if (state == true)
        {
            AudioLoader audio = (AudioLoader)loader;
            AudioSource.PlayClipAtPoint(audio.GetAudioClip(), Vector3.zero);
            AudioSource.PlayClipAtPoint(audio.GetAudioClip(), Vector3.one * 100);
            AudioSource.PlayClipAtPoint(audio.GetAudioClip(), Vector3.one * -100);

        }
    }
    // Update is called once per frame
    void Update()
    {
        LoadTaskManager.Instance.Tick(Time.deltaTime);
    }
}
