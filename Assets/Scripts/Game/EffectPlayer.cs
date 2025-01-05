using UnityEngine;

public class EffectPlayer : MonoBehaviour
{
    [SerializeField] private ParticleSystem _heroDieExplosion;
    [SerializeField] private AudioSource _heroHitAudio;

    public void OnHeroHit()
    {
        _heroHitAudio.Play();
    }

    public void OnHeroDie(Vector3 position)
    {
        ParticleSystem effect = Instantiate(_heroDieExplosion, position, Quaternion.identity);
        effect.Play();

        float duration = effect.main.duration;
        Destroy(effect.gameObject, duration);
    }
}
