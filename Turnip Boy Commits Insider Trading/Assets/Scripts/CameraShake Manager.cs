using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using static Unity.Burst.Intrinsics.X86.Avx;

public class CameraShakeManager : MonoBehaviour
{

    private CinemachineVirtualCamera _cam;
    private float timer;

    private void Awake()
    {
        _cam = GetComponent<CinemachineVirtualCamera>();
    }

    // Start is called before the first frame update
    void Start()
    {
        StopShake();
    }

    public void ShakeCamera(float timeGiven, float intensity)
    {
        CinemachineBasicMultiChannelPerlin _cmbp = _cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        _cmbp.m_AmplitudeGain= intensity;
        timer = timeGiven;
    }
    public void StopShake()
    {
        CinemachineBasicMultiChannelPerlin _cmbp = _cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        timer = 0;
        _cmbp.m_AmplitudeGain = 0;
    }

    public void Update()
    {
        if(timer > 0)
        {
            timer -= Time.deltaTime;
            if(timer < 0)
            {
                StopShake();
            }
        }
    }

}
