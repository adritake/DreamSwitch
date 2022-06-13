using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Eyeball : ChecklistEvent
{
    public Transform player;

    FMOD.Studio.EventInstance e_Alarm;

    Sequence seq;

    protected override void Start()
    {
        base.Start();
        e_Alarm = FMODUnity.RuntimeManager.CreateInstance("event:/Sfx/Loop2/Eyeball");
        e_Alarm.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
        e_Alarm.start();

        seq = DOTween.Sequence();

        seq.Append(transform.DOMoveY(0.05f, 0.08f).SetRelative())
            .Append(transform.DOMoveY(-0.05f, 0.08f).SetRelative())
            .SetLoops(-1)
            .Play();
    }

    void Update()
    {
        transform.LookAt(player);
    }

    public void LowerAlarm()
    {
        e_Alarm.setParameterByName("Lower", 1);
    }

    public override bool CompleteEvent()
    {
        /* if (!base.CompleteEvent())
        {
            return false;
        } */
        e_Alarm.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        seq.Kill();
        FMODUnity.RuntimeManager.PlayOneShotAttached("event:/Sfx/Loop1/AlarmTurnOff", gameObject);
        Debug.Log("Alarm End");
        return true;
    }
}
