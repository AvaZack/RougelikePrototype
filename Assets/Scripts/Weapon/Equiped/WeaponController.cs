using System;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] protected float cooldown;
    [SerializeField] protected float damage;

    [SerializeField] protected GameObject prefab;

    protected Vector3 toMouseDir;

    float cooldownTimer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
    {
        
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        toMouseDir = getToMouseDir();
        if (cooldownTimer > cooldown)
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

    // 获取朝向鼠标的方向
    private Vector3 getToMouseDir()
    {
        Vector3 mousePositionScreen = Input.mousePosition;
        Vector3 mousePositionWorld = Camera.main.ScreenToWorldPoint(mousePositionScreen);
        mousePositionWorld.z = transform.position.z;
        return (mousePositionWorld - transform.position).normalized;
    }

}
