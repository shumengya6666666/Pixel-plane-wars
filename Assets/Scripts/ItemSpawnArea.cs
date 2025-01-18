using UnityEngine;
using static UnityEditor.Progress;

public class ItemSpawnArea : MonoBehaviour
{
    public float spawnAreaSize = 10f; // 生成区域的边长
    public float spawnInterval = 1f;  // 生成间隔时间
    public GameObject itemPrefab;     // 特殊物品的预制体
    private int itemNumber = 0;       // 物品的数量
    private float probability;

    void Start()
    {
        // 定期生成特殊物品
        InvokeRepeating("SpawnItem", 0f, spawnInterval);
    }

    void OnDrawGizmos()
    {
        // 设置 Gizmo 颜色
        Gizmos.color = Color.green;

        // 绘制一个正方形区域，表示物品生成的区域
        Gizmos.DrawWireCube(transform.position, new Vector3(spawnAreaSize, spawnAreaSize, 0f));
    }

    public void SpawnItem()
    {
        // 随机生成 X 和 Z 坐标，确保在正方形区域内
        float x = Random.Range(-spawnAreaSize / 2f, spawnAreaSize / 2f);
        float z = Random.Range(-spawnAreaSize / 2f, spawnAreaSize / 2f);

        // 生成物品的随机位置
        Vector3 randomPosition = new Vector3(x, 0f, z) + transform.position;

        // 输出生成的位置
        Debug.Log("下一个物品生成在: " + randomPosition);

        // 获取物品对象的 Item 组件，并传递生成概率
        Item item = itemPrefab.GetComponent<Item>();
        if (item != null)
        {
           // probability = item.Generation_Probability;
        }

        // 生成物品的概率
        float generationProbability = Random.Range(0f, 1f); // 生成一个 [0, 1) 之间的随机值

        // 假设物品生成概率为 50%
        if (generationProbability <= probability)
        {
            // 实例化物品
            Instantiate(itemPrefab, randomPosition, Quaternion.identity);
            // 生成物品数量增加
            itemNumber++;
        }
        else
        {
            Debug.Log("这次没有生成物品。");
        }
    }
}