using UnityEngine;

public class RangedWeaponController : WeaponController
{
    [SerializeField] protected GameObject projectilePrefab;
    [SerializeField] protected float speed;
    [SerializeField] protected float pierce;

    protected override void Attack()
    {
        base.Attack();
        // TODO: 建立对象池
        GameObject obj = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        Projectile projectile = obj.GetComponent<Projectile>();
        projectile.Initialize(speed, pierce, dir);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(new Ray(transform.position, dir));
    }
}
