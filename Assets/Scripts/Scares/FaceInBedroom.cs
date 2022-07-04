using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FaceInBedroom : MonoBehaviour
{
    public Renderer Shadow;
    public Renderer Deform;
    public float VanishTime;
    public float VanishDuration;

   
    void Start()
    {
        Invoke(nameof(GoAway), VanishTime);
    }

    void GoAway()
    {
        Shadow.material.DOFloat(0, "_Alpha", VanishDuration);
        Deform.material.DOFloat(0, "_Alpha", VanishDuration);
    }
}
