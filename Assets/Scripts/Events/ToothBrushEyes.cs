using DG.Tweening;
using UnityEngine;

public class ToothBrushEyes : ChecklistEvent
{
    public float BrushTime;
    public float SingleBrushTime;
    public int BrushRepeats;
    public Transform BrushPos1;
    public Transform BrushPos2;
    public Transform BrushLookAt;
    public Material PainPostProcessingMaterial;

    private Collider _collider;
    private Vector3 _initialPosition;
    private Quaternion _initialRotation;
    private const string POST_PROCESSING_PROPERTY = "_EffectIntensity";

    protected override void Start()
    {
        base.Start();
        _collider = GetComponent<Collider>();
        _initialPosition = transform.position;
        _initialRotation = transform.rotation;
    }

    public override bool OnInteractBegin()
    {
        if (!base.OnInteractBegin())
        {
            return false;
        }

        _player.CanMove = false;
        _collider.enabled = false;
        BrushingProcess();
        FMODUnity.RuntimeManager.PlayOneShotAttached("event:/Sfx/Loop1/Toothbrush", gameObject);
        PainPostProcessingMaterial.DOFloat(1, POST_PROCESSING_PROPERTY, 0.5f);
        Invoke(nameof(EndEvent), BrushTime);
        return true;
    }

    private void EndEvent()
    {
        _player.CanMove = true;
        _collider.enabled = true;
        transform.position = _initialPosition;
        transform.rotation = _initialRotation;
        PainPostProcessingMaterial.DOFloat(0, POST_PROCESSING_PROPERTY, 0.5f);
    }

    private void BrushingProcess()
    {
        transform.position = BrushPos1.position;
        transform.LookAt(BrushLookAt);
        transform.DOMove(BrushPos2.position, SingleBrushTime).SetLoops(BrushRepeats, LoopType.Yoyo).SetEase(Ease.InOutQuad);
    }
}
