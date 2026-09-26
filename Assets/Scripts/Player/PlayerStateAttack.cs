using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStateAttack : PlayerState
{
    public PlayerStateAttack(Player player) : base(player) { }

    public override void Enter()
    {
        Character.Animator.CompletedStrike += SetNoneState;
        Character.Animator.SetTriggerAttack();
        Character.WeaponHolding.CurrentWeapon.Attack();
    }

    public override void LateUpdate()
    {
        Character.WeaponHolding.TurnToTarget();
    }

    public override void Exit()
    {
        Character.Animator.CompletedStrike -= SetNoneState;
    }

    private void SetNoneState()
    {
        Character.CombatStateMachine.ChangeState<PlayerStateNone>();
    }
}