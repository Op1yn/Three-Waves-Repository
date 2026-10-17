using System;
using UnityEngine;

public class EnemyAttackDetector : MonoBehaviour
{
    public IDamageable Player { get; private set; }
    public bool IsPlayerInKillZone { get; private set; } = false;

    public event Action PlayerDetected;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            IsPlayerInKillZone = true;
            Player = player;
            PlayerDetected?.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Player _))
        {
            IsPlayerInKillZone = false;
            Player = null;
        }
    }
}
