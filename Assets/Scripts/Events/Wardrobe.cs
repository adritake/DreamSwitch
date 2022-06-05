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

    public override bool CompleteEvent()
    {
        if (!base.CompleteEvent())
        {
            return false;
        }
        _player.CanMove = false;
        Clothes.transform.DOMove(ClothesEndPosition.position, ClothesMovingTime).SetEase(Ease.InOutQuad);
        Invoke(nameof(EndEvent), DressingTime);
        return true;
    }

    private void EndEvent()
    {
        _player.CanMove = true;
        Destroy(Clothes);
    }
}
