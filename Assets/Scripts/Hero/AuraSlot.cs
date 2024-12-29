using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SpriteRenderer))]
public class AuraSlot : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private Coroutine _delayedDeactivationCoroutine;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        Deactivate();
    }

    public void OnAuraCollected(Aura aura)
    {
        _spriteRenderer.enabled = true;
        _spriteRenderer.color = aura.GetColor();
        StartSpriteTimer(aura.Duration);
    }

    private void Deactivate()
    {
        _spriteRenderer.enabled = false;
    }

    private void StartSpriteTimer(float seconds)
    {
        if (_delayedDeactivationCoroutine != null)
        {
            StopCoroutine(_delayedDeactivationCoroutine);
        }

        _delayedDeactivationCoroutine = StartCoroutine(DelayedDeactivation(seconds));
    }

    private IEnumerator DelayedDeactivation(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Deactivate();

        _delayedDeactivationCoroutine = null;
    }
}
