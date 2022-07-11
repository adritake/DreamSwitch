using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CeilingFan : ChecklistEvent
{
    public GameObject Blade1;
    public GameObject Blade2;
    public GameObject Blade3;

    public override bool OnInteractBegin()
    {
        FMODUnity.RuntimeManager.PlayOneShotAttached("event:/Sfx/Loop1/AlarmTurnOff", gameObject);

        Invoke(nameof(StartSpinning), 0.5f);
        return true;
    }

    private void StartSpinning()
    {
        FMODUnity.RuntimeManager.PlayOneShotAttached("event:/Sfx/Loop2/Ceiling Fan", gameObject);
        Blade1.transform.DORotate(new Vector3(360, 0, 0), 0.4f).SetEase(Ease.Linear).SetRelative().SetLoops(-1).Play();
        Blade2.transform.DORotate(new Vector3(360, 0, 0), 0.4f).SetEase(Ease.Linear).SetRelative().SetLoops(-1).Play();
        Blade3.transform.DORotate(new Vector3(360, 0, 0), 0.4f).SetEase(Ease.Linear).SetRelative().SetLoops(-1).Play();
    }
}
