using System;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Character
{
    [SerializeField] private GameObject[] _skins;

    [field: SerializeField] public EnemyAnimator Animator { get; private set; }
    [field: SerializeField] public NavMeshAgent NavMeshAgent { get; private set; }
    [field: SerializeField] public EnemyAttackDetector EnemyAttackDetector { get; private set; }
    [field: SerializeField] public float Damage { get; private set; } = 10;
    [field: SerializeField] public Health Health { get; private set; }
    [field: SerializeField] public Collider HitCollider { get; private set; }

    public Transform PlayerTransform { get; private set; }
    public EnemyStateMachine StateMachine { get; private set; }

    public event Action<Enemy> Dying;

    private void Update()
    {
        StateMachine.Update();
    }

    private void OnDestroy()
    {
        Dying = null;
    }

    public void SetPlayerTransform(Transform transform)
    {
        PlayerTransform = transform;
    }

    public void SetStateMachine(EnemyStateMachine stateMachine)
    {
        StateMachine = stateMachine;
    }

    public override void TakeDamage(float damage)
    {
        Health.SubtractHealth(damage);
    }

    public void OnHealthPointsOver()
    {
        StateMachine.ChangeState<EnemyStateDead>();
    }

    public void ReportDeath()
    {
        Dying?.Invoke(this);
    }

    internal void ChooseSkin()
    {
        for (int i = 0; i < _skins.Length; i++)
        {
            _skins[i].SetActive(false);
        }

        _skins[UnityEngine.Random.Range(0, _skins.Length)].SetActive(true);
    }
}
