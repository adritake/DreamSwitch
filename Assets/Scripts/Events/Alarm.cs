using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alarm : ChecklistEvent
{

    public override void CompleteEvent()
    {
        base.CompleteEvent();
        Debug.Log("Alarm End");
        LevelChecklistManager.Instance.CompleteEvent(EventName);
    }
}
