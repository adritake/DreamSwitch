using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TV : ChecklistEvent
{
    public float WatchTime;

    public override void CompleteEvent()
    {
        base.CompleteEvent();
        _player.CanMove = false;
        _player.LookAt(transform.position);
        Invoke(nameof(ReleasePlayer), WatchTime);
    }

    private void ReleasePlayer()
    {
        _player.CanMove = true;
    }
}
