using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alarm : MonoBehaviour, IInteractable, IChecklistEvent
{
    private bool _looked;

    public void OnInteractBegin()
    {
        Debug.Log("Alarm turned off");
    }

    public void OnInteractEnd()
    {
    }

    public void OnLookedBegin()
    {
        if (!_looked)
        {
            _looked = true;
            Debug.Log("Alarm look begin");
        }
    }

    public void OnLookedEnd()
    {
        if (_looked)
        {
            _looked = false;
            Debug.Log("Alarm look begin");
        }
    }

    public void CompleteEvent()
    {
        Debug.Log("Alarm End");
    }
}
