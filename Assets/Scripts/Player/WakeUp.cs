using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class WakeUp : MonoBehaviour
{
    public Eyelids EyeLids;
    public Transform WakeUpEndPosition;
    public Vector3 WakeUpStartRotation = new Vector3(-90, 0, 0);
    public Vector3 WakeUpEndRotation = new Vector3(0, 90, 0);

    public float WakeUpTime;

    private PlayerController _playerController;
    private PlayerCameraManager _cameraManager;

    void Start()
    {
        _playerController = GetComponent<PlayerController>();
        _cameraManager = GetComponent<PlayerCameraManager>();
        InitiatePlayer();
        WakeUpPlayer();
    }

    private void InitiatePlayer()
    {
        _playerController.CanMove = false;
        transform.rotation = Quaternion.Euler(WakeUpStartRotation);
        EyeLids.CloseEyes(true);
    }

    private void WakeUpPlayer()
    {
        EyeLids.OpenEyes();
        _cameraManager.EnableHandHeldMovement(false);
        Sequence sequence = DOTween.Sequence();
        sequence.AppendInterval(2);
        sequence.Append(transform.DORotate(WakeUpEndRotation, WakeUpTime));
        sequence.Join(transform.DOMove(WakeUpEndPosition.position, WakeUpTime));
        sequence.AppendCallback(() => _playerController.CanMove = true);
        sequence.AppendCallback(() => _cameraManager.EnableHandHeldMovement(true));

        sequence.Play();
    }
}
