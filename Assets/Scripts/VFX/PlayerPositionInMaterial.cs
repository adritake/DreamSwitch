using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPositionInMaterial : MonoBehaviour
{
    public Material[] Materials;
    public string PlayerPositionParameterName = "_PlayerPosition";
    public bool ApplyPositionOnStart;

    private bool _applyPosition;

    private void Start()
    {
        ApplyPosition(ApplyPositionOnStart);
    }

    void Update()
    {
        if (_applyPosition)
        {
            SetParameter(transform.position);
        }
    }

    private void SetParameter(Vector3 position)
    {
        foreach (var material in Materials)
        {
            material.SetVector(PlayerPositionParameterName, position);
        }
    }

    public void ApplyPosition(bool apply)
    {
        _applyPosition = apply;

        if (!apply)
        {
            SetParameter(Vector3.one * Mathf.Infinity);
        }
    }
}
