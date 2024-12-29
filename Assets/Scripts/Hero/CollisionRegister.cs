using UnityEngine;
using System;

[RequireComponent(typeof(Collider))]
public class CollisionRegister : MonoBehaviour
{
    public event Action<Aura> AuraFound;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.TryGetComponent(out Aura aura))
        {
            AuraFound?.Invoke(aura);
        }
    }
}
