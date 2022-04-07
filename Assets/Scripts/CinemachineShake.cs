using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemachineShake : MonoBehaviour
{

    //cached component refernces
    CinemachineVirtualCamera _cinemachineVirtualCamera;

    //state
    private float _shakeTimer;

    // Start is called before the first frame update
    void Start()
    {
        _cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
    }

    // Update is called once per frame
    void Update()
    {
        if(_shakeTimer> 0)
        {
            _shakeTimer -= Time.deltaTime;
            if (_shakeTimer <= 0f)
            {
                var _cinemachineNoise = _cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
                _cinemachineNoise.m_AmplitudeGain = 0f;
            }
        }
    }

    public void ShakeCamera(float intensity, float time)
    {
        var _cinemachineNoise = _cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        _cinemachineNoise.m_AmplitudeGain = intensity;
        _shakeTimer = time;
    }
}
