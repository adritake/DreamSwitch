using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiddenAlarm : MonoBehaviour
{
    public Transform WardrobeDoor;
    public Vector3 OpenAngles;
    public float OpenDuration;

    private FMOD.Studio.EventInstance e_Alarm;
    private PlayerTrigger _alarmTrigger;
    private bool _isAlarmOn;

    private void Start()
    {
        _alarmTrigger = GetComponentInChildren<PlayerTrigger>();
        InitializeSound();
        _isAlarmOn = true;
    }

    private void Update()
    {
        CheckAlarmTrigger();
    }

    private void CheckAlarmTrigger()
    {
        if (_alarmTrigger.IsTriggered && _isAlarmOn)
        {
            Debug.Log("Trigger turn off alarm");
            _isAlarmOn = false;
            TurnOffAlarm();
            OpenWardrobeDoor();
        }
    }

    private void InitializeSound()
    {
        e_Alarm = FMODUnity.RuntimeManager.CreateInstance("event:/Sfx/Loop1/Alarm");
        e_Alarm.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
        e_Alarm.start();
    }
    
    private void TurnOffAlarm()
    {
        e_Alarm.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        FMODUnity.RuntimeManager.PlayOneShotAttached("event:/Sfx/Loop1/AlarmTurnOff", gameObject);
    }

    private void OpenWardrobeDoor()
    {
        WardrobeDoor.transform.DORotate(OpenAngles, OpenDuration);
    }
}
