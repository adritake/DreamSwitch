using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CeilingFan : ChecklistEvent
{
    public GameObject blade1;
    public GameObject blade2;
    public GameObject blade3;


    protected override void Start()
    {
        base.Start();

    }
    
    public override bool OnInteractBegin()
    {

        blade1.transform.DORotate(new Vector3(360, 0, 0), 0.4f).SetEase(Ease.Linear).SetRelative().SetLoops(-1).Play();
        blade2.transform.DORotate(new Vector3(360, 0, 0), 0.4f).SetEase(Ease.Linear).SetRelative().SetLoops(-1).Play();
        blade3.transform.DORotate(new Vector3(360, 0, 0), 0.4f).SetEase(Ease.Linear).SetRelative().SetLoops(-1).Play();
            
        return true;
    }

    private void EndEvent()
    {
    }
}
