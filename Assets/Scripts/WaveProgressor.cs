using System;
using UnityEngine;

public class WaveProgressor : MonoBehaviour
{
    [SerializeField] private int[] _waveKillGoal;

    private int _spawnedCount;
    private int _killCount;
    private int _currentWaveNumber;

    public event Action WaveSpawnCompleted;
    public event Action KillTargetMet;
    public event Action<int> WaveStarted;
    public event Action WavesOver;

    public void StartProgressAccount()
    {
        _currentWaveNumber = 0;
        EnsureWavesFilled();
        StartWave();
    }

    public void StartWave()
    {
        WaveStarted?.Invoke(_currentWaveNumber);
        _spawnedCount = 0;
        _killCount = 0;
    }

    public void OnKillingEnemy(Character _)
    {
        _killCount++;

        if (_killCount >= _waveKillGoal[_currentWaveNumber])
        {
            KillTargetMet?.Invoke();
        }
    }

    public void OnZombieSpawned()
    {
        _spawnedCount++;

        if (_spawnedCount >= _waveKillGoal[_currentWaveNumber])
        {
            WaveSpawnCompleted?.Invoke();
        }
    }

    public void TrySwitchWave()
    {
        _currentWaveNumber++;

        if (_currentWaveNumber < _waveKillGoal.Length)
        {
            StartWave();
        }
        else
        {
            WavesOver?.Invoke();
        }
    }

    private void EnsureWavesFilled()
    {
        for (int i = 0; i < _waveKillGoal.Length; i++)
        {
            if (_waveKillGoal[i] < 1)
            {
                _waveKillGoal[i] = 1;
            }
        }
    }
}
