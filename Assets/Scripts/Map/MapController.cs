using System.Collections;
using System.Collections.Generic;
using System.Net;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapController : MonoBehaviour
{
    [Header("Terrian")]
    [SerializeField] Transform player;
    [SerializeField] float MapCheckRadius;
    [SerializeField] GameObject TerrianChunk;
    [SerializeField] float deactiveDist;
    [SerializeField] float deactiveInterval;

    [Header("Enemy")]
    [SerializeField] List<GameObject> enemyPrefabs;
    [SerializeField] int enemiesPerWave;
    [SerializeField] float spawnRange;
    [SerializeField] float spawnInterval;

    PlayerMovement playerMovement;
    Vector3 newChunkPos;
    List<GameObject> TerrianChunks;
    LayerMask terrianLayer;
    float deactiveTimer = 0;
    float enemySpawnTimer = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerMovement = FindAnyObjectByType<PlayerMovement>();
        TerrianChunks = new List<GameObject>();
        terrianLayer = LayerMask.GetMask("Terrian");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        TerrianGenerate();
        CheckTooFarChunk();

        EnemySpawn();
    }

    void TerrianGenerate()
    {
        //预期位置是移动方向+地图块大小
        newChunkPos = new Vector3(player.position.x, player.position.y) + playerMovement.moveDir * MapCheckRadius;
        newChunkPos = AlignNewChunkPos(newChunkPos, MapCheckRadius * 2);
        Collider2D[] existedChunks = Physics2D.OverlapCircleAll(newChunkPos, MapCheckRadius / 2, terrianLayer);
        if (existedChunks.Length == 0)
        {
            GameObject newChunk = Instantiate(TerrianChunk, newChunkPos, Quaternion.identity);
            newChunk.layer = terrianLayer;
            TerrianChunks.Add(newChunk);
        }
    }

    //对齐生成的地图位置到网格
    Vector3 AlignNewChunkPos(Vector3 pos, float gridSize)
    {
        float x = Mathf.Round(pos.x / gridSize) * gridSize;
        float y = Mathf.Round(pos.y / gridSize) * gridSize;
        return new Vector3(x, y);
    }

    //检测距离过远的地图块并禁用
    void CheckTooFarChunk()
    {
        if (deactiveTimer > deactiveInterval)
        {
            List<GameObject> chunksToRemove = new List<GameObject>();
            foreach (GameObject chunk in TerrianChunks)
            {
                if (Vector3.Distance(player.position, chunk.transform.position) > deactiveDist)
                {
                    chunk.SetActive(false);
                    chunksToRemove.Add(chunk);
                }
            }
            TerrianChunks.RemoveAll(o => chunksToRemove.Contains(o));
            deactiveTimer = 0;
        }
        deactiveTimer += Time.deltaTime;
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
                enemy.layer = LayerMask.GetMask("Enemy");
            }
            enemySpawnTimer = 0;
        }
    }


    private void OnDrawGizmos()
    {
        //Gizmos.color = Color.red;
        //Gizmos.DrawWireSphere(newChunkPos, MapCheckRadius / 2);
    }
}
