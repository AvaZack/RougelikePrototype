using System;
using Unity.VisualScripting;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    public float pierce;
    public Vector3 dir;
    public float destroyAfter = 3f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        fixRotate();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position.Set(speed * Time.deltaTime * dir.x, speed * Time.deltaTime * dir.y, transform.position.z);
        //Destroy(gameObject, destroyAfter);
    }

    private void fixRotate()
    {
        transform.Rotate(dir - new Vector3(0, 0, -45));
    }

}
