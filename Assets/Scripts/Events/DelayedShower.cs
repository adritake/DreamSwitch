using UnityEngine;

public class DelayedShower : ChecklistEvent
{
    public float ShowerTime;
    public Transform LookAt;
    public Transform ShoweringPosition;
    public float LookTime;

    private PlayerTrigger _showerTrigger;
    private bool _interactedOnce;

    protected override void Start()
    {
        base.Start();
        _showerTrigger = GetComponentInChildren<PlayerTrigger>();
    }

    private void Update()
    {
        CheckShowerTrigger();
    }

    private void CheckShowerTrigger()
    {
        if (_showerTrigger.IsTriggered && _interactedOnce)
        {
            FMODUnity.RuntimeManager.PlayOneShotAttached("event:/Sfx/Loop1/Shower", gameObject);
            base.OnInteractBegin();
        }
    }

    public override bool OnInteractBegin()
    {
        _interactedOnce = true;
        _player.CanMove = false;
        _player.ForcePosture(ShoweringPosition.position, LookAt.position, LookTime);
        Invoke(nameof(PlayShower), LookTime);

        return true;
    }

    private void PlayShower()
    {
        Invoke(nameof(EndEvent), ShowerTime);
    }

    private void EndEvent()
    {
        _player.CanMove = true;
    }
}
