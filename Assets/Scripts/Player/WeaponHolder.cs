using System;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class WeaponHolder : MonoBehaviour
{
    [SerializeField] private Transform TwoHandedInIdle;//тут
    [SerializeField] private Transform TwoHandedInStriking;//и тут в инспекторе заменить. Еслди не придётся вообще удалить
    [SerializeField] private TwoBoneIKConstraint _rightHandIK;
    [SerializeField] private TwoBoneIKConstraint _leftHandIK;

    public Weapon CurrentWeapon { get; private set; }

    private void Start()
    {
        _rightHandIK.weight = 0f;
        _leftHandIK.weight = 0f;
    }

    public void TakeUpArms(Weapon weapon)
    {
        CurrentWeapon = weapon;

        if (CurrentWeapon.IsFirearm)
        {
            TakeUpFirearm();
        }
        else
        {
            TakeUpTwoHanded();
        }
    }

    public void TakeUpTwoHandedForStriking()
    {
        CurrentWeapon.transform.parent = TwoHandedInStriking;
        CurrentWeapon.transform.position = TwoHandedInStriking.position;
        CurrentWeapon.transform.rotation = TwoHandedInStriking.rotation;
    }

    public void ToLetGoFirearm()
    {
        _rightHandIK.weight = 0f;
        _leftHandIK.weight = 0f;
    }

    private void TakeUpTwoHanded()
    {
        CurrentWeapon.transform.parent = TwoHandedInIdle;
        CurrentWeapon.transform.position = TwoHandedInIdle.position;
        CurrentWeapon.transform.rotation = TwoHandedInIdle.rotation;
    }

    private void TakeUpFirearm()
    {
        _rightHandIK.weight = 1f;
        _leftHandIK.weight = 1f;
    }
}