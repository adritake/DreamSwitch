using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WC : ChecklistEvent
{
    public float PeeTime = 5;
    public Transform LookPosition;
    public Transform PeePosition;
    public float LookTime;
    public GameObject fish;

    public override bool OnInteractBegin()
    {
        if (!base.OnInteractBegin())
        {
            return false;
        }
        _player.CanMove = false;
        _player.ForcePosture(PeePosition.position, LookPosition.position, LookTime);

        if(DreamLevel.Instance.level == DreamNumber.Dream1)
        {
            Invoke(nameof(PeeStandard), LookTime);
        }
        if(DreamLevel.Instance.level == DreamNumber.Dream2)
        {
            Invoke(nameof(PeeFish), PeeTime/2);
        }
        Invoke(nameof(ReleasePlayer), PeeTime);
        return true;
    }

    private void PeeStandard()
    {
        _player.GetComponent<PlayerVFXManager>().PlayPeeVFX();
        FMODUnity.RuntimeManager.PlayOneShotAttached("event:/Sfx/Loop1/Pee", gameObject);
    }

    private void PeeFish()
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
