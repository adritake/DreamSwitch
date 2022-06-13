using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    private bool _looked;

    protected virtual void Start()
    {

    }

    public virtual void OnLookedBegin()
    {
        if (!_looked)
        {
            _looked = true;
            ReticleUI.Instance.EnableReticle(true);
            Debug.Log(gameObject.name + " look begin");
        }
    }

    public virtual void OnLookedEnd()
    {
        if (_looked)
        {
            _looked = false;
            ReticleUI.Instance.EnableReticle(false);
            Debug.Log(gameObject.name + " look end");
        }
    }

    public virtual bool OnInteractBegin()
    {
        return true;
    }

    public virtual void OnInteractEnd()
    {
        
    }
}
