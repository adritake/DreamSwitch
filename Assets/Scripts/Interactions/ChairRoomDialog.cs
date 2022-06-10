using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChairRoomDialog : MonoBehaviour, IInteractable
{

    public void OnLookedBegin()
    {
    }
    public void OnLookedEnd()
    {

    }
    public void OnInteractBegin()
    {
        Debug.Log("Me estan Interactuando");
        TextController.Instance.StartDialog("chairRoom_1");
    }
    public void OnInteractEnd(){

    }
}
