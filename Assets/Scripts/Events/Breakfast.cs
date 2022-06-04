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


    public override void CompleteEvent()
    {
        base.CompleteEvent();
        _player.CanMove = false;
        Toast.transform.DOMove(ToastEndPosition.position, ClothesMovingTime).SetEase(Ease.InOutQuad);
        Invoke(nameof(EndEvent), EatingTime);
    }

    private void EndEvent()
    {
        _player.CanMove = true;
        LevelChecklistManager.Instance.CompleteEvent(EventName);
        Destroy(Toast);
    }
}
