using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alarm : ChecklistEvent
{
    
    FMOD.Studio.EventInstance e_Alarm;

    protected override void Start()
    {
        base.Start();
        e_Alarm = FMODUnity.RuntimeManager.CreateInstance("event:/Sfx/Loop1/Alarm");
        e_Alarm.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
        e_Alarm.start();
    }

    public void LowerAlarm()
    {
        e_Alarm.setParameterByName("Lower", 1);
    }

    public override bool CompleteEvent()
    {
        if (!base.CompleteEvent())
        {
            return false;
        }
        e_Alarm.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        FMODUnity.RuntimeManager.PlayOneShotAttached("event:/Sfx/Loop1/AlarmTurnOff", gameObject);
        Debug.Log("Alarm End");
        return true;
    }
}
