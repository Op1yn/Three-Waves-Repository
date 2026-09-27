using UnityEngine;
using UnityEngine.AI;

public class EnemyStateChase : EnemyState
{
    public EnemyStateChase(Enemy characters) : base(characters)
    {
    }

    public override void Update()
    {
        Character.NavMeshAgent.SetDestination(Character.PlayerTransform.position);
    }
}
