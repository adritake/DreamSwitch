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

    public override bool OnInteractBegin()
    {
        if (!base.OnInteractBegin())
        {
            return false;
        }
        _player.CanMove = false;
        Clothes.transform.DOMove(ClothesEndPosition.position, ClothesMovingTime).SetEase(Ease.InOutQuad);

        FMODUnity.RuntimeManager.PlayOneShotAttached("event:/Sfx/Loop1/Wardrobe", gameObject);

        Invoke(nameof(EndEvent), DressingTime);
        return true;
    }

    private void EndEvent()
    {
        _player.CanMove = true;
        Destroy(Clothes);
    }
}
