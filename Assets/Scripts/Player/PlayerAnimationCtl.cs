using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    Animator animator;
    PlayerController movement;
    SpriteRenderer spriteRenderer;
    bool isFacingRight;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        movement = GetComponent<PlayerController>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("move", movement.moveDir.x != 0 || movement.moveDir.y != 0);
        if (movement.moveDir.x != 0) {
            isFacingRight = movement.moveDir.x > 0;
        }
        spriteRenderer.flipX = !isFacingRight;
    }
}
