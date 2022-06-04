using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]

public class PlayerController : MonoBehaviour
{

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
    public bool canMove = true;
    

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

        float curSpeedX;
        float curSpeedY;

    #if !UNITY_EDITOR && UNITY_SWITCH
    {
        curSpeedX = canMove ? (isRunning ? RunningSpeed : WalkingSpeed) * InputSystem.Instance.switchButtons.StickLY : 0;
        curSpeedY = canMove ? (isRunning ? RunningSpeed : WalkingSpeed) * InputSystem.Instance.switchButtons.StickLX : 0;
    }
    #else
    {
        curSpeedX = canMove ? (isRunning ? RunningSpeed : WalkingSpeed) * Input.GetAxis("Vertical") : 0;
        curSpeedY = canMove ? (isRunning ? RunningSpeed : WalkingSpeed) * Input.GetAxis("Horizontal") : 0;
    }
    #endif

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
        if (canMove)
        {
        #if !UNITY_EDITOR && UNITY_SWITCH
        {
            _rotationX += -InputSystem.Instance.switchButtons.StickRY * LookSpeed * Time.deltaTime;
        }
        #else
        {
            _rotationX += -Input.GetAxis("Mouse Y") * LookSpeed * Time.deltaTime;
        }
        #endif
            _rotationX = Mathf.Clamp(_rotationX, -LookXLimit, LookXLimit);
            PlayerCamera.transform.localRotation = Quaternion.Euler(_rotationX, 0, 0);

        #if !UNITY_EDITOR && UNITY_SWITCH
        {
            transform.rotation *= Quaternion.Euler(0, InputSystem.Instance.switchButtons.StickRX * LookSpeed * Time.deltaTime, 0);
        }
        #else
        {
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * LookSpeed * Time.deltaTime, 0);
        }
        #endif
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + _moveDirection * 3);
    }
}
