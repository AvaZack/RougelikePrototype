using UnityEngine;

public class BatController : EnemyController
{
    private PlayerController player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();
        player = FindAnyObjectByType<PlayerController>();
        animator.SetFloat("offset", Random.Range(0f, 1f));
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
    }
}
