using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] protected LayerMask _layerMask;

    [field: SerializeField] public int Damage { get; private set; }
    [field: SerializeField] public bool IsFirearm { get; private set; }

    public abstract void Attack();
}