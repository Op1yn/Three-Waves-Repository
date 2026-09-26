using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
//TODO Возможно держание лопаты и ружья можно в аниматоре сделать одним состоянием и менять положение рук с помощью IK Constrain
//TODO Остановился тут. Надо сделать стрельбу (лучи) и удар лопатой (область по которой наносится урон). Затем сделать обычных врогов и босса

[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{
    [SerializeField] private GroundDetector _groundDetector;

    [field: SerializeField] public PlayerAnimator Animator { get; private set; }
    [field: SerializeField] public PlayerMover Mover { get; private set; }
    [field: SerializeField] public PlayerRotator Rotator { get; private set; }
    [field: SerializeField] public PlayerVerticalLook VerticalLook { get; private set; }
    [field: SerializeField] public List<Weapon> Weapons { get; private set; }
    [field: SerializeField] public PlayerWeaponHolder WeaponHolding { get; private set; }

    public CharacterController CharacterController { get; private set; }
    public PlayerInputSystemActions InputReader { get; private set; }
    public PlayerStateMachine MovementStateMachine { get; private set; }
    public PlayerStateMachine CombatStateMachine { get; private set; }

    private void Awake()
    {
        MovementStateMachine = new PlayerStateMachine();
        CombatStateMachine = new PlayerStateMachine();
        InputReader = new PlayerInputSystemActions();
        CharacterController = GetComponent<CharacterController>();
    }

    private void OnEnable()
    {
        InputReader.Enable();
    }

    private void Update()
    {
        MovementStateMachine.Update();
        CombatStateMachine.Update();
    }

    private void LateUpdate()
    {
        MovementStateMachine.LateUpdate();
        CombatStateMachine.LateUpdate();
    }

    private void OnDisable()
    {
        InputReader?.Disable();
    }
}
