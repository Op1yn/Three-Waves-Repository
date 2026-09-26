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

    public void TurnToTarget()
    {
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, 100))
        {
            _followerTransform.position = hit.point;
        }
        else
        {
            _followerTransform.position = _targetTransform.position;
        }
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