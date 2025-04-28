using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PropRandomizer : MonoBehaviour
{
    [SerializeField] List<GameObject> propPrefabs;
    [SerializeField] GameObject PropChunks;
    [SerializeField] float mapRadius;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        RandomizeProps();
    }

    // Update is called once per frame
    void Update()
    {

    }

    //随机放置物品
    void RandomizeProps()
    {
        int propCount = Random.Range(25, 30);

        float x, y;
        for (int i = 0; i < propCount; i++)
        {
            x = Random.Range(transform.position.x - mapRadius, transform.position.x + mapRadius);
            y = Random.Range(transform.position.y - mapRadius, transform.position.y + mapRadius);
            Instantiate(propPrefabs[Random.Range(0, propPrefabs.Count)], new Vector2(x, y), Quaternion.identity, PropChunks.transform);
        }
    }
}
