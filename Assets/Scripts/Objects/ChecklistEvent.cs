using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ChecklistEvent : Interactable
{
    public string EventName;

    private bool _completed;
    protected PlayerController _player;

    protected override void Start()
    {
        _player = FindObjectOfType<PlayerController>();
    }

    public override bool OnInteractBegin()
    {
        if (!_completed)
        {
            _completed = true;
            LevelChecklistManager.Instance.CompleteEvent(EventName);
            Debug.Log("Completed event : " + gameObject.name);
            return true;
        }
        else
        {
            return false;
        }
    }

    public override void OnLookedBegin()
    {
        if (!_completed)
        {
            base.OnLookedBegin();
        }
    }
}
