using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using static UnityEngine.EventSystems.EventTrigger;

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

    public event Action<Enemy> EnemyCreated;

    void Start()
    {
        _waiting = new WaitForSeconds(_spawnRate);
    }

    private void OnDisable()
    {
        StopSpawn();
    }

    public void SetPlayerTransform(Transform transform)
    {
        _playerTransform = transform;
    }

    public ObjectPool<Enemy> InitializeObjectPool()
    {
        Pool = new ObjectPool<Enemy>(
            createFunc: () => InstantiateObject(),
            actionOnGet: enemy => enemy.gameObject.SetActive(true),
            actionOnRelease: enemy => enemy.gameObject.SetActive(false),
            actionOnDestroy: enemy => Destroy(enemy),
            collectionCheck: true,
            defaultCapacity: _poolCapacity,
            maxSize: _poolMaximumSize
            );

        return Pool;
    }

    public void StartSpawn()
    {
        _startTimerCoroutine = StartCoroutine(SpawnEnemies());
    }

    public Enemy Spawn()
    {
        Enemy enemy = Pool.Get();
        enemy.SetPlayerTransform(_playerTransform);
        EnemyCreated?.Invoke(enemy);
        _activeObjects.Add(enemy);

        return enemy;
    }

    public void ReturnToPool(Enemy enemy)
    {
        if (_activeObjects.Contains(enemy))
        {
            Pool.Release(enemy);
            _activeObjects.Remove(enemy);
        }
    }

    public void ReturnAllToPool()
    {
        for (int i = _activeObjects.Count - 1; i >= 0; i--)
        {
            ReturnToPool(_activeObjects[i]);
        }
    }

    public void KillAll()
    {
        for (int i = _activeObjects.Count - 1; i >= 0; i--)
        {
            _activeObjects[i].TakeDamage(_activeObjects[i].Health.MaximumAmount);
        }
    }

    public void StopSpawn()
    {
        if (_startTimerCoroutine != null)
            StopCoroutine(_startTimerCoroutine);
    }

    private Enemy InstantiateObject()
    {
        Enemy enemy = Instantiate(_sample);
        InitializeEnemy(enemy);
        enemy.Dying += ReturnToPool;
        enemy.Animator.DeathAnimationComplete += enemy.ReportDeath;

        return enemy;
    }

    private void InitializeEnemy(Enemy enemy)
    {
        InitializeStateMachine(enemy);
    }

    private IEnumerator SpawnEnemies()
    {
        while (enabled)
        {
            Enemy enemy = Spawn();
            SetPosition(enemy.transform);
            enemy.ChooseSkin();
            enemy.EnemyAttackDetector.gameObject.SetActive(true);
            enemy.HitCollider.enabled = true;
            enemy.Health.SetMaximumHealthPoints();
            enemy.StateMachine.ChangeState<EnemyStateIdle>();

            yield return _waiting;
        }
    }

    private void SetPosition(Transform transform)
    {
        transform.position = _spawnPoints[UnityEngine.Random.Range(0, _spawnPoints.Length)].position;
    }

    private void InitializeStateMachine(Enemy enemy)
    {
        enemy.SetStateMachine(new EnemyStateMachine());

        enemy.StateMachine.AddState(new EnemyStateIdle(enemy));
        enemy.StateMachine.AddState(new EnemyStateChase(enemy));
        enemy.StateMachine.AddState(new EnemyStateAttack(enemy));
        enemy.StateMachine.AddState(new EnemyStateDead(enemy));

        enemy.Health.HealthPointsOver += enemy.OnHealthPointsOver;
    }
}
