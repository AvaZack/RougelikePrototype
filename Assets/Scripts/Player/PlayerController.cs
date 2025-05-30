using System;
using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("BaseComponent")]
    SpriteRenderer sr;

    [Header("Movement")]
    [SerializeField] float moveSpeed;
    Rigidbody2D rb;

    [HideInInspector] public Vector3 moveDir;

    [Header("Stats")]
    [SerializeField] float maxHealth;

    float currentHealth;
    float currentExp;
    int currentLevel;

    [Header("Damage")]
    [SerializeField] float knockbackForce = 5f;
    [SerializeField] float knockbackDuration = 0.2f;
    [SerializeField] float invincibleDuration = 1.5f;
    bool isInvincible;
    float invincibleTimer;
    Material flashMaterial;
    Material originMaterial;

    [Header("Pickup")]
    [SerializeField] float pickupRange;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        currentHealth = maxHealth;
        currentLevel = 1;

        isInvincible = false;
        flashMaterial = new Material(Shader.Find("Custom/Flash"));
        originMaterial = sr.material;
    }

    // Update is called once per frame
    void Update()
    {
        InputManagement();
    }

    private void FixedUpdate()
    {
        Movement();
        InvincibleCountDown();
    }

    void InputManagement()
    {
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputY = Input.GetAxisRaw("Vertical");

        moveDir = new Vector3(inputX, inputY);
    }

    void Movement()
    {
        rb.linearVelocity = moveDir * moveSpeed;
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    CollectibleItem item = collision.GetComponent<CollectibleItem>();
    //    if (item != null)
    //    {
    //        ApplyItemEffect(item);
    //    }
    //}

    public void ApplyItemEffect(CollectibleItem item)
    {
        //Debug.Log("collect " + item.itemType + " value " + item.value);
        switch (item.itemType)
        {
            case CollectibleItem.ItemType.Health:
                IncreaseHealth(item.value);
                break;
            case CollectibleItem.ItemType.Experience:
                IncreaseExp(item.value);
                break;
            case CollectibleItem.ItemType.Coin:
                break;
        }
    }

    public void IncreaseHealth(float heal)
    {
        currentHealth += heal;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    public void IncreaseExp(float exp)
    {
        currentExp += exp;
        float levelUpExp = GetLevelUpExp(currentLevel);
        if (currentExp >= levelUpExp)
        {
            LevelUp();
            currentExp -= levelUpExp;
        }
    }

    private void LevelUp()
    {
        currentLevel++;
        //Stats increase..
        maxHealth += 10;
        currentHealth = maxHealth;
    }

    static float GetLevelUpExp(int level)
    {
        if (0 < level && level < 10)
            return 500;
        if (10 <= level && level < 20)
            return 600;
        if (20 <= level && level < 30)
            return 700;
        if (30 <= level && level < 40)
            return 800;
        if (40 <= level && level < 50)
            return 900;
        return 1000;
    }

    public void TakeDamage(Transform from, float damage)
    {
        if (isInvincible)
            return;
        Debug.Log("Player got damage,=" + damage);
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            GameOver();
        }
        isInvincible = true;
        invincibleTimer = invincibleDuration;
        KnockbackCoroutine(from);
        StartCoroutine(ShowFlashEffect());
    }

    void InvincibleCountDown()
    {
        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer < 0)
                isInvincible = false;
        }
    }

    IEnumerator ShowFlashEffect()
    {
        while (isInvincible)
        {
            sr.material = flashMaterial;
            flashMaterial.SetFloat("_FlashAmount", 0.7f);

            yield return new WaitForSeconds(0.2f / 2);

            flashMaterial.SetFloat("_FlashAmount", 0);
            sr.material = originMaterial;

            yield return new WaitForSeconds(0.2f / 2);
        }
        sr.material = originMaterial;
    }

    void KnockbackCoroutine(Transform target)
    {
        Vector2 direction = (target.position - transform.position).normalized;
        rb.AddForce(-direction * knockbackForce, ForceMode2D.Impulse);
        //yield return new WaitForSeconds(knockbackDuration);
        //rb.linearVelocity = Vector2.zero;
    }

    private void GameOver()
    {
        Debug.Log("You died");
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, pickupRange);
    }
}
