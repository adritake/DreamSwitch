using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGraber : Interactor
{
    [Header("Graber")]
    public Transform GrabPlace;
    public float GrabSpeed;
    public GrabableObject GrabedObject => _grabedObject;

    private GrabableObject _grabedObject;
    private bool _isObjectGrabed;

    void Update()
    {
        GrabObject();
    }

    private void GrabObject()
    {
        bool pressedInteract;

        #if !UNITY_EDITOR && UNITY_SWITCH
            pressedInteract = InputSystem.Instance.switchButtons.A;
        #else
            pressedInteract = Input.GetKeyDown(KeyCode.E);
        #endif

        if (pressedInteract && !_isObjectGrabed && _isObjectDetected && _detectedObject is GrabableObject)
        {
            _isObjectGrabed = true;
            _grabedObject = (GrabableObject)_detectedObject;
            Debug.Log("Object grabed");
            StartCoroutine(MoveToGrabPlaceCoroutine());
        }
    }

    private IEnumerator MoveToGrabPlaceCoroutine()
    {
        _grabedObject.transform.LookAt(Camera.main.transform);

        while (Vector3.Distance(_grabedObject.transform.position, GrabPlace.position) > 0.1f)
        {
            Vector3 translation = (GrabPlace.position - _grabedObject.transform.position).normalized * GrabSpeed * Time.deltaTime;
            _grabedObject.transform.position += translation;

            yield return null;
        }
        _grabedObject.transform.LookAt(Camera.main.transform);
        _grabedObject.transform.parent = GrabPlace;
    }

    public void DeleteGrabedObject()
    {
        Destroy(_grabedObject.gameObject);
        _grabedObject = null;
        _isObjectGrabed = false;
    }
}
