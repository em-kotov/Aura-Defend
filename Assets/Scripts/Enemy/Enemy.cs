using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider), typeof(SpriteRenderer))]
public class Enemy : MonoBehaviour, IEnemy
{
    protected float Speed = 1f;
    protected Hero Hero;

    private SpriteRenderer _renderer;
    private float _health = 3f;
    private float _hitPoints = 1f;

    public Color AuraColor { get; private set; }

    public event Action<Vector3, Enemy> Died;
    public event Action Hit;

    virtual protected void Update()
    {
        CheckHealth();

        transform.position = Vector3.MoveTowards(transform.position, Hero.transform.position, Speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Bullet bullet))
        {
            if (bullet.AuraColor != AuraColor)
            {
                bullet.Deactivate();
                LooseHealth(_hitPoints);
            }
        }
    }

    virtual public void Initialize(Color color, float alpha, Hero hero)
    {
        _renderer = GetComponent<SpriteRenderer>();
        AuraColor = color;
        _renderer.color = SetAlpha(color, alpha);

        Hero = hero;
    }

    protected void CheckHealth()
    {
        if (_health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // _renderer.enabled = false;
        Died?.Invoke(transform.position, this);
        gameObject.SetActive(false);
    }

    private void LooseHealth(float points)
    {
        _health -= points;

        if (_health > 0)
        {
            Hit?.Invoke();
        }
    }

    private Color SetAlpha(Color color, float alpha)
    {
        Color newColor = color;
        newColor.a = alpha;
        return newColor;
    }
}
