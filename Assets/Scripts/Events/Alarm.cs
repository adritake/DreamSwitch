using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Alarm : ChecklistEvent
{
    private FMOD.Studio.EventInstance e_Alarm;
    private Sequence _shakeSequence;

    protected override void Start()
    {
        base.Start();

        InitializeSound();
        StartShaking();
    }

    public void LowerAlarm()
    {
        e_Alarm.setParameterByName("Lower", 1);
    }

    public override bool OnInteractBegin()
    {
        if (!base.OnInteractBegin())
        {
            return false;
        }
        e_Alarm.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        _shakeSequence.Kill();
        FMODUnity.RuntimeManager.PlayOneShotAttached("event:/Sfx/Loop1/AlarmTurnOff", gameObject);
        Debug.Log("Alarm End");
        return true;
    }

    private void InitializeSound()
    {
        e_Alarm = FMODUnity.RuntimeManager.CreateInstance("event:/Sfx/Loop1/Alarm");
        e_Alarm.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
        e_Alarm.start();
    }

    private void StartShaking()
    {
        _shakeSequence = DOTween.Sequence();

        _shakeSequence.Append(transform.DOMoveY(0.05f, 0.08f).SetRelative())
            .Append(transform.DOMoveY(-0.05f, 0.08f).SetRelative())
            .Insert(0, transform.DORotate(new Vector3(0, 5, 0), 0.08f).SetRelative())
            .Insert(0.08f, transform.DORotate(new Vector3(0, 0, 0), 0.08f).SetRelative())
            .SetLoops(-1)
            .Play();
    }
}
