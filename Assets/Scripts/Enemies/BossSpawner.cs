using UnityEngine;

public class BossSpawner : MonoBehaviour
{
    [SerializeField] private Boss _sample;
    [SerializeField] private Transform[] _spawnPoints;

    public Boss Boss { get; private set; }

    public Boss CreateBoss(Transform playerTransform)
    {
        Boss = Instantiate(_sample);
        Boss.gameObject.SetActive(false);
        Boss.Initialize(GetStateMachine(Boss), playerTransform);

        return Boss;
    }

    public void Spawn(Boss boss)
    {
        boss.gameObject.SetActive(true);
        boss.transform.position = _spawnPoints[UnityEngine.Random.Range(0, _spawnPoints.Length)].position;
        boss.Health.SetMaximumHealthPoints();
        boss.AttackDetector.gameObject.SetActive(true);
        boss.HitCollider.enabled = true;
        boss.NavMeshAgent.isStopped = false;
        boss.StateMachine.ChangeState<EnemyStateRunning>();
    }

    private EnemyStateMachine GetStateMachine(Boss boss)
    {
        EnemyStateMachine stateMachine = new EnemyStateMachine();
        stateMachine.AddState(new EnemyStateRunning(boss));
        stateMachine.AddState(new EnemyStateAttack(boss));
        stateMachine.AddState(new EnemyStateDead(boss));

        return stateMachine;
    }
}
