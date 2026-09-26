using System;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class PlayerWeaponHolder : MonoBehaviour
{
    [SerializeField] private Transform ShovelInIdle;
    [SerializeField] Rig _weaponsRig;
    [SerializeField] private Transform _followerTransform;

    private Transform _targetTransform;

    public Weapon CurrentWeapon { get; private set; }

    private void Start()
    {
        _weaponsRig.weight = 0f;
    }

    public void SetTargetTransform(Transform targetTransform)
    {
        _targetTransform = targetTransform;
    }

    public void TakeUpArms(Weapon weapon)
    {
        CurrentWeapon = weapon;

        if (CurrentWeapon.IsFirearm)
        {
            TakeUpShotgun();
        }
    }

    public void TurnToTarget()//Тут перед записью позиции в Follower нужно кидать рейкаст и смотреть расстояние до цели и приписывать его уже потом
    {
        _followerTransform.position = _targetTransform.position;
    }

    public void ToLetGoFirearm()
    {
        _weaponsRig.weight = 0f;
    }

    private void TakeUpShotgun()
    {
        _weaponsRig.weight = 1f;
    }
}