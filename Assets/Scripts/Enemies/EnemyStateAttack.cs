public class EnemyStateAttack : EnemyState
{
    public EnemyStateAttack(Enemy enemy) : base(enemy) { }

    public override void Enter()
    {
        Character.Animator.DamageFrameReached += TryInflictDamage;
        Character.Animator.AttackComplete += OnAnimationComplete;
        Character.Animator.SetTriggerAttack();
    }

    public override void Exit()
    {
        Character.Animator.DamageFrameReached -= TryInflictDamage;
        Character.Animator.AttackComplete -= OnAnimationComplete;
    }

    private void TryInflictDamage()
    {
        if (Character.AttackDetector.IsPlayerInKillZone)
        {
            Character.AttackDetector.Player.TakeDamage(Character.Damage);
        }
    }

    private void OnAnimationComplete()
    {
        if (Character.AttackDetector.IsPlayerInKillZone)
        {
            Character.Animator.RestartAttack();
        }
        else
        {
            Character.StateMachine.ChangeState<EnemyStateRunning>();
        }
    }
}
