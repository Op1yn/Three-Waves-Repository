using UnityEngine;

public class PlayerInitializer : MonoBehaviour
{
    [SerializeField] private Player _sample;

    public void CreatePlayer()
    {
        Player player = Instantiate(_sample);

        InitializePlayer(player);
    }

    public void InitializePlayer(Player player)//“ут иницианизируем все вспомогательные методы (Mover, 
    {
        InitializeStates(player);
        player.Mover.SetPlayerInputSystemActions(player.InputReader);
        player.Rotator.SetPlayerInputSystemActions(player.InputReader);

        player.WeaponHolding.TakeUpArms(player.Weapons[0]);

        player.gameObject.SetActive(true);
    }

    private void InitializeStates(Player player)
    {
        player.MovementStateMachine.AddState(new PlayerStateIdle(player));
        player.MovementStateMachine.AddState(new PlayerStateMove(player));

        player.CombatStateMachine.AddState(new PlayerStateNone(player));
        player.CombatStateMachine.AddState(new PlayerStateWeaponSwitch(player));
        player.CombatStateMachine.AddState(new PlayerStateAttack(player));

        player.MovementStateMachine.ChangeState<PlayerStateIdle>();
        player.CombatStateMachine.ChangeState<PlayerStateNone>();
    }
}
