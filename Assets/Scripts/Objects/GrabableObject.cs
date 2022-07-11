using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabableObject : Interactable
{
    private int _initialLayer;

    protected override void Start()
    {
        _initialLayer = gameObject.layer;
    }

    #region Interactable
    public override bool OnInteractBegin()
    {
        Debug.Log("Object interact begin");
        OnLookedEnd();
        return true;
    }

    public override void OnInteractEnd()
    {
        Debug.Log("Object interact end");
    }
    #endregion
}
