using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentInteractor : Interactor
{
    void Update()
    {
        InteractEvent();
    }

    private void InteractEvent()
    {
        bool pressedInteract;

        #if !UNITY_EDITOR && UNITY_SWITCH
                    pressedInteract = InputSystem.Instance.switchButtons.A;
        #else
                pressedInteract = Input.GetKeyDown(KeyCode.E);
        #endif

        if (pressedInteract && _isObjectDetected && _detectedObject is IInteractable)
        {
            ((IInteractable)_detectedObject).OnInteractBegin();
        }
    }
}
