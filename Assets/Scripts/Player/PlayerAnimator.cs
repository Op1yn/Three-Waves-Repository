using System;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    public static readonly int MoveSpeed = Animator.StringToHash(nameof(MoveSpeed));
    public static readonly int MoveDirectionForwardBackward = Animator.StringToHash(nameof(MoveDirectionForwardBackward));
    public static readonly int MoveDirectionLeftRight = Animator.StringToHash(nameof(MoveDirectionLeftRight));
    public static readonly int IsFirearm = Animator.StringToHash(nameof(IsFirearm));
    public static readonly int Attack = Animator.StringToHash(nameof(Attack));

    private Vector2 _currentInput = new Vector2(0, 0);
    private float _smoothTime = 0.2f;
    private Vector2 _velocity = new Vector2(0, 0);

    public event Action CompletedStrike;

    public void SetMoveDirection(Vector2 inputDirection)
    {
        _currentInput = Vector2.SmoothDamp(_currentInput, inputDirection, ref _velocity, _smoothTime);

        _animator.SetFloat(MoveDirectionForwardBackward, _currentInput.y);
        _animator.SetFloat(MoveDirectionLeftRight, _currentInput.x);
    }

    public void SetMoveSpeed(float speed)
    {
        _animator.SetFloat(MoveSpeed, speed);
    }

    public void SetTrueIsFirearm()
    {
        _animator.SetBool(IsFirearm, true);
    }

    public void SetFalseIsFirearm()
    {
        _animator.SetBool(IsFirearm, false);
    }

    public void ReportEndOfStrike()
    {
        CompletedStrike?.Invoke();
    }

    public void SetTriggerAttack()
    {
        _animator.SetTrigger(Attack);
    }
}