using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alarm : ChecklistEvent
{

    protected override void Start()
    {
        base.Start();
    }

    public override void CompleteEvent()
    {
        base.CompleteEvent();
        Debug.Log("Alarm End");
    }
}
