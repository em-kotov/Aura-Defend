using UnityEngine;

public class Subscriber : MonoBehaviour
{
    [SerializeField] private Hero _hero;
    [SerializeField] private CollisionRegister _collisionRegister;
    [SerializeField] private AuraSlot _auraSlot;

    private void OnEnable()
    {
        _collisionRegister.AuraFound += _hero.OnAuraFound;

        _hero.CollectedAura += _auraSlot.OnAuraCollected;
    }

    private void OnDisable()
    {
        _collisionRegister.AuraFound -= _hero.OnAuraFound;

        _hero.CollectedAura -= _auraSlot.OnAuraCollected;
    }
}
