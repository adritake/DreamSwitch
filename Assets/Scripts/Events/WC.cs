using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WC : ChecklistEvent
{
    public float PeeTime = 5;
    public Transform LookPosition;

    private PlayerController _player;

    private void Start()
    {
        _player = FindObjectOfType<PlayerController>();
    }

    public override void CompleteEvent()
    {
        base.CompleteEvent();
        _player.CanMove = false;
        _player.LookAt(LookPosition.position);
        _player.GetComponent<PlayerVFXManager>().PlayPeeVFX();
        Invoke(nameof(ReleasePlayer), PeeTime);
    }

    private void ReleasePlayer()
    {
        _player.CanMove = true;
        _player.GetComponent<PlayerVFXManager>().StopPeeVFX();
    }
}
