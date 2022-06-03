using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterInputActions : MonoBehaviour
{
    
    InputAction.CallbackContext _interact;
    Vector3 _movementDirection;
    Vector2 _rotationVector;

    public CharacterInputActions()
    {
        _movementDirection = Vector3.zero;
    }

    void Update()
    {
        Debug.Log(_movementDirection);
    }

    public void OnInteract(InputAction.CallbackContext value)
    {
        _interact = value;
    }

    public void OnMovement(InputAction.CallbackContext value)
    {
        Vector2 inputMovement = value.ReadValue<Vector2>();
        _movementDirection = new Vector3(inputMovement.x, 0, inputMovement.y);
    }

    public void OnRotation(InputAction.CallbackContext value)
    {
        Vector2 rotation = value.ReadValue<Vector2>();
        _rotationVector = new Vector2(rotation.x,rotation.y);
    }

    public void OnMouseRotationX(InputAction.CallbackContext value)
    {
        float rotationX = value.ReadValue<float>();
        _rotationVector = new Vector2(rotationX, _rotationVector.y);
    }

    public void OnMouseRotationY(InputAction.CallbackContext value)
    {
        float rotationY = value.ReadValue<float>();
        _rotationVector = new Vector2(_rotationVector.x, rotationY);
    }

    public Vector3 GetMovementAction()
    {
        return _movementDirection;
    }

    public Vector2 GetRotationAction()
    {
        return _rotationVector;
    }

    public bool GetInteract()
    {
        if (_interact.action != null)
        {
            return _interact.action.WasPressedThisFrame();
        }
        else
        {
            return false;
        }
    }


}
