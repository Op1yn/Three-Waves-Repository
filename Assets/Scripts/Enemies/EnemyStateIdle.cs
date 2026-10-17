using UnityEngine;

public class EnemyStateIdle : EnemyState
{
    public EnemyStateIdle(Enemy characters) : base(characters)
    {
    }

    public override void Enter()
    {
        Character.StateMachine.ChangeState<EnemyStateChase>();
    }
}
