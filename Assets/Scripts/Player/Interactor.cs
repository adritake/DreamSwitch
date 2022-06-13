using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactor : MonoBehaviour
{
    [Header("Interactable")]
    public Transform LookStart;
    public float LookDistance;
    public LayerMask InteractableObjectsLayer;

    protected Interactable _detectedObject;
    protected bool _isObjectDetected;

    private void Update()
    {
        Interact();
    }

    private void FixedUpdate()
    {
        DetectObject();
    }

    private void DetectObject()
    {
        if (Physics.Raycast(LookStart.position, LookStart.forward, out RaycastHit hitInfo, LookDistance, InteractableObjectsLayer))
        {
            _isObjectDetected = true;
            _detectedObject = (Interactable)hitInfo.collider.gameObject.GetComponent(typeof(Interactable));
            _detectedObject.OnLookedBegin();
        }
        else
        {
            _isObjectDetected = false;
            if (_detectedObject != null)
            {
                _detectedObject.OnLookedEnd();
            }
        }
    }

    protected virtual void Interact()
    {
        bool pressedInteract;

#if !UNITY_EDITOR && UNITY_SWITCH
            pressedInteract = InputSystem.Instance.switchButtons.A;
#else
        pressedInteract = Input.GetKeyDown(KeyCode.E);
#endif

        if (pressedInteract && _isObjectDetected)
        {
            _detectedObject.OnInteractBegin();
            Debug.Log("Object interacted");
        }
    }
}
