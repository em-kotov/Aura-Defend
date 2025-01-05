using UnityEngine;
using System;

[RequireComponent(typeof(SpriteRenderer))]
public class Aura : MonoBehaviour
{
    public bool CanCollect { get; private set; } = true;
    public float Duration { get; private set; } = 5f;
    public float ShootDelay { get; private set; } = 1f;

    private SpriteRenderer _renderer;

    public event Action<Vector3, Aura> Collected;

    public void Deactivate()
    {
        CanCollect = false;
        Collected?.Invoke(transform.position, this);
        gameObject.SetActive(false);
    }

    public void Initialize(Color color)
    {
        _renderer = GetComponent<SpriteRenderer>();
        _renderer.color = color;
    }

    public Sprite GetSprite()
    {
        return _renderer.sprite;
    }

    public Color GetColor()
    {
        return _renderer.color;
    }
}
