using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alarm : ChecklistEvent
{

    protected override void Start()
    {
        base.Start();
    }

    public override bool CompleteEvent()
    {
        if (!base.CompleteEvent())
        {
            return false;
        }

        Debug.Log("Alarm End");
        return true;
    }
}
