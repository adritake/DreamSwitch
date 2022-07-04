using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMManager : Singleton<BGMManager>
{
    
    FMODUnity.StudioEventEmitter bgm;

    void Start()
    {
        bgm = GetComponent<FMODUnity.StudioEventEmitter>();
    }

    public void Play()
    {
        bgm.Play();
    }

    public void Stop()
    {
        bgm.Stop();
    }
}
