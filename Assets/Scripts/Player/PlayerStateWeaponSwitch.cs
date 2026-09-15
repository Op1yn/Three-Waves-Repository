using UnityEngine;

public class PlayerStateWeaponSwitch : PlayerState
{
    private int _currentWeaponIndex;

    public PlayerStateWeaponSwitch(Player player) : base(player)
    {
        _currentWeaponIndex = 0;
        Character.Weapons[_currentWeaponIndex].gameObject.SetActive(true);
    }

    public override void Enter()
    {
        ChangeWeapons();

        if (Character.WeaponHolding.CurrentWeapon != null)
        {
            if (Character.WeaponHolding.CurrentWeapon.IsFirearm)
            {
                Character.Animator.SetTrueIsFirearm();
            }
            else
            {
                Character.Animator.SetFalseIsFirearm();
            }
        }
    }

    public override void Update()
    {
        Character.CombatStateMachine.ChangeState<PlayerStateNone>();
    }

    private void ChangeWeapons()
    {
        int nextWeaponIndex = (_currentWeaponIndex + 1) % Character.Weapons.Count;

        Character.Weapons[_currentWeaponIndex].gameObject.SetActive(false);
        Character.Weapons[nextWeaponIndex].gameObject.SetActive(true);

        Character.WeaponHolding.TakeUpArms(Character.Weapons[nextWeaponIndex]);
        _currentWeaponIndex = nextWeaponIndex;

        if (Character.WeaponHolding.CurrentWeapon.IsFirearm == false) 
        {
            Character.WeaponHolding.ToLetGoFirearm();
        }
    }
}