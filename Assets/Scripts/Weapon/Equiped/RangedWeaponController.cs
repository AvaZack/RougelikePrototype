using UnityEngine;

public class RangedWeaponController : WeaponController
{
    [SerializeField] protected GameObject projectilePrefab;
    [SerializeField] protected float speed;
    [SerializeField] protected float pierce;

    protected override void Attack()
    {
        base.Attack();
        GameObject obj = Instantiate(projectilePrefab);
        obj.transform.position = transform.position;
        Projectile projectile = obj.GetComponent<Projectile>();
        projectile.speed = speed;
        projectile.pierce = pierce;
        projectile.dir = dir;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(new Ray(transform.position, dir));
    }
}
