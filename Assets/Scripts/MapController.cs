using UnityEngine;
using UnityEngine.Tilemaps;

public class MapController : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] LayerMask terrianLayer;
    [SerializeField] float MapCheckRadius;
    [SerializeField] GameObject TerrianChunk;

    PlayerMovement playerMovement;
    Vector2 newChunkPos;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerMovement = FindAnyObjectByType<PlayerMovement>();

    }

    // Update is called once per frame
    void Update()
    {
        CheckForwardingTerrian();
    }

    void CheckForwardingTerrian()
    {
        //移动的时候
        if (playerMovement.moveDir.x != 0 || playerMovement.moveDir.y != 0)
        {
            //预期位置是移动方向+地图块大小
            newChunkPos = new Vector2(player.position.x, player.position.y) + playerMovement.moveDir * MapCheckRadius;
            newChunkPos = AlignNewChunkPos(newChunkPos, MapCheckRadius * 2);
            Debug.Log("newChunkPos=" + newChunkPos);
            Collider2D[] existedChunks = Physics2D.OverlapCircleAll(newChunkPos, MapCheckRadius/2, terrianLayer);
            if (existedChunks.Length == 0)
            {
                Instantiate(TerrianChunk, newChunkPos, Quaternion.identity).layer = terrianLayer;
            }
        }
    }

    //对齐生成的地图位置到网格
    Vector2 AlignNewChunkPos(Vector2 pos, float gridSize)
    {
        float x = Mathf.Round(pos.x / gridSize) * gridSize;
        float y = Mathf.Round(pos.y / gridSize) * gridSize;
        return new Vector2 (x, y);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(newChunkPos, MapCheckRadius/2);
    }
}
