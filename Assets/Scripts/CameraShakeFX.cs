using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraShakeFX : MonoBehaviour
{


    [SerializeField] private CinemachineImpulseSource impulse;

    public void ScreenShake(int facingDir)
    {
        impulse.m_DefaultVelocity = new Vector3(1 * facingDir, 1);
        impulse.GenerateImpulse();
    }


}
