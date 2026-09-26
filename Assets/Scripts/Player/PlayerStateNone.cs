using UnityEngine.InputSystem;

public class PlayerStateNone : PlayerState
{
    public PlayerStateNone(Player player) : base(player) { }

    public override void Enter()
    {
        Character.InputReader.Player.Attack.started += SetAttackState;
        Character.InputReader.Player.Interact.started += SetWeaponSwitchingState;
    }

    public override void LateUpdate()
    {
        Character.WeaponHolding.TurnToTarget();
    }

    public override void Exit()
    {
        Character.InputReader.Player.Attack.started -= SetAttackState;
        Character.InputReader.Player.Interact.started -= SetWeaponSwitchingState;
    }

    private void SetWeaponSwitchingState(InputAction.CallbackContext context)
    {
        Character.CombatStateMachine.ChangeState<PlayerStateWeaponSwitch>();
    }

    private void SetAttackState(InputAction.CallbackContext context)
    {
        Character.CombatStateMachine.ChangeState<PlayerStateAttack>();
    }
}