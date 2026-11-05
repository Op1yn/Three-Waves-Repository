using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Player : Character
{
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

    private void Awake()
    {
        MovementStateMachine = new PlayerStateMachine();
        CombatStateMachine = new PlayerStateMachine();
        InputReader = new PlayerInputSystemActions();
        CharacterController = GetComponent<CharacterController>();
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

    private void OnDestroy()
    {
        Health.HealthPointsOver -= SetStateDeath;
    }

    public void Initialize()
    {
        Health.HealthPointsOver += SetStateDeath;
        Mover.SetPlayerInputSystemActions(InputReader);
        Rotator.SetPlayerInputSystemActions(InputReader);
        VerticalLook.SetPlayerInputSystemActions(InputReader);
        Animator.DeathAnimationEnded += ReportDeath;
        InitializeStates();
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

    public void DisableControl()
    {
        InputReader?.Disable();
    }

    public void SetStartingParameters(Vector3 position)
    {
        Health.SetMaximumHealthPoints();
        transform.position = position;
        InputReader.Enable();
        MovementStateMachine.ChangeState<PlayerStateIdle>();
        CombatStateMachine.ChangeState<PlayerStateWeaponSwitch>();
        gameObject.SetActive(true);
    }

    private void InitializeStates()
    {
        MovementStateMachine.AddState(new PlayerStateIdle(this));
        MovementStateMachine.AddState(new PlayerStateMove(this));
        MovementStateMachine.AddState(new PlayerStateDead(this));

        CombatStateMachine.AddState(new PlayerStateNone(this));
        CombatStateMachine.AddState(new PlayerStateWeaponSwitch(this));
        CombatStateMachine.AddState(new PlayerStateAttack(this));
    }
}
