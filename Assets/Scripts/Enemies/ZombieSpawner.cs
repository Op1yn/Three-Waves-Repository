using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ZombieSpawner : MonoBehaviour
{
    [SerializeField] private int _poolCapacity;
    [SerializeField] private int _poolMaximumSize;
    [SerializeField] private Zombie _sample;
    [SerializeField] private float _spawnRate;
    [SerializeField] private Transform[] _spawnPoints;

    protected ObjectPool<Zombie> _pool;

    private List<Zombie> _activeZombies;
    private Transform _playerTransform;
    private Coroutine _startTimerCoroutine;
    private WaitForSeconds waiting;

    public event Action<Zombie> Instantiated;
    public event Action Spawned;

    private void OnDestroy()
    {
        StopSpawn();
        _pool?.Dispose();
    }

    public void Initialize(Transform playerTransform)
    {
        _activeZombies = new List<Zombie>(_poolMaximumSize);
        _playerTransform = playerTransform;
        waiting = new WaitForSeconds(_spawnRate);

        _pool = new ObjectPool<Zombie>(
            createFunc: InstantiateZombie,
            actionOnGet: zombie => zombie.gameObject.SetActive(true),
            actionOnRelease: zombie => zombie.Respawn(),
            actionOnDestroy: zombie => { if (zombie == null) return; zombie.Dying -= ReturnToPool; Destroy(zombie.gameObject); },
            true, _poolCapacity, _poolMaximumSize);
    }

    public void StartSpawn()
    {
        if (_startTimerCoroutine != null)
            StopCoroutine(_startTimerCoroutine);

        _startTimerCoroutine = StartCoroutine(Spawn());
    }

    public void StopSpawn()
    {
        if (_startTimerCoroutine != null)
        {
            StopCoroutine(_startTimerCoroutine);
            _startTimerCoroutine = null;
        }
    }

    public void ReturnAlltoPool()
    {
        for (int i = _activeZombies.Count - 1; i >= 0; i--)
        {
            ReturnToPool(_activeZombies[i]);
        }
    }

    protected virtual Zombie InstantiateZombie()
    {
        Zombie zombie = Instantiate(_sample);
        zombie.Initialize(GetZombieStateMachine(zombie), _playerTransform);
        zombie.Dying += ReturnToPool;

        Instantiated?.Invoke(zombie);

        return zombie;
    }

    private IEnumerator Spawn()
    {
        while (enabled)
        {
            yield return waiting;

            Zombie zombie = _pool.Get();
            zombie.transform.position = _spawnPoints[UnityEngine.Random.Range(0, _spawnPoints.Length)].transform.position;
            zombie.Health.SetMaximumHealthPoints();
            zombie.ChooseSkin();
            zombie.AttackDetector.gameObject.SetActive(true);
            zombie.HitCollider.enabled = true;
            zombie.NavMeshAgent.isStopped = false;
            zombie.StateMachine.ChangeState<EnemyStateRunning>();
            _activeZombies.Add(zombie);

            Spawned?.Invoke();
        }
    }

    private EnemyStateMachine GetZombieStateMachine(Zombie zombie)
    {
        EnemyStateMachine stateMachine = new EnemyStateMachine();
        stateMachine.AddState(new EnemyStateRunning(zombie));
        stateMachine.AddState(new EnemyStateAttack(zombie));
        stateMachine.AddState(new EnemyStateDead(zombie));

        return stateMachine;
    }

    private void ReturnToPool(Character character)
    {
        if (character is Zombie zombie && _activeZombies.Contains(zombie))
        {
            _pool.Release(zombie);
            _activeZombies.Remove(zombie);
        }
    }
}
