using System;
using System.Collections.Generic;
using UnityEngine;

//TODO Остановился тут. Сделать менеджер волн и затем хилбары для игрока и врагов
//TODO Остановился тут. А в Scene должен вестись счёт заспавненных зомби, чтобы останавливать когда потребуется

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
    [field: SerializeField] public Health Health { get; private set; }

    public CharacterController CharacterController { get; private set; }
    public PlayerInputSystemActions InputReader { get; private set; }
    public PlayerStateMachine MovementStateMachine { get; private set; }
    public PlayerStateMachine CombatStateMachine { get; private set; }

    public event Action Dying;

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
        Health.HealthPointsOver -= SetStateDeath;
    }

    public override void TakeDamage(float damage)
    {
        Health.SubtractHealth(damage);
    }

    public void SetStateDeath()
    {
        MovementStateMachine.ChangeState<PlayerStateDead>();
        CombatStateMachine.ChangeState<PlayerStateDead>();
    }

    public void ReportDeaths()
    {
        Dying?.Invoke();
    }
}
