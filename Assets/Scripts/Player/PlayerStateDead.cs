public class PlayerStateDead : PlayerState
{
    public PlayerStateDead(Player player) : base(player) { }

    public override void Enter()
    {
        Character.Animator.SetTriggerDead();
    }
}
