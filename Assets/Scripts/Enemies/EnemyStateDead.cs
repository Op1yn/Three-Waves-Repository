public class EnemyStateDead : EnemyState
{
    public EnemyStateDead(Enemy enemy) : base(enemy) { }

    public override void Enter()
    {
        if (Character.NavMeshAgent.enabled && Character.NavMeshAgent.isOnNavMesh)
        {
            Character.NavMeshAgent.isStopped = true;
            Character.NavMeshAgent.ResetPath();
        }

        Character.Animator.SetTriggerDead();
    }
}
