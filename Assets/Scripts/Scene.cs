using System;
using UnityEngine;
using UnityEngine.Pool;

public class Scene : MonoBehaviour
{
    [SerializeField] private PlayerInitializer _playerInitializer;
    [SerializeField] private EnemySpawner _enemySpawner;
    [SerializeField] private GameObject _hud;
    [SerializeField] private Progressor _progressor;

    private ObjectPool<Enemy> _enemiesPool;
    //private int _zombiesSpawnedCount;

    public event Action GameOver;

    public void InitializeScene()
    {
        //_zombiesSpawnedCount = 0;

        Player player = _playerInitializer.CreatePlayer();
        player.Dying += EndScene;
        _progressor.ResetCounter();
        _progressor.KillTargetMet += _enemySpawner.StopSpawn;
        _progressor.KillTargetMet += _enemySpawner.KillAll;
        _progressor.KillTargetMet += ReleaseBoss;
        _enemySpawner.EnemyCreated += OnKillingEnemy;
        GameOver += _enemySpawner.StopSpawn;
        GameOver += _enemySpawner.ReturnAllToPool;

        InitializeEnemySpawner(player);

        _hud.SetActive(true);
    }

    public void EndScene()
    {
        GameOver?.Invoke();
        _enemySpawner.EnemyCreated -= OnKillingEnemy;
        _progressor.KillTargetMet -= _enemySpawner.StopSpawn;
        _progressor.KillTargetMet -= _enemySpawner.KillAll;
        _progressor.KillTargetMet -= ReleaseBoss;
        GameOver -= _enemySpawner.StopSpawn;
        GameOver -= _enemySpawner.ReturnAllToPool;

        _hud.SetActive(false);
    }

    private void OnKillingEnemy(Enemy enemy)
    {
        enemy.Dying += _progressor.OnKillingEnemy;
    }

    private void InitializeEnemySpawner(Player player)
    {
        _enemySpawner.SetPlayerTransform(player.transform);

        if (_enemiesPool == null)
        {
            _enemiesPool = _enemySpawner.InitializeObjectPool();
        }

        _enemySpawner.StartSpawn();
    }

    private void ReleaseBoss()
    {

    }
}
