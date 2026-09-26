using Unity.AppUI.UI;
using UnityEngine;

public class Shovel : Weapon
{
    [SerializeField] private Transform _overlapStartPoint;
    [SerializeField] private float _sphereRadiys = 1f;

    private readonly Collider[] _overlapResults = new Collider[32];
    private int _overlapResultsCount;

    public override void Attack()
    {
        

        if (Physics.OverlapSphereNonAlloc(_overlapStartPoint.position, _sphereRadiys, _overlapResults, _layerMask) > 0)
        {
            for (int i = 0; i < _overlapResultsCount; i++)
            {
                if (_overlapResults[i].gameObject.TryGetComponent(out Enemy enemy))
                {
                    enemy.TakeDamage(Damage);
                }
            }
        }
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.DrawSphere(_overlapStartPoint.position, _sphereRadiys);
    //}
}
