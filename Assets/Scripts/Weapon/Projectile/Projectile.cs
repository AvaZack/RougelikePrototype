using System;
using Unity.VisualScripting;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    public float pierce;
    public Vector2 dir;
    public float destroyAfter = 2f;
    private Rigidbody2D rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        fixRotate();
    }

    // Update is called once per frame
    void Update()
    {
        rb.linearVelocity = dir.normalized * speed;
        Destroy(gameObject, destroyAfter);
    }

    public void Initialize(float speed, float pierce, Vector2 dir)
    {
        this.speed = speed;
        this.pierce = pierce;
        this.dir = dir;
    }

    // 根据飞行方向修正prefab角度
    private void fixRotate()
    {
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle - 45, Vector3.forward);
    }

}
