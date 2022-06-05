using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FinalGoalLineUI : MonoBehaviour
{
    public Color EnabledColor;
    public Color DisabledColor;
    public TextMeshProUGUI GoalText;
    public Image SquareImage;

    private void Awake()
    {
        EnableGoal(false);
    }

    public void EnableGoal(bool enable)
    {
        Color color = enable ? EnabledColor : DisabledColor;

        GoalText.color = color;
        SquareImage.color = color;
    }
}
