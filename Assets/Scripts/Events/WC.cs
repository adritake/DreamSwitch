using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WC : ChecklistEvent
{
    public float PeeTime = 5;
    public Transform LookPosition;
    public GameObject fish;

    public override bool CompleteEvent()
    {
        if (!base.CompleteEvent())
        {
            return false;
        }
        _player.CanMove = false;
        _player.LookAt(LookPosition.position);

        if(DreamLevel.Instance.level == DreamNumber.Dream1)
        {
            _player.GetComponent<PlayerVFXManager>().PlayPeeVFX();
            FMODUnity.RuntimeManager.PlayOneShotAttached("event:/Sfx/Loop1/Pee", gameObject);
        }
        if(DreamLevel.Instance.level == DreamNumber.Dream2)
        {
            Invoke(nameof(ReleaseFish), PeeTime/2);
        }
        Invoke(nameof(ReleasePlayer), PeeTime);
        return true;
    }

    private void ReleaseFish()
    {
        Instantiate(fish, transform);
        FMODUnity.RuntimeManager.PlayOneShotAttached("event:/Sfx/Loop2/Fish", gameObject);
    }

    private void ReleasePlayer()
    {
        _player.CanMove = true;
        _player.GetComponent<PlayerVFXManager>().StopPeeVFX();
    }
}
