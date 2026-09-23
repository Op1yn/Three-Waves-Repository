using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerVerticalLook : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed = 10f;

    private PlayerInputSystemActions _playerInput;
    private Vector2 _directionLook;
    private float _pitch = 0f;

    public void SetPlayerInputSystemActions(PlayerInputSystemActions playerInput)
    {
        _playerInput = playerInput;
    }

    public void Rotate()
    {
        _directionLook = _playerInput.Player.Look.ReadValue<Vector2>();

        float pitchDelta = -_directionLook.y * _rotationSpeed * Time.deltaTime;
        _pitch += pitchDelta;
        _pitch = Mathf.Clamp(_pitch, -50f, 50f);
        Quaternion pitchQ = Quaternion.Euler(_pitch, 0, 0);

        transform.localRotation = pitchQ;
    }
}
