using DG.Tweening;
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
    public float LookSpeed = 1.0f;
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
        e_Steps.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
    }

    void Update()
    {
        CalculateMovement();
        AdjustDirectionToSlope();
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

        curSpeedX = _canMove ? (isRunning ? RunningSpeed : WalkingSpeed) * InputManager.Instance.Movement().x : 0;
        curSpeedY = _canMove ? (isRunning ? RunningSpeed : WalkingSpeed) * InputManager.Instance.Movement().y : 0;

        _moveDirection = (right * curSpeedX) + (forward * curSpeedY);

        if (_moveDirection.x != 0 || _moveDirection.y != 0)
        {
            FMOD.Studio.PLAYBACK_STATE state;
            e_Steps.getPlaybackState(out state);

            if (state == FMOD.Studio.PLAYBACK_STATE.STOPPED)
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
        _moveDirection.y -= Gravity * Time.deltaTime;
    }

    private void Move()
    {
        _characterController.Move(_moveDirection * Time.deltaTime);
    }

    private void Look()
    {
        if (_canMove)
        {
            _rotationX += -InputManager.Instance.Camera().y * LookSpeed * Time.deltaTime;
            
            _rotationX = Mathf.Clamp(_rotationX, -LookXLimit, LookXLimit);
            PlayerCamera.transform.localRotation = Quaternion.Euler(_rotationX, 0, 0);

            transform.rotation *= Quaternion.Euler(0, InputManager.Instance.Camera().x * LookSpeed * Time.deltaTime, 0);
        }
    }


    private void AdjustDirectionToSlope()
    {
        var ray = new Ray(transform.position, Vector3.down);

        if (Physics.Raycast(ray, out RaycastHit hitInfo, 2))
        {
            var slopeRotation = Quaternion.FromToRotation(Vector3.up, hitInfo.normal);
            var adjustedVelocity = slopeRotation * _moveDirection;
            if (adjustedVelocity.y < 0)
            {
                _moveDirection = adjustedVelocity;
            }
        }
    }

    public void ForcePosture(Vector3 position, Vector3 lookTarget, float time)
    {
        transform.DOMove(position, time);
        StartCoroutine(LookingAtCoroutine(lookTarget, time));
    }

    private IEnumerator LookingAtCoroutine(Vector3 lookTarget, float time)
    {
        float startTime = Time.time;
        while(startTime + time > Time.time)
        {
            transform.LookAt(new Vector3(lookTarget.x, transform.position.y, lookTarget.z));
            PlayerCamera.transform.LookAt(lookTarget);
            yield return null;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + _moveDirection * 3);
    }
}
