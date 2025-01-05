using System;
using UnityEngine;

public class Hero : MonoBehaviour
{
    [SerializeField] private Shoot _shoot;
    [SerializeField] private Movement _movement;
    [SerializeField] private Vector3 _startPosition = new Vector3(0f, 4f, 7f);

    private Aura _currentAura;
    private float _health = 3f;
    private float _hitPoints = 1f;

    public event Action<Aura> CollectedAura;
    public event Action<Vector3> Died;
    public event Action Hit;

    private void Start()
    {
        transform.position = _startPosition;
    }

    private void Update()
    {
        CheckHealth();
        _movement.Move();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out IEnemy enemy))
        {
            LooseHealth(_hitPoints);
        }
    }

    public void OnAuraFound(Aura aura)
    {
        if (aura.CanCollect)
        {
            _currentAura = aura;

            aura.Deactivate();

            CollectedAura?.Invoke(_currentAura);
            _shoot.StartShooting(_currentAura.Duration, _currentAura.ShootDelay, _currentAura.GetColor());
        }
    }

    private void Deactivate()
    {
        Died?.Invoke(transform.position);
        Collider collider = GetComponent<Collider>();
        collider.enabled = false;
        gameObject.SetActive(false);
        Debug.Log("you died");
    }

    private void LooseHealth(float points)
    {
       Hit?.Invoke();
        _health -= points;
    }

    private void CheckHealth()
    {
        if (_health <= 0)
        {
            Deactivate();
        }
    }
}
