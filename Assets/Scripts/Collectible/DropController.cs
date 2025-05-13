using UnityEngine;
using System.Collections.Generic;

public class DropController : MonoBehaviour
{
    [System.Serializable]
    public class DropItem
    {
        public GameObject itemPrefab;  // 掉落物预制体
        [Tooltip("掉落概率 (0~1)")]
        [Range(0, 1)] public float dropChance = 0.3f; // 独立概率
        [Tooltip("最小/最大掉落数量")]
        public Vector2Int dropAmount = Vector2Int.one;
    }

    [Header("基础设置")]
    [Tooltip("总掉落概率 (0=无掉落, 1=必掉)")]
    [Range(0, 1)] public float globalDropChance = 0.5f;
    public bool useIndependentProbability = true; // 是否使用独立概率

    [Header("掉落物品列表")]
    public List<DropItem> dropTable = new List<DropItem>();

    [Header("高级设置")]
    public Vector2 positionOffset = new Vector2(0, 0.5f); // 生成位置偏移
    public float dropRadius = 1f; // 掉落物散布半径

    // 当对象被销毁时调用（由敌人/地形脚本触发）
    public void OnDeath()
    {
        if (Random.value > globalDropChance) return;

        if (useIndependentProbability)
        {
            DropWithIndependentChance();
        }
        else
        {
            DropWithWeightedRandom();
        }
    }

    // 独立概率掉落方式（每个物品单独计算）
    private void DropWithIndependentChance()
    {
        foreach (var item in dropTable)
        {
            if (Random.value <= item.dropChance)
            {
                SpawnItem(item);
            }
        }
    }

    // 权重随机掉落方式（根据概率权重选择一项）
    private void DropWithWeightedRandom()
    {
        float totalWeight = 0;
        foreach (var item in dropTable)
        {
            totalWeight += item.dropChance;
        }

        float randomPoint = Random.value * totalWeight;
        float currentWeight = 0;

        foreach (var item in dropTable)
        {
            currentWeight += item.dropChance;
            if (randomPoint <= currentWeight)
            {
                SpawnItem(item);
                break; // 只掉落一件物品
            }
        }
    }

    // 生成掉落物
    private void SpawnItem(DropItem item)
    {
        int amount = Random.Range(item.dropAmount.x, item.dropAmount.y + 1);

        for (int i = 0; i < amount; i++)
        {
            Vector2 spawnPos = (Vector2)transform.position + positionOffset;
            spawnPos += Random.insideUnitCircle * dropRadius;

            Instantiate(item.itemPrefab, spawnPos, Quaternion.identity);
        }
    }

    // 编辑器可视化
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position + (Vector3)positionOffset, 0.2f);
        Gizmos.DrawWireSphere(transform.position + (Vector3)positionOffset, dropRadius);
    }
}