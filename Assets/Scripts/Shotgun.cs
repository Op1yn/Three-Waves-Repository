using UnityEngine;

public class Shotgun : Weapon
{
    [SerializeField] private int _pelletsCount;
    [SerializeField] private float _spreadFactor;
    [SerializeField] private float _shotRange;

    public override void Attack()
    {
        for (int i = 0; i < _pelletsCount; i++)
        {
            PerformRaycast();
        }
    }

    private void PerformRaycast()
    {
        Vector3 direction = transform.forward + CalculateSpread();
        Ray ray = new Ray(transform.position, direction);

        Debug.DrawRay(transform.position, direction * _shotRange, Color.red, 15);

        if (Physics.Raycast(ray, out RaycastHit hitInfo, _shotRange, _layerMask))
        {
            Collider hitCollider = hitInfo.collider;

            if (hitCollider.gameObject.TryGetComponent(out Enemy enemy))
            {
                enemy.TakeDamage(Damage);
            }
        }
    }

    private Vector3 CalculateSpread()
    {
        return new Vector3
        {
            x = Random.Range(0f, _spreadFactor),
            y = Random.Range(0f, _spreadFactor),
            z = Random.Range(0f, _spreadFactor),
        };
    }
}
