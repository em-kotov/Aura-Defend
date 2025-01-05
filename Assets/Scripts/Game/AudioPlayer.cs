using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [SerializeField] private AudioSource _calmBackgroundMusic;
    [SerializeField] private AudioSource _winSound;
    [SerializeField] private AudioSource _finishMusic;
    [SerializeField] private ParticleSystem _winConfetti;

    private void Awake()
    {
        _calmBackgroundMusic.Play();
    }

    public void OnWin()
    {
        _calmBackgroundMusic.Stop();
        _winSound.Play();
        _winConfetti.Play();
        _finishMusic.Play();
    }
}
