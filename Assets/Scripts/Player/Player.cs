using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
//TODO —делал основной слой анимации. ƒальше нежно сделать 4 анимации боевого сло€ (удар выстрел держание лопаты и ружь€)
//TODO ¬озможно держание лопаты и ружь€ можно в аниматоре сделать одним состо€нием и мен€ть положение рук с помощью IK Constrain
public class Player : MonoBehaviour
{
    [SerializeField] private GroundDetector _groundDetector;

    [field: SerializeField] public PlayerAnimator Animator { get; private set; }
    [field: SerializeField] public PlayerMover Mover { get; private set; }
    [field: SerializeField] public PlayerRotator Rotator { get; private set; }
    [field: SerializeField] public List<Weapon> Weapons { get; private set; }
    [field: SerializeField] public WeaponHolder WeaponHolding { get; private set; }

    public CharacterController CharacterController { get; private set; }
    public PlayerInputSystemActions InputReader { get; private set; }
    public PlayerStateMachine MovementStateMachine { get; private set; }
    public PlayerStateMachine CombatStateMachine { get; private set; }

    private void Awake()
    {
        MovementStateMachine = new PlayerStateMachine();
        CombatStateMachine = new PlayerStateMachine();
    }
}
