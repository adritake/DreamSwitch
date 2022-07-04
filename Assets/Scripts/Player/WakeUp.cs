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
    public float timeBeforeWakeup;
    public float timeBeforeGetup;

    private PlayerController _playerController;

    void Start()
    {
        _playerController = GetComponent<PlayerController>();
        InitiatePlayer();

        if(DreamLevel.Instance.level == DreamNumber.Dream1)
        {
            StartCoroutine(WakeUpPlayer());
        }
        if(DreamLevel.Instance.level == DreamNumber.Dream2)
        {
            StartCoroutine(WakeUpPlayer2());
        }
    }

    private void InitiatePlayer()
    {
        _playerController.CanMove = false;
        transform.rotation = Quaternion.Euler(WakeUpStartRotation);
        EyeLids.CloseEyes(true);
    }

    private IEnumerator WakeUpPlayer()
    {
        FMODUnity.RuntimeManager.PlayOneShotAttached("event:/Sfx/Loop1/Wakeup", gameObject);
        yield return new WaitForSeconds(timeBeforeWakeup);
        EyeLids.OpenEyes();
        yield return new WaitForSeconds(1);
        TextController.Instance.StartDialog("wakeUp_1");
        yield return new WaitForSeconds(timeBeforeGetup-1);
        BGMManager.Instance.Play();
        FindObjectOfType<Alarm>().LowerAlarm();
        Sequence sequence = DOTween.Sequence();
        sequence.AppendInterval(2);
        sequence.Append(transform.DORotate(WakeUpEndRotation, WakeUpTime));
        sequence.Join(transform.DOMove(WakeUpEndPosition.position, WakeUpTime));
        sequence.AppendCallback(() => _playerController.CanMove = true);

        sequence.Play();
    }

    private IEnumerator WakeUpPlayer2()
    {
        FMODUnity.RuntimeManager.PlayOneShotAttached("event:/Sfx/Loop1/Wakeup", gameObject);
        yield return new WaitForSeconds(timeBeforeWakeup);
        EyeLids.OpenEyes();
        yield return new WaitForSeconds(1);
        TextController.Instance.StartDialog("wakeUp_1");
        yield return new WaitForSeconds(timeBeforeGetup-1);
        FindObjectOfType<Eyeball>().LowerAlarm();
        Sequence sequence = DOTween.Sequence();
        sequence.AppendInterval(2);
        sequence.Append(transform.DORotate(WakeUpEndRotation, WakeUpTime));
        sequence.Join(transform.DOMove(WakeUpEndPosition.position, WakeUpTime));
        sequence.AppendCallback(() => _playerController.CanMove = true);

        sequence.Play();
    }
}