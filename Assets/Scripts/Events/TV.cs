using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TV : ChecklistEvent
{
    public float WatchTime;
    public Renderer TVScreen;
    public Transform LookAt;


    public override bool CompleteEvent()
    {
        if (!base.CompleteEvent())
        {
            return false;
        }
        _player.CanMove = false;
        _player.LookAt(LookAt.position);
        TurnOnTv(true);
        Invoke(nameof(EndEvent), WatchTime);
        return true;
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
