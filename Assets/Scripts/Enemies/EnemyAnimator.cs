using System;
using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    public static readonly int IsMoving = Animator.StringToHash(nameof(IsMoving));
    public static readonly int Attack = Animator.StringToHash(nameof(Attack));
    public static readonly int Dead = Animator.StringToHash(nameof(Dead));

    private static readonly int Strike = Animator.StringToHash(nameof(Strike));

    public event Action DamageFrameReached;
    public event Action AttackComplete;
    public event Action DeathAnimationComplete;

    public void SetTriggerAttack()
    {
        _animator.SetTrigger(Attack);
    }

    public void ReportEndOfAttack()
    {
        AttackComplete?.Invoke();
    }

    public void ReportEndDeathAnimation()
    {
        DeathAnimationComplete?.Invoke();
    }

    public void OnDamageFrameReached()
    {
        DamageFrameReached?.Invoke();
    }

    public void SetTrueIsMoving()
    {
        _animator.SetBool(IsMoving, true);
    }

    public void SetFalseIsMoving()
    {
        _animator.SetBool(IsMoving, false);
    }

    public void SetTriggerDead()
    {
        _animator.SetTrigger(Dead);
    }

    public void RestartAttack()
    {
        _animator.Play(Strike, 0, 0f);
    }
}
