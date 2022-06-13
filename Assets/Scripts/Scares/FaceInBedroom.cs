using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FaceInBedroom : MonoBehaviour
{
    public GameObject face;

    Sequence seq;

    void Start()
    {
        seq = DOTween.Sequence();

        seq.Append(face.transform.DOMoveY(0.2f, 1.5f).SetEase(Ease.OutQuad).SetRelative())
            .AppendInterval(0.05f)
            .Append(face.transform.DOMoveY(-0.2f, 1.5f).SetEase(Ease.OutQuad).SetRelative())
            .AppendInterval(0.05f)
            .SetLoops(-1)
            .Play();
            
        Invoke("GoAway", 10);
    }

    void Update()
    {

    }

    void GoAway()
    {
        seq.Kill();

        face.transform.DOMoveX(-5, 2f).SetRelative().OnComplete(() => {
            Destroy(gameObject);
        });
    }
}
