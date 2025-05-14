using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] float damage;
    [SerializeField] float maxHealth;
    [SerializeField] protected float speed;
    [SerializeField] float attackRadius;
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
        PlayerDetect();
    }

    public void TakeDamage(float damage)
    {
        Debug.Log("Enemy take damage=" + damage);
        health -= damage;
        if (health <= 0)
        {
            dropController.OnDeath();
            Destroy(gameObject);
        }
    }

    //TO BE IMPLEMENTED
    void PlayerDetect()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position,attackRadius,1<<LayerMask.NameToLayer("Player"));
        foreach (Collider2D hit in hits) {
            if (hit.CompareTag("Player"))
            {
                hit.GetComponent<PlayerController>().TakeDamage(transform, damage);
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }
}
