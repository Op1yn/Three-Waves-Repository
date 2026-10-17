using System;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStateChase : EnemyState
{
    public EnemyStateChase(Enemy characters) : base(characters) { }

    public override void Enter()
    {
        Character.EnemyAttackDetector.PlayerDetected += SetStateAttack;
        Character.Animator.SetTrueIsMoving();
    }

    public override void Update()
    {
        Character.NavMeshAgent.SetDestination(Character.PlayerTransform.position);
    }

    private void SetStateAttack()
    {
        Character.StateMachine.ChangeState<EnemyStateAttack>();
    }

    public override void Exit()
    {
        Character.EnemyAttackDetector.PlayerDetected -= SetStateAttack;
        Character.Animator.SetFalseIsMoving();
    }
}
