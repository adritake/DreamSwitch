using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelChecklistManager : Singleton<LevelChecklistManager>
{
    public List<EventCheck> EventChecks;
    public EventChecklistUI EventCheckListUI;

    [Header("Debug")]
    public bool StartCompleted;

    public bool ListCompleted => _listCompleted;

    private bool _listCompleted;

    [Serializable]
    public class EventCheck
    {
        public string EventName;
        public bool Completed;
    }


    private void Start()
    {
        _listCompleted = StartCompleted;
    }

    private void Update()
    {
        CheckListCompleted();
    }

    private void CheckListCompleted()
    {
        if(EventChecks.All(x => x.Completed))
        {
            CompleteLevel();
        }
    }

    private void CompleteLevel()
    {
        if (!_listCompleted)
        {
            _listCompleted = true;
            Debug.Log("CHECKLIST COMPLETED!");
        }
    }

    public void CompleteEvent(string eventName)
    {
        EventChecks.First(x => x.EventName == eventName).Completed = true;
        EventCheckListUI.CompleteLine(eventName);
    }
}
