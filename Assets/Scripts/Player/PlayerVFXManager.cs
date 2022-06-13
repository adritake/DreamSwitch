using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVFXManager : MonoBehaviour
{
    public PlayerPeeVFX PeeVFX;

    public void PlayPeeVFX()
    {
        PeeVFX.PlayVFX();

    }
    public void StopPeeVFX()
    {
        PeeVFX.StopVFX();
    }
}
