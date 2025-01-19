using UnityEngine;

public class ItemSpawnArea : MonoBehaviour
{
    public float spawnAreaSize = 10f; // 生成区域的边长
    public float spawnInterval = 1f;  // 生成间隔时间
    public GameObject[] itemPrefabs;  // 特殊道具的预制体数组
    public Transform ItemParent;

    private int itemNumber = 0;       // 道具的数量
    private float probability = 0.5f; // 假设物品生成概率为 50%

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
        float y = Random.Range(-spawnAreaSize / 2f, spawnAreaSize / 2f);

        // 生成物品的随机位置
        Vector3 randomPosition = new Vector3(x, y, 0f) + transform.position;

        // 输出生成的位置
        Debug.Log("下一个道具生成在: " + randomPosition);

        // 从道具预制体数组中随机选择一个
        int index = Random.Range(0, itemPrefabs.Length);
        GameObject selectedPrefab = itemPrefabs[index];

        // 获取物品对象的 Item 组件，并传递生成概率
        Item item = selectedPrefab.GetComponent<Item>();
        if (item != null)
        {
            probability = item.Generation_Probability; // 获取物品的生成概率
        }

        // 生成物品的概率
        float generationProbability = Random.Range(0f, 1f); // 生成一个 [0, 1) 之间的随机值

        // 判断是否生成物品
        if (generationProbability <= probability)
        {
            GameObject Item = Instantiate(selectedPrefab, randomPosition, Quaternion.identity);
            Item.transform.SetParent(ItemParent);  // 设置父物体为 Enemys
            // 生成物品数量增加
            itemNumber++;
            GameManager.Instance.ItemNumber = itemNumber;
        }
        else
        {
            Debug.Log("这次没有生成物品。");
        }
    }
}
