using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [field: SerializeField] public float CurrentAmount { get; private set; }
    [field: SerializeField] public float MaximumAmount { get; private set; }

    public event Action<float> HealthChanged;
    public event Action HealthPointsOver;

    public void SubtractHealth(float amount)
    {
        if (IsNumberPositive(amount))
        {
            if (IsNumberPositive(CurrentAmount - amount))
            {
                CurrentAmount -= amount;
            }
            else
            {
                CurrentAmount = 0;
                HealthPointsOver?.Invoke();
            }
        }

        HealthChanged?.Invoke(CurrentAmount);
    }

    public void SetMaximumHealthPoints()
    {
        CurrentAmount = MaximumAmount;
    }

    private bool IsNumberPositive(float value)
    {
        return value > 0;
    }
}
