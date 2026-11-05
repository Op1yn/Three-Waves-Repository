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
                UpdateHealth(CurrentAmount -= amount);
            }
            else
            {
                UpdateHealth(0);
                HealthPointsOver?.Invoke();
            }
        }
    }

    public void SetMaximumHealthPoints()
    {
        UpdateHealth(MaximumAmount);
    }

    private void UpdateHealth(float amount)
    {
        CurrentAmount = amount;
        HealthChanged?.Invoke(CurrentAmount);
    }

    private bool IsNumberPositive(float value)
    {
        return value > 0;
    }
}
