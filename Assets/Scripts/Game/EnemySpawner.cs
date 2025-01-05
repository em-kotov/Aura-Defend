using UnityEngine;
using System.Collections.Generic;
using System;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private LinearFollower _linearFollower;
    [SerializeField] private AdaptiveFollower _adaptiveFollower;
    [SerializeField] private Hero _hero;
    [SerializeField] private float _alpha = 250;
    [SerializeField] private ParticleSystem _dieExplosion;
    [SerializeField] private AudioSource _enemyHitAudio;
    [SerializeField] private AudioSource _enemyDieAudio;

    [Header("Spawn Settings")]
    [SerializeField] private float _minDistanceFromHero = 10f;
    [SerializeField] private Vector2 _gridSize = new Vector2(30f, 16f);
    [SerializeField] private float _cellSize = 1f;
    [SerializeField] private float _positionZ = 7f;

    private List<Enemy> _currentEnemies = new List<Enemy>();

    public event Action EnemiesDied;

    public void Spawn(int count, List<Color> colors)
    {
        List<Vector2> availablePositions = GetAvailableSpawnPositions();

        for (int i = 0; i < count && availablePositions.Count > 0; i++)
        {
            // Get random position from pre-calculated available positions
            int randomIndex = UnityEngine.Random.Range(0, availablePositions.Count);

            Vector3 spawnPosition = new Vector3(
                availablePositions[randomIndex].x,
                availablePositions[randomIndex].y,
                _positionZ
            );

            CreateEnemy(spawnPosition, colors[i]);
            availablePositions.RemoveAt(randomIndex);
        }
    }

    private void CreateEnemy(Vector3 position, Color color)
    {
        Enemy enemy;

        int enemyType = GetRandomEnemyType();

        if (enemyType == 0)
        {
            enemy = Instantiate(_adaptiveFollower, position, Quaternion.identity);
        }
        else
        {
            enemy = Instantiate(_linearFollower, position, Quaternion.identity);
        }

        enemy.Died += OnDeath;
        enemy.Hit += OnHit;
        enemy.Initialize(color, _alpha, _hero);
        _currentEnemies.Add(enemy);
    }

    private int GetRandomEnemyType()
    {
        return UnityEngine.Random.Range(0, 2);
    }

    private void OnHit()
    {
        _enemyHitAudio.Play();
    }

    private void OnDeath(Vector3 position, Enemy enemy)
    {
        _currentEnemies.Remove(enemy);
        enemy.Died -= OnDeath;
        enemy.Hit -= OnHit;

        ParticleSystem effect = Instantiate(_dieExplosion, position, Quaternion.identity);
        effect.Play();
        _enemyDieAudio.Play();

        float duration = effect.main.duration;
        Destroy(effect.gameObject, duration);

        if(_currentEnemies.Count ==0)
        {
            EnemiesDied?.Invoke();
        }
    }

    private List<Vector2> GetAvailableSpawnPositions()
    {
        List<Vector2> positions = new List<Vector2>();
        Vector2 heroPosition = new Vector2(_hero.transform.position.x, _hero.transform.position.y);
        float minDistanceSqr = _minDistanceFromHero * _minDistanceFromHero;

        // Calculate grid boundaries
        int columnsCount = Mathf.CeilToInt(_gridSize.x / _cellSize);
        int rowsCount = Mathf.CeilToInt(_gridSize.y / _cellSize);

        // Start from center and expand outwards
        for (int x = -columnsCount / 2; x < columnsCount / 2; x++)
        {
            for (int y = -rowsCount / 2; y < rowsCount / 2; y++)
            {
                Vector2 position = new Vector2(x * _cellSize, y * _cellSize);

                // Check if position is far enough from hero
                if ((position - heroPosition).sqrMagnitude >= minDistanceSqr)
                {
                    positions.Add(position);
                }
            }
        }

        return positions;
    }
}
