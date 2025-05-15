using System.Collections;
using System.Collections.Generic;
using System.Net;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemySpawnController : MonoBehaviour
{

    [SerializeField] Transform player;
    [SerializeField] List<GameObject> enemyPrefabs;
    [SerializeField] int enemiesPerWave;
    [SerializeField] float spawnRange;
    [SerializeField] float spawnInterval;

    float enemySpawnTimer = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        EnemySpawn();
    }

    void EnemySpawn()
    {
        enemySpawnTimer += Time.deltaTime;
        if (enemySpawnTimer > spawnInterval)
        {
            for (int i = 0; i < enemiesPerWave; i++)
            {
                Vector2 randomCircle = Random.insideUnitCircle.normalized * spawnRange;
                Vector2 spawnPos = (Vector2)player.position + randomCircle;
                int idx = Random.Range(0, enemyPrefabs.Count);
                GameObject enemy = Instantiate(enemyPrefabs[idx], spawnPos, Quaternion.identity);
                enemy.tag = "Enemy";
                enemy.layer = LayerMask.NameToLayer("Enemy");
            }
            enemySpawnTimer = 0;
        }
    }


}
