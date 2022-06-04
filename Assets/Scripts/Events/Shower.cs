using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shower : ChecklistEvent
{
    public float ShowerTime;
    public Transform LookAt;
    public ParticleSystem ShowerVFX;

    public override void CompleteEvent()
    {
        _player.CanMove = false;
        ShowerVFX.Play();
        _player.LookAt(LookAt.position);
        Invoke(nameof(EndEvent), ShowerTime);
    }

    private void EndEvent()
    {
        _player.CanMove = true;
        ShowerVFX.Stop();
        LevelChecklistManager.Instance.CompleteEvent(EventName);
    }
}
