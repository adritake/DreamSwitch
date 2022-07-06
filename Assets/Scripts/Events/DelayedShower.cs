using UnityEngine;

public class DelayedShower : ChecklistEvent
{
    public float ShowerTime;
    public Transform LookAt;
    public Transform ShoweringPosition;
    public float LookTime;
    public PlayerTrigger StartShowerSoundTrigger;
    public PlayerTrigger EndShowerSoundTrigger;

    private bool _interactedOnce;
    private bool _soundPlaying;

    private void Update()
    {
        CheckShowerTrigger();
    }

    private void CheckShowerTrigger()
    {
        if (StartShowerSoundTrigger.IsTriggered && _interactedOnce)
        {
            _soundPlaying = true;
            FMODUnity.RuntimeManager.PlayOneShotAttached("event:/Sfx/Loop1/Shower", gameObject);
            base.OnInteractBegin();
        }

        if (EndShowerSoundTrigger.IsTriggered && _soundPlaying)
        {
            _soundPlaying = false;
            // STOP SHOWER SOUND
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
