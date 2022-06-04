using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToothBrush : ChecklistEvent
{
    public float BrushTime;
    public float SingleBrushTime;
    public Transform BrushPos1;
    public Transform BrushPos2;
    public Transform BrushLookAt;

    private Collider _collider;
    private Vector3 _initialPosition;
    private Quaternion _initialRotation;

    protected override void Start()
    {
        base.Start();
        _collider = GetComponent<Collider>();
        _initialPosition = transform.position;
        _initialRotation = transform.rotation;
    }

    public override void CompleteEvent()
    {
        base.CompleteEvent();

        _player.CanMove = false;
        _collider.enabled = false;
        BrushingProcess();
        Invoke(nameof(EndEvent), BrushTime);
    }

    private void EndEvent()
    {
        _player.CanMove = true;
        _collider.enabled = true;
        transform.position = _initialPosition;
        transform.rotation = _initialRotation;
    }

    private void BrushingProcess()
    {
        transform.position = BrushPos1.position;
        transform.LookAt(BrushLookAt);
        transform.DOMove(BrushPos2.position, SingleBrushTime).SetLoops(5, LoopType.Yoyo).SetEase(Ease.InOutQuad);
    }
}
