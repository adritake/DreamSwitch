using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shower : ChecklistEvent
{
    public float ShowerTime;
    public ParticleSystem ShowerVFX;

    public override void CompleteEvent()
    {
        _player.CanMove = false;
        ShowerVFX.Play();
        Invoke(nameof(EndEvent), ShowerTime);
    }

    private void EndEvent()
    {
        _player.CanMove = true;
        ShowerVFX.Stop();
    }
}
