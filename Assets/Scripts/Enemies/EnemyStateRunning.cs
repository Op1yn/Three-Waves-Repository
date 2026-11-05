public class EnemyStateRunning : EnemyState
{
    public EnemyStateRunning(Enemy characters) : base(characters) { }

    public override void Enter()
    {
        Character.AttackDetector.PlayerDetected += SetStateAttack;
        Character.Animator.SetTrueIsMoving();
    }

    public override void Update()
    {
        Character.NavMeshAgent.SetDestination(Character.PlayerTransform.position);
    }

    public override void Exit()
    {
        Character.AttackDetector.PlayerDetected -= SetStateAttack;
        Character.Animator.SetFalseIsMoving();
    }

    private void SetStateAttack()
    {
        Character.StateMachine.ChangeState<EnemyStateAttack>();
    }
}
