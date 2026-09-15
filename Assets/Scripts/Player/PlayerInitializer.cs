using UnityEngine;

public class PlayerInitializer : MonoBehaviour
{
    [SerializeField] private Player _player;

    public void InitializePlayer()
    {
        InitializeStates();

        _player.gameObject.SetActive(true);
    }

    private void InitializeStates()
    {
        _player.MovementStateMachine.AddState(new PlayerStateIdle(_player));
        _player.MovementStateMachine.AddState(new PlayerStateMove(_player));

        _player.CombatStateMachine.AddState(new PlayerStateNone(_player));
        _player.CombatStateMachine.AddState(new PlayerStateWeaponSwitch(_player));
        _player.CombatStateMachine.AddState(new PlayerStateAttack(_player));

        _player.MovementStateMachine.ChangeState<PlayerStateIdle>();
        _player.CombatStateMachine.ChangeState<PlayerStateNone>();
    }
}
