using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("BaseComponents")]
    protected Animator animator;

    [SerializeField] float damage;
    [SerializeField] float maxHealth;
    [SerializeField] protected float speed;
    [SerializeField] float attackRadius;
    float health;
    DropController dropController;

    void Awake()
    {
        animator = GetComponent<Animator>();
        dropController = GetComponent<DropController>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        PlayerDetect();
    }

    public void TakeDamage(float damage)
    {
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
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, attackRadius, LayerMask.GetMask("Player"));
        foreach (Collider2D hit in hits)
        {
            if (hit.CompareTag("Player"))
            {
                hit.GetComponent<PlayerController>().TakeDamage(transform, damage);
            }
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }
}
