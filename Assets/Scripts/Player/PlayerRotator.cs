using UnityEngine;

public class PlayerRotator : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed = 15f;

    private PlayerInputSystemActions _playerInput;
    private Vector2 _directionLook;

    public void SetPlayerInputSystemActions(PlayerInputSystemActions playerInput)
    {
        _playerInput = playerInput;
    }

    public void RotateBody()
    {
        _directionLook = _playerInput.Player.Look.ReadValue<Vector2>();
        transform.Rotate(Vector3.up, _directionLook.x * _rotationSpeed * Time.deltaTime, Space.World);
    }
}
