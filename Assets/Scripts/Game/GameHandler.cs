using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameHandler : MonoBehaviour
{
    [SerializeField] private AuraSpawner _auraSpawner;
    [SerializeField] private EnemySpawner _enemySpawner;
    [SerializeField] private ColorBook _colorBook;
    [SerializeField] private float _waveDelay = 18f;

    [SerializeField]
    private int[] _aurasCount = new int[]{
        3,
        5,
        8,
        13,
        21
    };

    [SerializeField]
    private int[] _enemiesCount = new int[]{
        2,
        3,
        5,
        8,
        13
    };

    private int _waveIndex = 0;
    private Coroutine _startWaveWithDelayCoroutine;

    public event Action Won;

    private void Start()
    {
        _enemySpawner.EnemiesDied += OnEnemiesDied;
        _startWaveWithDelayCoroutine = StartCoroutine(StartWaveWithDelay());
    }

    public void OnHeroDeath(Vector3 position)
    {
        StartCoroutine(ReloadWithDelay(2.5f));
    }

    private void CreateWave(int waveIndex)
    {
        int aurasCount = _aurasCount[waveIndex];
        _auraSpawner.Spawn(aurasCount, GetColors(aurasCount));

        int enemiesCount = _enemiesCount[waveIndex];
        _enemySpawner.Spawn(enemiesCount, GetColors(enemiesCount));
    }

    private IEnumerator StartWaveWithDelay()
    {
        WaitForSeconds delay = new WaitForSeconds(_waveDelay);

        while (_waveIndex < _enemiesCount.Length)
        {
            CreateWave(_waveIndex);
            _waveIndex++;
            yield return delay;
        }

        _startWaveWithDelayCoroutine = null;
    }

    private void OnEnemiesDied()
    {
        if (_waveIndex >= _enemiesCount.Length)
        {
            Won?.Invoke();
            _enemySpawner.EnemiesDied -= OnEnemiesDied;
        }
    }

    private IEnumerator ReloadWithDelay(float seconds)
    {
        WaitForSeconds delay = new WaitForSeconds(seconds);
        yield return delay;

        _enemySpawner.EnemiesDied -= OnEnemiesDied;

        SceneManager.LoadScene("Aura Defend");
    }

    private List<Color> GetColors(int count)
    {
        List<Color> colors = new List<Color>(count);

        for (int i = 0; i < count; i++)
        {
            colors.Add(_colorBook.GetRandomColor());
        }

        return colors;
    }
}
