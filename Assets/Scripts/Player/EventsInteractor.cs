using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventsInteractor : Interactor
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

        if (pressedInteract && _isObjectDetected && _detectedObject is ChecklistEvent)
        {
            ((ChecklistEvent)_detectedObject).CompleteEvent();
        }
    }
}
