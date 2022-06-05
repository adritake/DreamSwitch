using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EventLineUI : MonoBehaviour
{
    public Image CheckImage;
    public string EventName;
    public string EventId;
    public TextMeshProUGUI EventText;

    private void Start()
    {
        CheckImage.enabled = false;
    }

    public void CheckEvent()
    {
        CheckImage.enabled = true;
    }

    public void OnValidate()
    {
        EventText.text = EventName;
    }
}
