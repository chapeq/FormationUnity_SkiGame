using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraShake : MonoBehaviour
{

    public float intensity;
    public float time;

    private CinemachineVirtualCamera cinemaCamera;
    private float ShakeTimer;

    // Start is called before the first frame update
    void Awake()
    {
        cinemaCamera = GetComponent<CinemachineVirtualCamera>();
    }

    private void OnEnable()
    {
        PlayerEvents.OnHit += ShakeCamera;
    }
    private void OnDisable()
    {
        PlayerEvents.OnHit -= ShakeCamera;
    }
    // Update is called once per frame
    void Update()
    {
        if (ShakeTimer > 0)
        {
            ShakeTimer -= Time.deltaTime;
        }

        if (ShakeTimer <= 0f)
        {
            CinemachineBasicMultiChannelPerlin cm = cinemaCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
            cm.m_AmplitudeGain = 0;
        }
    }

    public void ShakeCamera()
    {
        CinemachineBasicMultiChannelPerlin cm = cinemaCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        cm.m_AmplitudeGain = intensity;
        ShakeTimer = time;
    }

}
