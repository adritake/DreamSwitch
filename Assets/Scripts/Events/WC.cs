using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WC : MonoBehaviour, IInteractable, IChecklistEvent
{
    public float PeeTime = 5;
    public Transform LookPosition;

    private PlayerController _player;
    private bool _looked;

    private void Start()
    {
        _player = FindObjectOfType<PlayerController>();
    }

    public void CompleteEvent()
    {
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

    public void OnInteractBegin()
    {
    }

    public void OnInteractEnd()
    {
    }

    public void OnLookedBegin()
    {
        if (!_looked)
        {
            _looked = true;
            Debug.Log("WC look begin");
        }
    }

    public void OnLookedEnd()
    {
        if (_looked)
        {
            _looked = false;
            Debug.Log("WC look end");
        }
    }
}
