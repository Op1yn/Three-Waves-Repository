using System.Net.NetworkInformation;
using UnityEngine;

public class EnemyStateDead : EnemyState
{
    public EnemyStateDead(Enemy enemy) : base(enemy) { }

    public override void Enter()
    {
        Character.Animator.SetTriggerDead();
        Character.EnemyAttackDetector.gameObject.SetActive(false);
        Character.NavMeshAgent.ResetPath();
        Character.HitCollider.enabled = false;
    }
}
