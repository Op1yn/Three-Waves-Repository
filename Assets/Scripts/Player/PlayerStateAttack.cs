using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStateAttack : PlayerState
{
    public PlayerStateAttack(Player player) : base(player) { }

    public override void Enter()
    {
        Character.Animator.CompletedStrike += SetNoneState;
        Character.Animator.SetTriggerAttack();

        if (Character.WeaponHolding.CurrentWeapon.IsFirearm == false)
        {
            Character.Animator.DamageFrameReached += Character.WeaponHolding.CurrentWeapon.Attack;
        }
        else
        {
            Character.WeaponHolding.CurrentWeapon.Attack();
        }
    }

    public override void LateUpdate()
    {
        Character.WeaponHolding.TurnToTarget();
    }

    public override void Exit()
    {
        Character.Animator.CompletedStrike -= SetNoneState;

        if (Character.WeaponHolding.CurrentWeapon.IsFirearm == false)
        {
            Character.Animator.DamageFrameReached -= Character.WeaponHolding.CurrentWeapon.Attack;
        }
    }

    private void SetNoneState()
    {
        Character.CombatStateMachine.ChangeState<PlayerStateNone>();
    }
}