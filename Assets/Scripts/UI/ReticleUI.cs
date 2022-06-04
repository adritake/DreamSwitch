using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReticleUI : Singleton<ReticleUI>
{
    public Image Reticle;

    public void EnableReticle(bool enable)
    {
        Reticle.enabled = enable;
    }
}
