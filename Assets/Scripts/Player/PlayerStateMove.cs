using UnityEngine;

public class PlayerStateMove : PlayerState
{
    private const float Deadzone = 0.01f;

    public PlayerStateMove(Player player) : base(player) { }

    public override void Update()
    {
        Character.Mover.Move();
        Character.Animator.SetMoveSpeed(Character.Mover.GetCurrentSpeed());
        Character.Animator.SetMoveDirection(Character.InputReader.Player.Move.ReadValue<Vector2>());

        if (Character.InputReader.Player.Move.ReadValue<Vector2>().magnitude < Deadzone && Character.InputReader.Player.Look.ReadValue<Vector2>().magnitude < Deadzone)
        {
            Character.MovementStateMachine.ChangeState<PlayerStateIdle>();
        }
    }

    public override void LateUpdate()
    {
        Character.Rotator.RotateBody();
        Character.VerticalLook.Rotate();
    }
}