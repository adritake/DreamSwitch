using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]

public class PlayerController : MonoBehaviour
{
    public CharacterInputActions InputEvents;

    [Header("Movement")]
    public float WalkingSpeed = 7.5f;
    public float RunningSpeed = 11.5f;
    public float Gravity = 20.0f;

    [Header("Look")]
    public Camera PlayerCamera;
    public float LookSpeed = 2.0f;
    public float LookXLimit = 45.0f;

    private CharacterController _characterController;
    private Vector3 _moveDirection = Vector3.zero;
    private float _rotationX = 0;
    private float _movementDirectionY;

    [HideInInspector]
    public bool CanMove = true;
    

    void Start()
    {
        _characterController = GetComponent<CharacterController>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        CalculateMovement();
        ApplyGravity();
        Move();
        Look();
    }

    private void CalculateMovement()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);
        bool isRunning = false;

        float curSpeedX = CanMove ? (isRunning ? RunningSpeed : WalkingSpeed) * InputEvents.GetMovementAction().z : 0;
        float curSpeedY = CanMove ? (isRunning ? RunningSpeed : WalkingSpeed) * InputEvents.GetMovementAction().x : 0;
        _movementDirectionY = _moveDirection.y;
        _moveDirection = (forward * curSpeedX) + (right * curSpeedY);
        _moveDirection.y = _movementDirectionY;
    }

    private void ApplyGravity()
    {
        if (!_characterController.isGrounded)
        {
            _moveDirection.y -= Gravity * Time.deltaTime;
        }
        else
        {
            _moveDirection.y = 0;
        }
    }

    private void Move()
    {
        _characterController.Move(_moveDirection * Time.deltaTime);
    }

    private void Look()
    {
        if (CanMove)
        {
            _rotationX += -InputEvents.GetRotationAction().y * LookSpeed * Time.deltaTime;
            _rotationX = Mathf.Clamp(_rotationX, -LookXLimit, LookXLimit);
            PlayerCamera.transform.localRotation = Quaternion.Euler(_rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, InputEvents.GetRotationAction().x * LookSpeed * Time.deltaTime, 0);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + _moveDirection * 3);
    }
}
