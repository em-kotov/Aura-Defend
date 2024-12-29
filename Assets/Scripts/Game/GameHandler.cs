using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    //sound, aura stack, game end,  hero hit

    private void Start()
    {
        _startWaveWithDelayCoroutine = StartCoroutine(StartWaveWithDelay());
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

        while(_waveIndex<_enemiesCount.Length)
        {
            CreateWave(_waveIndex);
            _waveIndex++;
            yield return delay;
        }

        _startWaveWithDelayCoroutine = null;
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
