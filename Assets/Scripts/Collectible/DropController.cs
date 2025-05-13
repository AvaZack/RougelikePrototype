using UnityEngine;
using System.Collections.Generic;

public class DropController : MonoBehaviour
{
    [System.Serializable]
    public class DropItem
    {
        public GameObject itemPrefab;  // ������Ԥ����
        [Tooltip("������� (0~1)")]
        [Range(0, 1)] public float dropChance = 0.3f; // ��������
        [Tooltip("��С/����������")]
        public Vector2Int dropAmount = Vector2Int.one;
    }

    [Header("��������")]
    [Tooltip("�ܵ������ (0=�޵���, 1=�ص�)")]
    [Range(0, 1)] public float globalDropChance = 0.5f;
    public bool useIndependentProbability = true; // �Ƿ�ʹ�ö�������

    [Header("������Ʒ�б�")]
    public List<DropItem> dropTable = new List<DropItem>();

    [Header("�߼�����")]
    public Vector2 positionOffset = new Vector2(0, 0.5f); // ����λ��ƫ��
    public float dropRadius = 1f; // ������ɢ���뾶

    // ����������ʱ���ã��ɵ���/���νű�������
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

    // �������ʵ��䷽ʽ��ÿ����Ʒ�������㣩
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

    // Ȩ��������䷽ʽ�����ݸ���Ȩ��ѡ��һ�
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
                break; // ֻ����һ����Ʒ
            }
        }
    }

    // ���ɵ�����
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

    // �༭�����ӻ�
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position + (Vector3)positionOffset, 0.2f);
        Gizmos.DrawWireSphere(transform.position + (Vector3)positionOffset, dropRadius);
    }
}