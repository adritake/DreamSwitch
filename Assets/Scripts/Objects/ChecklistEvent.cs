using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ChecklistEvent : MonoBehaviour, IInteractable
{
    public string EventName;

    private bool _looked;
    protected PlayerController _player;

    protected virtual void Start()
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
            ReticleUI.Instance.EnableReticle(true);
            Debug.Log(gameObject.name + " look begin");
        }
    }

    public void OnLookedEnd()
    {
        if (_looked)
        {
            _looked = false;
            ReticleUI.Instance.EnableReticle(false);
            Debug.Log(gameObject.name + " look end");
        }
    }
}
