using UnityEngine;

public class Subscriber : MonoBehaviour
{
    [SerializeField] private Hero _hero;
    [SerializeField] private GameHandler _gameHandler;
    [SerializeField] private CollisionRegister _collisionRegister;
    [SerializeField] private AuraSlot _auraSlot;
    [SerializeField] private EffectPlayer _effectPlayer;
    [SerializeField] private AudioPlayer _audioPlayer;

    private void OnEnable()
    {
        _collisionRegister.AuraFound += _hero.OnAuraFound;

        _hero.CollectedAura += _auraSlot.OnAuraCollected;

        _hero.Died += _gameHandler.OnHeroDeath;
        _hero.Died += _effectPlayer.OnHeroDie;

        _hero.Hit += _effectPlayer.OnHeroHit;

        _gameHandler.Won += _audioPlayer.OnWin;
    }

    private void OnDisable()
    {
        _collisionRegister.AuraFound -= _hero.OnAuraFound;

        _hero.CollectedAura -= _auraSlot.OnAuraCollected;

        _hero.Died -= _gameHandler.OnHeroDeath;
        _hero.Died -= _effectPlayer.OnHeroDie;

        _hero.Hit -= _effectPlayer.OnHeroHit;

        _gameHandler.Won -= _audioPlayer.OnWin;
    }
}
