using System;
using UnityEngine;

public abstract class Character : MonoBehaviour, IDamageable
{
    public event Action<Character> Dying;

    public abstract void TakeDamage(float damage);

    public virtual void ReportDeath()
    {
        Dying?.Invoke(this);
    }
}
