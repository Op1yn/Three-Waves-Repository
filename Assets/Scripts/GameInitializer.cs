using UnityEngine;

public class GameInitializer : MonoBehaviour
{
    [SerializeField] private PlayerInitializer _playerInitializer;
    [SerializeField] private EnemySpawner _enemySpawner;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        Player player = _playerInitializer.CreatePlayer();

        InitializeEnemySpawner(player);
    }

    private void InitializeEnemySpawner(Player player)
    {
        _enemySpawner.SetPlayerTransform(player.transform);
        _enemySpawner.InitializeObjectPool();
        _enemySpawner.StartSpawn();
    }
}
