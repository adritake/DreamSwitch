using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChairRoomDialog : Interactable
{
    public override bool OnInteractBegin()
    {
        TextController.Instance.StartDialog("chairRoom_1");
        return true;
    }
}
