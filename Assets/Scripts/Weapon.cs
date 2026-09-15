using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] protected Player _player;//удалить
    [SerializeField] protected LayerMask _layerMask;

    [field: SerializeField] public int Damage { get; private set; }
    [field: SerializeField] public bool IsFirearm { get; private set; }
}