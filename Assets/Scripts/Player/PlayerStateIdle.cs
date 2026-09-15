using UnityEngine;

public class PlayerStateIdle : PlayerState
{
    public PlayerStateIdle(Player player) : base(player) { }

    public override void Enter()
    {
        Character.Animator.SetMoveSpeed(0);
    }

    public override void Update()
    {
        if (Character.InputReader.Player.Move.ReadValue<Vector2>().magnitude != 0)
        {
            Character.MovementStateMachine.ChangeState<PlayerStateMove>();
        }
    }

    public override void LateUpdate()
    {
        Character.Mover.RotateBody();
    }
}