using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraManager : MonoBehaviour
{
    public CinemachineVirtualCamera Camera;

    public void EnableHandHeldMovement(bool enable)
    {
        if (enable)
        {
            SetNoiseAmplitude(0.2f);
        }
        else
        {
            SetNoiseAmplitude(0);
        }
    }

    private void SetNoiseAmplitude(float amplitude)
    {
        Camera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = amplitude;
    }
}
