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
    private PlayerCameraManager _cameraManager;
    private Vector3 _moveDirection = Vector3.zero;
    private float _rotationX = 0;
    private float _movementDirectionY;

    private bool _canMove = true;

    FMOD.Studio.EventInstance e_Steps;

    public bool CanMove
    {
        get { return _canMove; }
        set
        {
            _canMove = value;
            _cameraManager.EnableHandHeldMovement(value);
        }
    }

    void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _cameraManager = GetComponent<PlayerCameraManager>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        e_Steps = FMODUnity.RuntimeManager.CreateInstance("event:/Sfx/General/Steps");
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
            curSpeedX = _canMove ? (isRunning ? RunningSpeed : WalkingSpeed) * InputSystem.Instance.switchButtons.StickLY : 0;
            curSpeedY = _canMove ? (isRunning ? RunningSpeed : WalkingSpeed) * InputSystem.Instance.switchButtons.StickLX : 0;
        }
    #else
        {
            curSpeedX = _canMove ? (isRunning ? RunningSpeed : WalkingSpeed) * Input.GetAxis("Vertical") : 0;
            curSpeedY = _canMove ? (isRunning ? RunningSpeed : WalkingSpeed) * Input.GetAxis("Horizontal") : 0;
    }
    #endif

        _movementDirectionY = _moveDirection.y;
        _moveDirection = (forward * curSpeedX) + (right * curSpeedY);
        _moveDirection.y = _movementDirectionY;

        if(_moveDirection.x != 0 || _moveDirection.y != 0)
        {
            FMOD.Studio.PLAYBACK_STATE state;
            e_Steps.getPlaybackState(out state);

            if(state == FMOD.Studio.PLAYBACK_STATE.STOPPED)
            {
                e_Steps.start();
            }
        }

        else
        {
            e_Steps.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        }
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
        var direction = AdjustDirectionToSlope(_moveDirection);
        _characterController.Move(direction * Time.deltaTime);
    }

    private void Look()
    {
        if (_canMove)
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


    private Vector3 AdjustDirectionToSlope(Vector3 direction)
    {
        var ray = new Ray(transform.position, Vector3.down);

        if (Physics.Raycast(ray, out RaycastHit hitInfo, 2))
        {
            var slopeRotation = Quaternion.FromToRotation(Vector3.up, hitInfo.normal);
            var adjustedVelocity = slopeRotation * direction;
            if (adjustedVelocity.y < 0)
            {
                return adjustedVelocity;
            }
        }
        return direction;
    }

    public void LookAt(Vector3 position)
    {
        transform.LookAt(new Vector3(position.x, transform.position.y, position.z));
        PlayerCamera.transform.LookAt(position);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + _moveDirection * 3);
    }
}
