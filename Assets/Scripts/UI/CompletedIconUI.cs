using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CompletedIconUI : MonoBehaviour
{
    public Image Square;
    public Image Check;
    public float FadeTime;
    public int FadeTimes;

    private Tween _squareTween;
    private Tween _checkTween;

    private Color _transparent = new Color(1, 1, 1, 0);
    private Color _white = new Color(1, 1, 1, 1);


    private void Awake()
    {
        ResetColors();
    }

    public void PlayCompleted()
    {
        _squareTween.Kill();
        _checkTween.Kill();
        ResetColors();

        _squareTween = Square.DOColor(_white, FadeTime).SetEase(Ease.InOutQuart).SetLoops(FadeTimes, LoopType.Yoyo);
        _checkTween = Check.DOColor(_white, FadeTime).SetEase(Ease.InOutQuart).SetLoops(FadeTimes, LoopType.Yoyo);
        FMODUnity.RuntimeManager.PlayOneShotAttached("event:/Sfx/UI/TaskCheck", gameObject);
    }

    private void ResetColors()
    {
        Square.color = _transparent;
        Check.color = _transparent;
    }
}
