using System.Collections.Generic;
using System.Net;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapController : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] LayerMask terrianLayer;
    [SerializeField] float MapCheckRadius;
    [SerializeField] GameObject TerrianChunk;
    [SerializeField] float deactiveDist;
    [SerializeField] float deactiveInterval;

    PlayerMovement playerMovement;
    Vector2 newChunkPos;
    List<GameObject> TerrianChunks;
    float deactiveTimer = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerMovement = FindAnyObjectByType<PlayerMovement>();
        TerrianChunks = new List<GameObject>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        TerrianGenerate();
        CheckTooFarChunk();
    }

    void TerrianGenerate()
    {
        //预期位置是移动方向+地图块大小
        newChunkPos = new Vector2(player.position.x, player.position.y) + playerMovement.moveDir * MapCheckRadius;
        newChunkPos = AlignNewChunkPos(newChunkPos, MapCheckRadius * 2);
        Debug.Log("newChunkPos=" + newChunkPos);
        Collider2D[] existedChunks = Physics2D.OverlapCircleAll(newChunkPos, MapCheckRadius / 2, terrianLayer);
        if (existedChunks.Length == 0)
        {
            GameObject newChunk = Instantiate(TerrianChunk, newChunkPos, Quaternion.identity);
            newChunk.layer = terrianLayer;
            TerrianChunks.Add(newChunk);
        }
    }

    //对齐生成的地图位置到网格
    Vector2 AlignNewChunkPos(Vector2 pos, float gridSize)
    {
        float x = Mathf.Round(pos.x / gridSize) * gridSize;
        float y = Mathf.Round(pos.y / gridSize) * gridSize;
        return new Vector2(x, y);
    }

    //检测距离过远的地图块并禁用
    void CheckTooFarChunk()
    {
        if (deactiveTimer > deactiveInterval)
        {
            foreach (GameObject chunk in TerrianChunks)
            {
                if (Vector2.Distance(player.position, chunk.transform.position) > deactiveDist)
                {
                    chunk.SetActive(false);
                    TerrianChunks.Remove(chunk);
                }
            }
            deactiveTimer = 0;
        }
        deactiveTimer += Time.deltaTime;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(newChunkPos, MapCheckRadius / 2);
    }
}
