using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class EventChecklistUI : MonoBehaviour
{
    public Image OpenImage;
    public float OpenTime;
    public float ClosedX = 700;
    public float OpenedX = -700;

    private List<EventLineUI> _eventLines;
    private bool _opened;

    private void Awake()
    {
        _eventLines = GetComponentsInChildren<EventLineUI>().ToList();
    }


    private void Update()
    {
        CheckOpenChecklist();
    }

    private void CheckOpenChecklist()
    {
        bool pressedOpen;

        #if !UNITY_EDITOR && UNITY_SWITCH
            pressedOpen = InputSystem.Instance.switchButtons.Minus;
        #else
            pressedOpen = Input.GetKeyDown(KeyCode.Q);
        #endif

        if (pressedOpen)
        {
            _opened = !_opened;
            EnableCheckList(_opened);
        }
    }

    public void CompleteLine(string eventId)
    {
        _eventLines.First(x => x.EventId == eventId).CheckEvent();
    }

    private void EnableCheckList(bool enable)
    {
        float positionX = enable ? OpenedX : ClosedX;
        float alpha = enable ? 0 : 1;
        float openIconTime = enable ? 0 : OpenTime;

        Sequence sequence = DOTween.Sequence();
        sequence.Append(transform.DOMove(new Vector3(transform.position.x + positionX, transform.position.y, transform.position.z), OpenTime).SetEase(Ease.InOutQuad));
        sequence.Insert(openIconTime, OpenImage.DOColor(new Color(1, 1, 1, alpha), 0.1f));
        sequence.Play();
    }
}
