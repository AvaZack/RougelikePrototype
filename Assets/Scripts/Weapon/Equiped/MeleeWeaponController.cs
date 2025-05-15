using System.Collections;
using System.Linq;
using UnityEngine;
using static UnityEngine.Rendering.HableCurve;

public class MeleeWeaponController : WeaponController
{
    [SerializeField] protected float attackAngle;
    [SerializeField] protected float attackRange;

    [SerializeField] protected Material attackFxMat;
    [SerializeField] protected float attackFxLast;

    protected override void Attack()
    {
        base.Attack();
        // ������Чshader
        StartCoroutine(ShowAttackFx());

        //��������
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, attackRange, LayerMask.GetMask("Enemy"));
        foreach (Collider2D enemy in enemies)
        {
            //Debug.Log("hit " + enemies.Length + " enemies");
            if (enemy.CompareTag("Enemy") && IsInSector(enemy.transform.position))
            {
                enemy.GetComponent<EnemyController>().TakeDamage(damage);
            }
        }

        //���ƻ�����
        Collider2D[] props = Physics2D.OverlapCircleAll(transform.position, attackRange, LayerMask.GetMask("Terrian"));
        foreach (Collider2D prop in props)
        {
            //Debug.Log("break " + props.Length + " props");
            if (prop.CompareTag("BreakableProp") && IsInSector(prop.transform.position))
            {
                prop.GetComponent<BreakableProp>().TakeDamage(damage);
            }
        }

    }

    bool IsInSector(Vector3 targetPos)
    {
        // 1. ���㷽��������������ָ��Ŀ�꣩
        Vector3 dirToTarget = (targetPos - transform.position).normalized;

        // 2. ��������귽�����η���ļн�
        float currentAngle = Vector3.Angle(toMouseDir, dirToTarget);

        // 3. �жϽǶȺ;���
        return currentAngle <= attackAngle / 2f;
    }

    // Gizmos ���ƹ�������
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        DrawSectorGizmo(transform.position, toMouseDir, attackAngle, attackRange);
    }

    void DrawSectorGizmo(Vector3 center, Vector3 direction, float angle, float radius)
    {
        // �������α�Ե
        //UnityEditor.Handles.DrawWireArc(center, Vector3.forward,
        //    Quaternion.Euler(0, 0, -angle / 2) * direction,
        //    angle, radius);

        // �����������
        Vector3 leftDir = Quaternion.Euler(0, 0, angle / 2) * direction * radius;
        Vector3 rightDir = Quaternion.Euler(0, 0, -angle / 2) * direction * radius;

        Gizmos.DrawLine(center, center + leftDir);
        Gizmos.DrawLine(center, center + rightDir);
    }

    IEnumerator ShowAttackFx()
    {
        attackFxMat.SetColor("_MainColor", new Color(1, 0, 0, 0.5f));
        attackFxMat.SetFloat("_Radius", attackRange * 2);
        attackFxMat.SetFloat("_Angle", attackAngle);
        attackFxMat.SetVector("_Direction", toMouseDir);
        yield return new WaitForSeconds(attackFxLast);
        attackFxMat.SetColor("_MainColor", new Color(1, 0, 0, 0));
    }
}
