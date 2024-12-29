using UnityEngine;
using System.Collections;

public class Shoot : MonoBehaviour
{
    [SerializeField] private EnemyDetector _enemyDetector;
    [SerializeField] private Bullet _bulletPrefab;
    [SerializeField] private float _shootSpeed = 10f;
    [SerializeField] private float _shootRadius = 20f;

    private Coroutine _shootingCoroutine;

    public void StartShooting(float duration, float delay, Color color)
    {
        if (_shootingCoroutine != null)
        {
            StopCoroutine(_shootingCoroutine);
        }

        _shootingCoroutine = StartCoroutine(StandardShoot(duration, delay, color));
    }

    private void ShootSingle(Color color)
    {
        Bullet bullet = Instantiate(_bulletPrefab, transform.position, Quaternion.identity);
        bullet.SetAuraColor(color);
        bullet.AddForce(GetShootingDirection() * _shootSpeed);
    }

    private IEnumerator StandardShoot(float duration, float delay, Color color)
    {
        float passedTime = 0f;
        WaitForSeconds waitBetweenShots = new WaitForSeconds(delay);

        while (passedTime < duration)
        {
            ShootSingle(color);
            passedTime += delay;
            yield return waitBetweenShots;
        }

        _shootingCoroutine = null;
    }

    private Vector3 GetShootingDirection()
    {
        Vector3 direction = Vector3.right;
        Enemy enemy = _enemyDetector.GetClosestEnemy(_shootRadius, transform.position);

        if (enemy != null)
        {
            direction = (enemy.transform.position - transform.position).normalized;
        }

        return direction;
    }
}
