using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPeeVFX :MonoBehaviour
{
    public ParticleSystem PeeVFX;

    public void PlayVFX()
    {
        PeeVFX.Play();
    }

    public void StopVFX()
    {
        PeeVFX.Stop();
    }
}
