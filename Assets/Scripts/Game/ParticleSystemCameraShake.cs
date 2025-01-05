using UnityEngine;

public class ParticleSystemCameraShake : MonoBehaviour
{
    [SerializeField] private float shakeDuration = 0.1f;
    [SerializeField] private float shakeMagnitude = 0.1f;
    
    private ParticleSystem _particleSystem;
    private Camera _mainCamera;
    private Vector3 originalCameraPos;
    private float currentShakeDuration;
    private bool isShaking = false;

    private void Start()
    {
        _particleSystem = GetComponent<ParticleSystem>();
        _mainCamera = Camera.main;
        originalCameraPos = _mainCamera.transform.localPosition;
    }

    private void Update()
    {
        // Check if particle system just started playing
        if (_particleSystem.isPlaying && !isShaking)
        {
            StartShake();
        }

        // Handle shake effect
        if (isShaking)
        {
            if (currentShakeDuration > 0)
            {
                _mainCamera.transform.localPosition = originalCameraPos + Random.insideUnitSphere * shakeMagnitude;
                currentShakeDuration -= Time.deltaTime;
            }
            else
            {
                isShaking = false;
                currentShakeDuration = 0f;
                _mainCamera.transform.localPosition = originalCameraPos;
            }
        }
    }

    private void StartShake()
    {
        isShaking = true;
        currentShakeDuration = shakeDuration;
    }
}