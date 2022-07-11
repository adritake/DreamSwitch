using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedBear : ChecklistEvent
{
    public string FoodName = "Rib";

    public override bool OnInteractBegin()
    {
        var objectGraber = _player.gameObject.GetComponent<ObjectGraber>();

        if (!_completed && objectGraber.GrabedObject != null && objectGraber.GrabedObject.name == FoodName)
        {
            base.OnInteractBegin();
            objectGraber.DeleteGrabedObject();
            GetComponent<Rigidbody>().isKinematic = false;
            return true;
        }
        else
        {
            return false;
        }
    }
}
