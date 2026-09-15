using UnityEngine;

public class SceneInitializer : MonoBehaviour
{
    [SerializeField] private Player _player;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        _player.gameObject.SetActive(true);
    }
}
