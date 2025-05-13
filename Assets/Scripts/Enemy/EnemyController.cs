using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] float damage;
    [SerializeField] float maxHealth;
    [SerializeField] protected float speed;

    float health;
    DropController dropController;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        health = maxHealth;
        dropController = GetComponent<DropController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(float damage)
    {
        Debug.Log("Enemy take damage=" + damage);
        health -= damage;
        if (health <= 0) {
            dropController.OnDeath();
            Destroy(gameObject);
        }
    }
}
