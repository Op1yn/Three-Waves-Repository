using UnityEngine;

public class GameInitializer : MonoBehaviour
{
    [SerializeField] private PlayerInitializer _playerInitializer;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        _playerInitializer.CreatePlayer();
    }
}
