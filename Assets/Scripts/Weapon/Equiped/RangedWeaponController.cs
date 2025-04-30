using UnityEngine;

public class RangedWeaponController : WeaponController
{
    protected override void Attack()
    {
        base.Attack();
        // TODO: ���������
        GameObject obj = Instantiate(weaponData.ProjectilePrefab, transform.position, Quaternion.identity);
        Projectile projectile = obj.GetComponent<Projectile>();
        projectile.Initialize(weaponData);
        projectile.setDir(dir);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(new Ray(transform.position, dir));
    }
}
