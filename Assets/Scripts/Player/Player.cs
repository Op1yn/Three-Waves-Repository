using System.Collections.Generic;
using UnityEngine;

//TODO Остановился тут. Надо сделать врагам состояния, атаки и смерть врагов и игрока. Затем делать босса.

[RequireComponent(typeof(CharacterController))]
public class Player : Character
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
