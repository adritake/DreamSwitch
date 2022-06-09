using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radio : ChecklistEvent
{

    public float songBPM;

    FMODUnity.StudioEventEmitter radioSong;

    Sequence seqX;
    Sequence seqY;

    float beatToSec;

    protected override void Start()
    {

        float beatToMs = 60000 / songBPM;
        beatToSec = beatToMs / 1000;

        seqX = DOTween.Sequence();
        seqY = DOTween.Sequence();


        seqX.Append(transform.DOScaleX(1.2f, beatToSec * 0.4f).SetEase(Ease.Flash))
            .Append(transform.DOScaleX(1f, beatToSec * 0.6f).SetEase(Ease.Linear))
            .SetLoops(-1).Pause();

        seqY.Append(transform.DOScaleY(0.8f, beatToSec * 0.4f).SetEase(Ease.Flash))
            .Append(transform.DOScaleY(1f, beatToSec * 0.6f).SetEase(Ease.Linear))
            .SetLoops(-1).Pause();


        radioSong = GetComponent<FMODUnity.StudioEventEmitter>();
    }

    public override bool CompleteEvent()
    {

        if(!radioSong.IsPlaying())
        {
            radioSong.Play();
            FMODUnity.RuntimeManager.PlayOneShotAttached("event:/Sfx/Loop1/AlarmTurnOff", gameObject);
            seqX.Play();
            seqY.Play();
        }
        else
        {
            radioSong.Stop();
            FMODUnity.RuntimeManager.PlayOneShotAttached("event:/Sfx/Loop1/AlarmTurnOff", gameObject);
            seqX.Pause();
            seqY.Pause();
            transform.DOScaleX(1f, beatToSec * 0.6f).SetEase(Ease.Linear);
            transform.DOScaleY(1f, beatToSec * 0.6f).SetEase(Ease.Linear);
        }

        return true;
    }
}
