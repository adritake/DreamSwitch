using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shower : MonoBehaviour, IInteractable, IChecklistEvent
{
    public float ShowerTime;
    public ParticleSystem ShowerVFX;
    private PlayerController _player;

    private bool _looked;

    private void Start()
    {
        _player = FindObjectOfType<PlayerController>();
    }

    public void CompleteEvent()
    {
        _player.CanMove = false;
        ShowerVFX.Play();
        Invoke(nameof(EndEvent), ShowerTime);
    }

    private void EndEvent()
    {
        _player.CanMove = true;
        ShowerVFX.Stop();
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
            Debug.Log("Shower look begin");
        }
    }

    public void OnLookedEnd()
    {
        if (_looked)
        {
            _looked = false;
            Debug.Log("Shower look begin");
        }
    }
}
