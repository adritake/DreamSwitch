using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ChecklistEvent : MonoBehaviour, IInteractable
{
    private bool _looked;
    protected PlayerController _player;

    private void Start()
    {
        _player = FindObjectOfType<PlayerController>();
    }

    public virtual void CompleteEvent()
    {
        Debug.Log("Completed event : " + gameObject.name);
    }

    public virtual void OnInteractBegin()
    {
    }

    public virtual void OnInteractEnd()
    {
    }

    public void OnLookedBegin()
    {
        if (!_looked)
        {
            _looked = true;
            Debug.Log(gameObject.name + " look begin");
        }
    }

    public void OnLookedEnd()
    {
        if (_looked)
        {
            _looked = false;
            Debug.Log(gameObject.name + " look end");
        }
    }
}
