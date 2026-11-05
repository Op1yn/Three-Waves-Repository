using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] private Player _sample;
    [SerializeField] private Transform _targetTransform;
    [SerializeField] private HealthBarDisplay _healthBarDisplay;

    public Player CreatePlayer()
    {
        Player player = Instantiate(_sample);
        player.Initialize();
        player.Health.HealthChanged += _healthBarDisplay.UpdateHealthDisplay;
        player.WeaponHolding.SetTargetTransform(_targetTransform);
        player.gameObject.SetActive(false);

        return player;
    }
}
