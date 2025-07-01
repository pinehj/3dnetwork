using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class CameraManager : Singleton<CameraManager>
{
    private CinemachineCamera _camera;
    private CinemachineBasicMultiChannelPerlin _cameraShake;
    private float _shakeDuration = 0.6f;
    private float _shakePower = 7f;
    protected override void Awake()
    {
        base.Awake();
        _camera = FindAnyObjectByType<CinemachineCamera>();
        _cameraShake = _camera.GetComponent<CinemachineBasicMultiChannelPerlin>();
    }

    public IEnumerator ShakeCamera()
    {
        _cameraShake.AmplitudeGain = _shakePower;

        // for shake duration
        while(_cameraShake.AmplitudeGain >= 0)
        {
            _cameraShake.AmplitudeGain -= Time.deltaTime * _shakePower / _shakeDuration;
            yield return null;
        }
    }
}
