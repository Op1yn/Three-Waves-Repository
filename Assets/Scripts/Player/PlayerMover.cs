using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 3f;
    [SerializeField] private float _rotationSpeed = 15f;
    [SerializeField] private const float _forceGravity = -9.81f;
    [SerializeField] CharacterController _characterController;

    private PlayerInputSystemActions _playerInput;
    private Vector2 _directionMovement;
    private Vector3 _horizontalDirection;
    private Vector2 _directionLook;
    private float _verticalVelocity;

    public void SetPlayerInputSystemActions(PlayerInputSystemActions playerInput)// Нужно инициализировать это в соотвевующем класе инициализации игрока
    {
        _playerInput = playerInput;
    }

    public float GetCurrentSpeed()
    {
        return _horizontalDirection.magnitude * _moveSpeed;
    }

    public void Move()
    {
        _directionMovement = _playerInput.Player.Move.ReadValue<Vector2>();
        _horizontalDirection = (transform.forward * _directionMovement.y + transform.right * _directionMovement.x);

        if (_horizontalDirection.magnitude > 0.1f)
            _horizontalDirection.Normalize();

        _verticalVelocity += _forceGravity * Time.deltaTime;
        Vector3 offset = (_horizontalDirection * _moveSpeed * Time.deltaTime) + new Vector3(0, _verticalVelocity * Time.deltaTime, 0);
        _characterController.Move(offset);

        if (_characterController.isGrounded)
            _verticalVelocity = 0;
    }

    public void RotateBody()
    {
        _directionLook = _playerInput.Player.Look.ReadValue<Vector2>();
        transform.Rotate(Vector3.up, _directionLook.x * _rotationSpeed * Time.deltaTime, Space.World);
    }
}