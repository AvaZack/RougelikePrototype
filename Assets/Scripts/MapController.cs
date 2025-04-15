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
            Debug.Log("newChunkPos=" + newChunkPos);
            Collider2D[] existedChunks = Physics2D.OverlapCircleAll(newChunkPos, MapCheckRadius, terrianLayer);
            if (existedChunks.Length == 0)
            {
                Instantiate(TerrianChunk, newChunkPos, Quaternion.identity).layer = terrianLayer;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(player.position, newChunkPos);

        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(newChunkPos, MapCheckRadius);
    }
}
