using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChairRoomDialog : ChecklistEvent
{

    public override bool CompleteEvent()
    {

        TextController.Instance.StartDialog("chairRoom_1");

        return true;
    }
}
