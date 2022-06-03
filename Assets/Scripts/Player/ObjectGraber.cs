using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGraber : MonoBehaviour
{

    public CharacterInputActions InputEvents;

    public Transform LookStart;
    public float GrabDistance;
    public Transform GrabPlace;
    public LayerMask GrabableObjectsLayer;
    public float GrabSpeed;

    private GrabableObject _detectedObject;
    private bool _isObjectDetected;
    private GrabableObject _grabedObject;
    private bool _isObjectGrabed;

    void Update()
    {
        UseObject();
        GrabObject();
    }

    private void FixedUpdate()
    {
        DetectObject();
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
        if (InputEvents.GetInteract() && !_isObjectGrabed && _isObjectDetected)
        {
            _detectedObject.OnInteractBegin();
            _isObjectGrabed = true;
            _grabedObject = _detectedObject;
            Debug.Log("Object grabed");
            StartCoroutine(MoveToGrabPlaceCoroutine());
        }
    }

    private void UseObject()
    {
        if (InputEvents.GetInteract() && _isObjectGrabed)
        {
            _grabedObject.OnInteractEnd();
            _isObjectGrabed = false;
            Debug.Log("Object used");

            _grabedObject.transform.parent = null;
        }
    }

    private IEnumerator MoveToGrabPlaceCoroutine()
    {
        _grabedObject.transform.LookAt(Camera.main.transform);

        while(Vector3.Distance(_grabedObject.transform.position, GrabPlace.position) > 0.1f)
        {
            Vector3 translation = (GrabPlace.position - _grabedObject.transform.position).normalized * GrabSpeed * Time.deltaTime;
            _grabedObject.transform.position += translation;

            yield return null;
        }
        _grabedObject.transform.LookAt(Camera.main.transform);
        _grabedObject.transform.parent = GrabPlace;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(LookStart.position, LookStart.position + LookStart.forward * GrabDistance);
    }
}
