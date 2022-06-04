using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wardrobe : ChecklistEvent
{
    public float DressingTime;
    public GameObject Clothes;
    public Transform ClothesEndPosition;
    public float ClothesMovingTime;

    public override void CompleteEvent()
    {
        base.CompleteEvent();
        _player.CanMove = false;
        Clothes.transform.DOMove(ClothesEndPosition.position, ClothesMovingTime).SetEase(Ease.InOutQuad);
        Invoke(nameof(EndEvent), DressingTime);
    }

    private void EndEvent()
    {
        _player.CanMove = true;
        Destroy(Clothes);
        LevelChecklistManager.Instance.CompleteEvent(EventName);
    }
}
