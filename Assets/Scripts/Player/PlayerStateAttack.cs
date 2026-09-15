using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStateAttack : PlayerState
{
    public PlayerStateAttack(Player player) : base(player) { }

    public override void Enter()
    {
        Character.Animator.CompletedStrike += SetNoneState;

        if (Character.WeaponHolding.CurrentWeapon.IsFirearm == false)
        {
            Character.WeaponHolding.TakeUpTwoHandedForStriking();
        }

        Character.Animator.SetTriggerAttack();
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