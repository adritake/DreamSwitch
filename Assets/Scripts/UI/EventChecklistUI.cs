using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EventChecklistUI : MonoBehaviour
{
    private List<EventLineUI> _eventLines;

    private void Awake()
    {
        _eventLines = GetComponentsInChildren<EventLineUI>().ToList();
    }

    public void CompleteLine(string eventId)
    {
        _eventLines.First(x => x.EventId == eventId).CheckEvent();
    }
}
