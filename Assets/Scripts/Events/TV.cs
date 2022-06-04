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
        Invoke(nameof(EndEvent), WatchTime);
    }

    private void EndEvent()
    {
        _player.CanMove = true;
        LevelChecklistManager.Instance.CompleteEvent(EventName);
    }
}
