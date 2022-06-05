using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WC : ChecklistEvent
{
    public float PeeTime = 5;
    public Transform LookPosition;

    public override bool CompleteEvent()
    {
        if (!base.CompleteEvent())
        {
            return false;
        }
        _player.CanMove = false;
        _player.LookAt(LookPosition.position);
        _player.GetComponent<PlayerVFXManager>().PlayPeeVFX();
        FMODUnity.RuntimeManager.PlayOneShotAttached("event:/Sfx/Loop1/Pee", gameObject);
        Invoke(nameof(ReleasePlayer), PeeTime);
        return true;
    }

    private void ReleasePlayer()
    {
        _player.CanMove = true;
        _player.GetComponent<PlayerVFXManager>().StopPeeVFX();
    }
}
