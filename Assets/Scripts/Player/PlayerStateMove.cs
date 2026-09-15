using UnityEngine;

public class PlayerStateMove : PlayerState
{
    public PlayerStateMove(Player player) : base(player) { }

    public override void Update()
    {
        Character.Mover.Move();
        Character.Animator.SetMoveSpeed(Character.Mover.GetCurrentSpeed());
        Character.Animator.SetMoveDirection(Character.InputReader.Player.Move.ReadValue<Vector2>());

        if (Character.InputReader.Player.Move.ReadValue<Vector2>().magnitude == 0)
        {
            Character.MovementStateMachine.ChangeState<PlayerStateIdle>();
        }
    }

    public override void LateUpdate()
    {
        Character.Mover.RotateBody();
    }
}