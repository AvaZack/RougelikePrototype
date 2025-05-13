using UnityEngine;

public class BreakableProp : MonoBehaviour
{
    [SerializeField] float maxHealth;

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
        health -= damage;
        if (health < 0)
        {
            dropController.OnDeath();
            Destroy(gameObject);
        }
    }
}
