using System;
using UnityEngine;

public class Scene : MonoBehaviour
{
    [SerializeField] private PlayerSpawner _playerSpawner;
    [SerializeField] private ZombieSpawner _zombieSpawner;
    [SerializeField] private Transform _playerStartingPosition;
    [SerializeField] private BossSpawner _bossSpawner;

    [field: SerializeField] public WaveProgressor Progressor { get; private set; }

    private Player _player;
    private Boss _boss;

    public event Action VictoryWon;
    public event Action Defeated;

    private void OnDestroy()
    {
        Cleanup();
    }

    public void Initialize()
    {
        _player = _playerSpawner.CreatePlayer();
        _zombieSpawner.Initialize(_player.transform);
        _boss = _bossSpawner.CreateBoss(_player.transform);

        _zombieSpawner.Instantiated += OnZombieInstantiated;
        _zombieSpawner.Spawned += Progressor.OnZombieSpawned;
        Progressor.WaveStarted += OnWaveStarted;
        Progressor.WaveSpawnCompleted += _zombieSpawner.StopSpawn;
        Progressor.KillTargetMet += OnKillTargetMet;
        _boss.Dying += OnBossDying;

        _player.Dying += OnPlayerDying;
        Progressor.WavesOver += OnWavesOver;
    }

    public void StartScene()
    {
        _player.SetStartingParameters(_playerStartingPosition.position);
        Progressor.StartProgressAccount();
    }

    private void OnZombieInstantiated(Zombie zombie)
    {
        zombie.Dying += Progressor.OnKillingEnemy;
    }

    private void OnWaveStarted(int _)
    {
        _zombieSpawner.StartSpawn();
    }

    private void OnKillTargetMet()
    {
        _bossSpawner.Spawn(_boss);
    }

    private void OnBossDying(Character _)
    {
        Progressor.TrySwitchWave();
    }

    private void OnPlayerDying(Character _)
    {
        EndScene();
        Defeated?.Invoke();
        _boss.Respawn();
    }

    private void OnWavesOver()
    {
        EndScene();
        VictoryWon?.Invoke();
    }

    private void EndScene()
    {
        _player.DisableControl();
        _player.gameObject.SetActive(false);
        _zombieSpawner.StopSpawn();
        _zombieSpawner.ReturnAlltoPool();
    }

    private void Cleanup()
    {
        if (_player != null)
        {
            _player.Dying -= OnPlayerDying;
            _player.Dying -= Progressor.OnKillingEnemy;
        }

        if (_boss != null)
            _boss.Dying -= OnBossDying;

        _zombieSpawner.Instantiated -= OnZombieInstantiated;
        _zombieSpawner.Spawned -= Progressor.OnZombieSpawned;

        Progressor.WaveStarted -= OnWaveStarted;
        Progressor.WaveSpawnCompleted -= _zombieSpawner.StopSpawn;
        Progressor.KillTargetMet -= OnKillTargetMet;
        Progressor.WavesOver -= OnWavesOver;
    }
}
