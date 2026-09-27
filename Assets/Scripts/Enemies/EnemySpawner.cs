using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Enemy _sample;
    [SerializeField] private float _spawnRate = 1f;
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private int _poolCapacity = 5;
    [SerializeField] private int _poolMaximumSize = 10;

    private List<Enemy> _activeObjects = new List<Enemy>();
    private Transform _playerTransform;
    private Coroutine _startTimerCoroutine;
    private WaitForSeconds _waiting;

    public ObjectPool<Enemy> Pool { get; private set; }


    void Start()
    {
        _waiting = new WaitForSeconds(_spawnRate);
    }

    public void SetPlayerTransform(Transform transform)
    {
        _playerTransform = transform;
    }

    public void InitializeObjectPool()
    {
        Pool = new ObjectPool<Enemy>(
            createFunc: () => InstantiateObject(),
            actionOnGet: projectile => projectile.gameObject.SetActive(true),
            actionOnRelease: projectile => projectile.gameObject.SetActive(false),
            actionOnDestroy: projectile => Destroy(projectile),
            collectionCheck: true,
            defaultCapacity: _poolCapacity,
            maxSize: _poolMaximumSize
            );
    }

    public void StartSpawn()
    {
        _startTimerCoroutine = StartCoroutine(SpawnEnemies());
    }

    public void StopSpawn()
    {
        if (_startTimerCoroutine != null)
            StopCoroutine(_startTimerCoroutine);
    }

    public Enemy Spawn()
    {
        Enemy enemy = Pool.Get();
        _activeObjects.Add(enemy);

        return enemy;
    }

    private Enemy InstantiateObject()
    {
        Enemy enemy = Instantiate(_sample);
        InitializeEnemy(enemy);

        return enemy;
    }

    private void InitializeEnemy(Enemy enemy)
    {
        enemy.SetPlayerTransform(_playerTransform);
    }

    private IEnumerator SpawnEnemies()
    {
        while (enabled)
        {
            Enemy enemy = Spawn();
            SetPosition(enemy.transform);
            enemy.ChooseSkin();
            enemy.StateMachine.ChangeState<EnemyStateIdle>();

            yield return _waiting;
        }
    }

    private void SetPosition(Transform transform)
    {
        transform.position = _spawnPoints[UnityEngine.Random.Range(0, _spawnPoints.Length)].position;
    }
}
