using Cinemachine;
using UnityEngine;

public class CinemachineShake : MonoBehaviour
{
    #region fields

    public static CinemachineShake Instance;
    private CinemachineFreeLook _cinemachineFreeLookCamera;
    private float _shakeTimer;
    private float _shakeTimerTotal;
    private float _startingIntensity;
    #endregion

    private void Awake()
    {
        Instance = this;
        _cinemachineFreeLookCamera = GetComponent<CinemachineFreeLook>();
    }

    public void ShakeCamera(float intensity, float time, float frequency)
    {
        _startingIntensity = intensity;
        _shakeTimer = time;
        _shakeTimerTotal = time;

        _cinemachineFreeLookCamera.GetRig(1).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = intensity;
        _cinemachineFreeLookCamera.GetRig(1).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = frequency;
    }

    void Update()
    {
        if (_shakeTimer > 0)
        {
            _shakeTimer -= Time.deltaTime;
            if (_shakeTimer <= 0)
            {
                _cinemachineFreeLookCamera.GetRig(1).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = Mathf.Lerp(_startingIntensity, 0f, 1 - (_shakeTimer/_shakeTimerTotal));
                _cinemachineFreeLookCamera.GetRig(1).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = Mathf.Lerp(_startingIntensity, 0f, 1 - (_shakeTimer/_shakeTimerTotal));
            }
        }
    }
}
