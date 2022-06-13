using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabableObject : Interactable
{
    public string GrabedObjectsLayer;

    private int _initialLayer;

    protected override void Start()
    {
        _initialLayer = gameObject.layer;
    }

    #region Interactable
    public override bool OnInteractBegin()
    {
        Debug.Log("Object interact begin");
        //MoveToLayer(transform, LayerMask.NameToLayer(GrabedObjectsLayer));
        OnLookedEnd();
        return true;
    }

    public override void OnInteractEnd()
    {
        Debug.Log("Object interact end");
        MoveToLayer(transform, _initialLayer);
    }
    #endregion

    private void MoveToLayer(Transform root, int layer)
    {
        root.gameObject.layer = layer;
        foreach (Transform child in root)
            MoveToLayer(child, layer);
    }
}
