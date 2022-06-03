using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabableObject : MonoBehaviour, IInteractable
{
    public string GrabedObjectsLayer;

    private int _initialLayer;
    private bool _lookedAt;

    void Start()
    {
        _initialLayer = gameObject.layer;
    }

    #region Interactable
    public void OnInteractBegin()
    {
        Debug.Log("Object interact begin");
        gameObject.layer = LayerMask.NameToLayer(GrabedObjectsLayer);
        OnLookedEnd();
    }

    public void OnInteractEnd()
    {
        Debug.Log("Object interact end");
        gameObject.layer = _initialLayer;
    }

    public void OnLookedBegin()
    {
        if (!_lookedAt)
        {
            _lookedAt = true;
            Debug.Log("Object looked begin");
        }
    }

    public void OnLookedEnd()
    {
        if (_lookedAt)
        {
            _lookedAt = false;
            Debug.Log("Object looked end");
        }
    }
    #endregion
   
}
