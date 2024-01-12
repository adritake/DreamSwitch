using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractor : Interactor
{
    void Update()
    {
        InteractEvent();
    }

    private void InteractEvent()
    {
        bool pressedInteract;

        pressedInteract = InputManager.Instance.Interact();

        if (pressedInteract && _isObjectDetected && _detectedObject is Interactable)
        {
            _detectedObject.OnInteractBegin();
        }
    }
}
