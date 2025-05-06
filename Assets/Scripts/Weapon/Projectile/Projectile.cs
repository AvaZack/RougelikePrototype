using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Projectile : MonoBehaviour
{
    public float speed;
    public float pierce;
    public Vector3 dir;
    public float destroyAfter = 3f;
    public float damage;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        fixRotate();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position += dir * speed * Time.deltaTime;
        Destroy(gameObject, destroyAfter);
    }

    // 根据飞行方向修正prefab角度
    private void fixRotate()
    {
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle - 45, Vector3.forward);
    }

    internal void Initialize(float speed, int pierce, float damage)
    {
        this.speed = speed;
        this.pierce = pierce;
        this.damage = damage;
    }

    internal void setDir(Vector3 dir)
    {
        this.dir = dir;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("projectile hit sth.");
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<EnemyController>().TakeDamage(damage);
        }
    }

}
