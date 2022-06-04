using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shower : ChecklistEvent
{
    public float ShowerTime;
    public ParticleSystem ShowerVFX;

    private void Start()
    {
        _player = FindObjectOfType<PlayerController>();
    }

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
