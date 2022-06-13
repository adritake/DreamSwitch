using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakfast : ChecklistEvent
{
    public float EatingTime;
    public GameObject Toast;
    public Transform ToastMidPosition;
    public Transform ToastEndPosition;
    public float ClothesMovingTime;

    Sequence seq;

    protected override void Start()
    {
        base.Start();

        seq = DOTween.Sequence();
    }
    
    public override bool OnInteractBegin()
    {
        _player.CanMove = false;

        seq.Append(Toast.transform.DOMove(ToastMidPosition.position, ClothesMovingTime/2).SetEase(Ease.InOutQuad))
            .Append(Toast.transform.DOMove(ToastEndPosition.position, ClothesMovingTime/2).SetEase(Ease.InOutQuad));
        //Toast.transform.DOMove(ToastEndPosition.position, ClothesMovingTime).SetEase(Ease.InOutQuad);
        //FMODUnity.RuntimeManager.PlayOneShotAttached("event:/Sfx/Loop1/Toast", gameObject);
        Invoke(nameof(EndEvent), EatingTime);
        return true;
    }

    private void EndEvent()
    {
        _player.CanMove = true;
        Destroy(Toast);
    }
}
