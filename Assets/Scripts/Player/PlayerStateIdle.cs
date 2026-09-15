using UnityEngine;

public class PlayerStateIdle : PlayerState
{
    private const float Deadzone = 0.01f;

    public PlayerStateIdle(Player player) : base(player) { }

    public override void Enter()
    {
        Character.Animator.SetMoveSpeed(0);
    }

    public override void Update()
    {
        if (Character.InputReader.Player.Move.ReadValue<Vector2>().magnitude > Deadzone || Character.InputReader.Player.Look.ReadValue<Vector2>().magnitude > Deadzone)
        {
            Character.MovementStateMachine.ChangeState<PlayerStateMove>();
        }
    }
}