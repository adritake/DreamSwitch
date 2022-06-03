using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGraber : MonoBehaviour
{
    public Transform LookStart;
    public float GrabDistance;
    public Transform GrabPlace;
    public LayerMask GrabableObjectsLayer;

    private GrabableObject _detectedObject;
    private bool _isObjectDetected;
    private GrabableObject _grabedObject;
    private bool _isObjectGrabed;

    void Update()
    {
        DetectObject();
        GrabObject();
    }

    private void DetectObject()
    {
        if(Physics.Raycast(LookStart.position, LookStart.forward, out RaycastHit hitInfo, GrabDistance, GrabableObjectsLayer))
        {
            _isObjectDetected = true;
            _detectedObject = hitInfo.collider.gameObject.GetComponent<GrabableObject>();
            _detectedObject.OnLookedBegin();
        }
        else
        {
            _isObjectDetected = false;
            if (_detectedObject)
            {
                _detectedObject.OnLookedEnd();
            }
        }
    }

    private void GrabObject()
    {
        if (Input.GetKeyDown(KeyCode.E) && !_isObjectGrabed)
        {
            _detectedObject.OnInteractBegin();
            _isObjectGrabed = true;
            _grabedObject = _detectedObject;
            Debug.Log("Object grabed");
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(LookStart.position, LookStart.position + LookStart.forward * GrabDistance);
    }
}
