using System;
using UnityEngine;

public class PlayerInitializer : MonoBehaviour
{
    [SerializeField] private Player _sample;
    [SerializeField] private Transform _targetTransform;

    private Player _player;

    public Player CreatePlayer()
    {
        if (_player)
        {
            Destroy(_player.gameObject);
        }

        _player = Instantiate(_sample);
        InitializePlayer(_player);

        return _player;
    }

    public void InitializePlayer(Player player)
    {
        InitializeStates(player);
        player.Mover.SetPlayerInputSystemActions(player.InputReader);
        player.Rotator.SetPlayerInputSystemActions(player.InputReader);
        player.VerticalLook.SetPlayerInputSystemActions(player.InputReader);
        player.Animator.DeathAnimationEnded += player.ReportDeaths;

        InitializeWeaponHolder(player, _targetTransform);

        player.gameObject.SetActive(true);
    }

    //public void DestroyPlayer()
    //{
    //    Destroy(_player);
    //}

    private void InitializeStates(Player player)
    {
        player.MovementStateMachine.AddState(new PlayerStateIdle(player));
        player.MovementStateMachine.AddState(new PlayerStateMove(player));
        player.MovementStateMachine.AddState(new PlayerStateDead(player));

        player.CombatStateMachine.AddState(new PlayerStateNone(player));
        player.CombatStateMachine.AddState(new PlayerStateWeaponSwitch(player));
        player.CombatStateMachine.AddState(new PlayerStateAttack(player));

        player.Health.HealthPointsOver += player.SetStateDeath;

        player.MovementStateMachine.ChangeState<PlayerStateIdle>();
        player.CombatStateMachine.ChangeState<PlayerStateWeaponSwitch>();
    }

    private void InitializeWeaponHolder(Player player, Transform targetTransform)
    {
        player.WeaponHolding.SetTargetTransform(targetTransform);
    }
}
