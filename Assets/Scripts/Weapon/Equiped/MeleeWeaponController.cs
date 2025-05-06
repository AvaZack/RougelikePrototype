using UnityEngine;
using static UnityEngine.Rendering.HableCurve;

public class MeleeWeaponController : WeaponController
{
    [SerializeField] protected float attackAngle;
    [SerializeField] protected float attackRange;

    [SerializeField] protected Material attackFxMat;

    protected override void Attack()
    {
        base.Attack();
        // 设置特效shader
        attackFxMat.SetFloat("_Radius", attackRange * 2);
        attackFxMat.SetFloat("_Angle", attackAngle);
        attackFxMat.SetVector("_Direction", toMouseDir);

        //
        Collider2D[] collider2Ds = Physics2D.OverlapCircleAll(transform.position, attackRange, LayerMask.GetMask("Enemy"));
        foreach (Collider2D collider in collider2Ds)
        {
            if (collider.CompareTag("Enemy") && IsInSector(collider.transform.position))
            {
                collider.GetComponent<EnemyController>().TakeDamage(damage);
                Debug.Log("melee weapon hit enemy.");
            }
        }

    }



    bool IsInSector(Vector3 targetPos)
    {
        // 1. 计算方向向量（从中心指向目标）
        Vector3 dirToTarget = (targetPos - transform.position).normalized;

        // 2. 计算与鼠标方向扇形方向的夹角
        float currentAngle = Vector3.Angle(toMouseDir, dirToTarget);
        Debug.Log("currentAngle=" + currentAngle);

        // 3. 判断角度和距离
        return currentAngle <= attackAngle / 2f;
    }

    // Gizmos 绘制攻击区域
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        DrawSectorGizmo(transform.position, toMouseDir, attackAngle, attackRange);
    }

    void DrawSectorGizmo(Vector3 center, Vector3 direction, float angle, float radius)
    {
        // 绘制扇形边缘
        UnityEditor.Handles.DrawWireArc(center, Vector3.forward,
            Quaternion.Euler(0, 0, -angle / 2) * direction,
            angle, radius);

        // 绘制两侧边线
        Vector3 leftDir = Quaternion.Euler(0, 0, angle / 2) * direction * radius;
        Vector3 rightDir = Quaternion.Euler(0, 0, -angle / 2) * direction * radius;

        Gizmos.DrawLine(center, center + leftDir);
        Gizmos.DrawLine(center, center + rightDir);
    }
}
