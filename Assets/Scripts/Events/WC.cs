using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WC : ChecklistEvent
{
    public float PeeTime = 5;
    public Transform LookPosition;

    public override void CompleteEvent()
    {
        base.CompleteEvent();
        _player.CanMove = false;
        _player.LookAt(LookPosition.position);
        _player.GetComponent<PlayerVFXManager>().PlayPeeVFX();
        Invoke(nameof(ReleasePlayer), PeeTime);
    }

    private void ReleasePlayer()
    {
        _player.CanMove = true;
        _player.GetComponent<PlayerVFXManager>().StopPeeVFX();
    }
}
