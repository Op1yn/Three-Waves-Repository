using UnityEngine;

public class EnemyStateAttack : EnemyState
{
    public EnemyStateAttack(Enemy enemy) : base(enemy) { }

    public override void Enter()
    {
        Character.Animator.DamageFrameReached += TryInflictDamage;
        Character.Animator.AttackComplete += TrySetStateChase;
        Character.Animator.SetTriggerAttack();
    }

    public override void Exit()
    {
        Character.Animator.DamageFrameReached -= TryInflictDamage;
        Character.Animator.AttackComplete -= TrySetStateChase;
    }

    private void TryInflictDamage()
    {
        if (Character.EnemyAttackDetector.IsPlayerInKillZone)
        {
            Character.EnemyAttackDetector.Player.TakeDamage(Character.Damage);
        }
    }

    private void TrySetStateChase()
    {
        if (Character.EnemyAttackDetector.IsPlayerInKillZone)
        {
            Character.Animator.SetTriggerAttack();
        }
        else
        {
            Character.StateMachine.ChangeState<EnemyStateChase>();
        }
    }
}
