using System.Collections;
using UnityEngine;

public class CollectibleItem : MonoBehaviour
{
    public float attractRadius = 3f;
    public float flySpeed = 5f;

    public ItemType itemType;
    public int value = 10;

    private PlayerController player;
    private bool isAttracting = false;

    void Start()
    {
        player = FindAnyObjectByType<PlayerController>();
    }

    void Update()
    {
        if (!isAttracting && Vector2.Distance(transform.position, player.transform.position) <= attractRadius)
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
            transform.position = Vector3.Lerp(startPos, player.transform.position, journey);
            yield return null;
        }
        player.ApplyItemEffect(this);
        Destroy(gameObject);
    }

    public enum ItemType
    {
        Health,
        Experience,
        Coin
    }
}