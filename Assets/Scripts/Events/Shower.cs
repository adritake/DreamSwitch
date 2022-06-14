using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shower : ChecklistEvent
{
    public float ShowerTime;
    public Transform LookAt;
    public Transform ShoweringPosition;
    public float LookTime;
    public ParticleSystem ShowerVFX;

    public override bool OnInteractBegin()
    {
        if (!base.OnInteractBegin())
        {
            return false;
        }
        
        _player.CanMove = false;
        _player.ForcePosture(ShoweringPosition.position, LookAt.position, LookTime);
        Invoke(nameof(PlayShower), LookTime);

        return true;
    }

    private void PlayShower()
    {
        ShowerVFX.Play();
        FMODUnity.RuntimeManager.PlayOneShotAttached("event:/Sfx/Loop1/Shower", gameObject);
        Invoke(nameof(EndEvent), ShowerTime);
    }

    private void EndEvent()
    {
        _player.CanMove = true;
        ShowerVFX.Stop();
    }
}
