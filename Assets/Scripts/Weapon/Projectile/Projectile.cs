using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Projectile : MonoBehaviour
{
    public float speed;
    public float pierce;
    public Vector3 dir;
    public float destroyAfter = 2f;
    public float damage;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        fixRotate();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += dir.normalized * speed * Time.deltaTime;
        Destroy(gameObject, destroyAfter);
    }

    // 根据飞行方向修正prefab角度
    private void fixRotate()
    {
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle - 45, Vector3.forward);
    }

    internal void Initialize(WeaponScriptableObj data)
    {
        this.speed = data.Speed;
        this.pierce = data.Pierce;
        this.damage = data.Damage;
    }

    internal void setDir(Vector3 dir)
    {
        this.dir = dir;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<EnemyController>().TakeDamage(damage);
        }
    }

}
