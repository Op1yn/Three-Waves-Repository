using UnityEngine;

public abstract class Character : MonoBehaviour, IDamageable
{
    public abstract void TakeDamage(float damage);
}
