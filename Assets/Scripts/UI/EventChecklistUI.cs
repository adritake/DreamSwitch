using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class EventChecklistUI : MonoBehaviour
{
    public Image OpenBG;
    public Image OpenIcon;
    public CompletedIconUI CompletedIcon;
    public float OpenTime;

    private List<EventLineUI> _eventLines;
    private FinalGoalLineUI _goalLine;
    private bool _opened;
    private bool _isChecklistMoving;
    private RectTransform _recTransform;

    private void Awake()
    {
        _eventLines = GetComponentsInChildren<EventLineUI>().ToList();
        _goalLine = GetComponentInChildren<FinalGoalLineUI>();
        _recTransform = (RectTransform)transform;
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

        if (pressedOpen && !_isChecklistMoving)
        {
            _opened = !_opened;
            EnableCheckList(_opened);
        }
    }

    public void CompleteLine(string eventId)
    {
        _eventLines.First(x => x.EventId == eventId).CheckEvent();
        CompletedIcon.PlayCompleted();
    }

    public void EnableGoalLine()
    {
        _goalLine.EnableGoal(true);
    }

    private void EnableCheckList(bool enable)
    {
        _isChecklistMoving = true;
        float xSize = _recTransform.sizeDelta.x;
        float positionX = enable ? -xSize : xSize;
        float alpha = enable ? 0 : 1;
        float openIconTime = enable ? 0 : OpenTime;

        Debug.Log("PositionX: " + positionX);
        Debug.Log("Initial checklist position: " + transform.position);
        Sequence sequence = DOTween.Sequence();
        sequence.Append(_recTransform.DOAnchorPosX(positionX, OpenTime).SetEase(Ease.InOutQuad).SetRelative());
        sequence.Insert(openIconTime, OpenBG.DOColor(new Color(1, 1, 1, alpha * 0.5f), 0.1f));
        sequence.Insert(openIconTime, OpenIcon.DOColor(new Color(1, 1, 1, alpha ), 0.1f));
        sequence.AppendCallback(() => _isChecklistMoving = false);
        sequence.AppendCallback(() => Debug.Log("Final checklist position: " + transform.position));
        sequence.Play();
    }
}
