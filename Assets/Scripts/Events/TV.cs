using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TV : ChecklistEvent
{
    public float WatchTime;
    public Renderer TVScreen;
    public Transform LookAt;
    public Transform LookFromPosition;
    public float LookTime;


    public override bool OnInteractBegin()
    {
        _player.CanMove = false;
        _player.ForcePosture(LookFromPosition.position, LookAt.position, LookTime);
        Invoke(nameof(PlayTv), LookTime);
        
        return true;
    }

    private void PlayTv()
    {
        TurnOnTv(true);
        FMODUnity.RuntimeManager.PlayOneShotAttached("event:/Sfx/Loop1/TVStatic", gameObject);
        Invoke(nameof(EndEvent), WatchTime);
    }

    private void EndEvent()
    {
        _player.CanMove = true;
        TurnOnTv(false);
    }

    private void TurnOnTv(bool turnOn)
    {
        int value = turnOn ? 1 : 0;
        foreach (var material in TVScreen.materials)
        {
            if (material.HasInt("_On"))
            {
                material.SetInt("_On", value);
            }
        }
    }
}
