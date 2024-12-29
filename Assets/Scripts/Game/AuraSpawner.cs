using System.Collections.Generic;
using UnityEngine;

public class AuraSpawner : MonoBehaviour
{
    [SerializeField] private Aura _auraPrefab;
    [SerializeField] private Hero _hero;

    [Header("Spawn Settings")]
    [SerializeField] private float _minDistanceFromHero = 5f;
    [SerializeField] private Vector2 _gridSize = new Vector2(30f, 16f);
    [SerializeField] private float _cellSize = 1f;
    [SerializeField] private float _positionZ = 7f;

    public void Spawn(int count, List<Color> colors)
    {
        List<Vector2> availablePositions = GetAvailableSpawnPositions();

        for (int i = 0; i < count && availablePositions.Count > 0; i++)
        {
            // Get random position from pre-calculated available positions
            int randomIndex = Random.Range(0, availablePositions.Count);

            Vector3 spawnPosition = new Vector3(
                availablePositions[randomIndex].x,
                availablePositions[randomIndex].y,
                _positionZ
            );

            CreateSingle(spawnPosition, colors[i]);
            availablePositions.RemoveAt(randomIndex);
        }
    }

    private void CreateSingle(Vector3 position, Color color)
    {
        Aura aura = Instantiate(_auraPrefab, position, Quaternion.identity);
        aura.Initialize(color);
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

    //Visualize spawn area in editor
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(Vector3.zero, new Vector3(_gridSize.x, _gridSize.y, 0));

        if (_hero != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(_hero.transform.position, _minDistanceFromHero);
        }
    }
}
