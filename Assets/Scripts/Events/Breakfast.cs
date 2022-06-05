using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakfast : ChecklistEvent
{
    public float EatingTime;
    public GameObject Toast;
    public Transform ToastEndPosition;
    public float ClothesMovingTime;


    public override bool CompleteEvent()
    {
        if (!base.CompleteEvent())
        {
            return false;
        }
        _player.CanMove = false;
        Toast.transform.DOMove(ToastEndPosition.position, ClothesMovingTime).SetEase(Ease.InOutQuad);
        Invoke(nameof(EndEvent), EatingTime);
        return true;
    }

    private void EndEvent()
    {
        _player.CanMove = true;
        Destroy(Toast);
    }
}
