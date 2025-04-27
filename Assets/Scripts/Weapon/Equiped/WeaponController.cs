using System;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] protected GameObject prefab;
    [SerializeField] protected float attack;
    [SerializeField] protected float cooldown;

    protected Vector3 dir;

    float cooldownTimer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
    {
        
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (cooldownTimer > cooldown)
        {
            Attack();
            cooldownTimer = 0;
        }
        cooldownTimer += Time.deltaTime;

        dir = getToMouseDir();
        Debug.Log("dir=" + dir);
    }

    protected virtual void Attack()
    {

    }

    private Vector3 getToMouseDir()
    {
        Vector3 mousePositionScreen = Input.mousePosition;
        Vector3 mousePositionWorld = Camera.main.ScreenToWorldPoint(mousePositionScreen);
        return mousePositionWorld - transform.position;
    }

}
