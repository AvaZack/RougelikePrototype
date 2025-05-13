using System.Collections;
using UnityEngine;

public class CollectibleItem : MonoBehaviour
{
    public float attractRadius = 3f;
    public float flySpeed = 5f;

    public ItemType itemType;
    public int value = 10;

    private Transform player;
    private bool isAttracting = false;

    public ParticleSystem collectEffect;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (!isAttracting && Vector2.Distance(transform.position, player.position) <= attractRadius)
        {
            StartCoroutine(FlyToPlayer());
        }
    }

    IEnumerator FlyToPlayer()
    {
        isAttracting = true;
        float journey = 0f;
        Vector3 startPos = transform.position;

        while (journey <= 1f)
        {
            journey += Time.deltaTime * flySpeed;
            transform.position = Vector3.Lerp(startPos, player.position, journey);
            yield return null;
        }

        if (collectEffect != null)
        {
            collectEffect.Play();
            yield return new WaitForSeconds(collectEffect.main.duration);
        }

        Destroy(gameObject);
    }

    public enum ItemType
    {
        Health,
        Experience,
        Coin
    }
}