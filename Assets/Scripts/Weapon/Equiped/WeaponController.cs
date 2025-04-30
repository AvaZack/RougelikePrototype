using System;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [Header("Weapon stats")]
    public WeaponScriptableObj weaponData;

    protected Vector3 dir;

    float cooldownTimer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
    {
        
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        dir = getToMouseDir();
        if (cooldownTimer > weaponData.Cooldown)
        {
            Attack();
            cooldownTimer = 0;
        }
        cooldownTimer += Time.deltaTime;
    }

    protected virtual void Attack()
    {
        Debug.Log("Weapon attack");
    }

    // ��ȡ�������ķ���
    private Vector3 getToMouseDir()
    {
        Vector3 mousePositionScreen = Input.mousePosition;
        Vector3 mousePositionWorld = Camera.main.ScreenToWorldPoint(mousePositionScreen);
        return (mousePositionWorld - transform.position).normalized;
    }

}
