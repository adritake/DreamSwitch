using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class Eyelids : MonoBehaviour
{
    public Image UpperEyeLid;
    public Image LowerEyeLid;

    public float CloseTime;
    public int Displacement = 1080;

    [ContextMenu("Close eyes")]
    public void CloseEyes(bool instant = false)
    {
        float time = instant ? 0 : CloseTime;
        UpperEyeLid.rectTransform.DOMove(new Vector3(UpperEyeLid.transform.position.x, Displacement - Displacement / 6, 0), time).SetEase(Ease.InOutQuad);
        LowerEyeLid.rectTransform.DOMove(new Vector3(LowerEyeLid.transform.position.x, Displacement / 6, 0), time).SetEase(Ease.InOutQuad);
    }

    [ContextMenu("Open eyes")]
    public void OpenEyes()
    {
        UpperEyeLid.rectTransform.DOMove(new Vector3(UpperEyeLid.transform.position.x, Displacement + Displacement / 2, 0), CloseTime).SetEase(Ease.InQuad);
        LowerEyeLid.rectTransform.DOMove(new Vector3(LowerEyeLid.transform.position.x, -Displacement / 2, 0), CloseTime).SetEase(Ease.InQuad);
    }
}
