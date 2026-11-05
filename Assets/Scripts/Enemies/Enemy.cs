using UnityEngine;
using UnityEngine.AI;

public abstract class Enemy : Character
{
    [field: SerializeField] public EnemyAnimator Animator { get; private set; }
    [field: SerializeField] public NavMeshAgent NavMeshAgent { get; private set; }
    [field: SerializeField] public EnemyAttackDetector AttackDetector { get; private set; }
    [field: SerializeField] public float Damage { get; private set; } = 10;
    [field: SerializeField] public Health Health { get; private set; }
    [field: SerializeField] public Collider HitCollider { get; private set; }

    public Transform PlayerTransform { get; private set; }
    public EnemyStateMachine StateMachine { get; private set; }

    private void Update()
    {
        StateMachine.Update();
    }

    private void OnDestroy()
    {
        Health.HealthPointsOver -= SetStateDeath;
    }

    public void Initialize(EnemyStateMachine stateMachine, Transform transform)
    {
        StateMachine = stateMachine;
        PlayerTransform = transform;
        Health.HealthPointsOver += SetStateDeath;
        Animator.DeathAnimationComplete += ReportDeath;
    }

    public void Respawn()
    {
        AttackDetector.gameObject.SetActive(false);
        HitCollider.enabled = false;
        gameObject.SetActive(false);
    }

    public override void TakeDamage(float damage)
    {
        Health.SubtractHealth(damage);
    }

    private void SetStateDeath()
    {
        StateMachine.ChangeState<EnemyStateDead>();
    }
}
