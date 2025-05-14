using UnityEngine;

public class BreakableProp : MonoBehaviour
{
    [SerializeField] float maxHealth;

    float health;
    DropController dropController;

    void Awake()
    {
        dropController = GetComponent<DropController>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        health = maxHealth;
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
