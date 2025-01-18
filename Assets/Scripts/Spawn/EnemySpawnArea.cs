using UnityEngine;

public class EnemySpawnArea : MonoBehaviour
{
    public float spawnRadius = 10f;  // 生成区域半径
    public float spawnInterval = 1f; // 生成间隔时间
    public GameObject[] enemyPrefabs;  // 敌人预制体数组
    private int EnemyNumber = 0;
    private float Probability;

    void Start()
    {
        // 定期生成敌人飞机
        InvokeRepeating("SpawnEnemy", 0f, spawnInterval);
    }

    void OnDrawGizmos()
    {
        // 设置 Gizmo 颜色
        Gizmos.color = Color.red;

        // 绘制一个半径为 spawnRadius 的圆形，表示敌人生成的区域
        Gizmos.DrawWireSphere(transform.position, spawnRadius);
    }

    public void SpawnEnemy()
    {
        // 随机角度和半径
        float angle = Random.Range(0f, 2 * Mathf.PI);  // 随机角度
        float radius = Random.Range(0f, spawnRadius);  // 随机半径，确保不超出圆形区域

        // 转换为笛卡尔坐标系 (X, Z)
        float x = transform.position.x + radius * Mathf.Cos(angle);
        float y = transform.position.y + radius * Mathf.Sin(angle);

        // 生成敌人飞机的随机位置
        Vector3 randomPosition = new Vector3(x, y, 0f);

        // 输出生成的位置
        Debug.Log("下一个敌人生成在: " + randomPosition);

        GameManager.Instance.UpdateEnemyNumber(EnemyNumber);

        // 从敌人预制体数组中随机选择一个
        int index = Random.Range(0, enemyPrefabs.Length);
        GameObject selectedPrefab = enemyPrefabs[index];

        // 获取敌人对象的 EnemyPlane 组件，并传递生成概率
        EnemyPlane enemyPlane = selectedPrefab.GetComponent<EnemyPlane>();
        if (enemyPlane != null)
        {
            Probability = enemyPlane.Generation_Probability;
        }

        // 生成敌人的概率
        float generationProbability = Random.Range(0f, 1f); // 生成一个 [0, 1) 之间的随机值

        // 假设敌人生成概率为 50%
        if (generationProbability <= Probability)
        {
            // 实例化敌人飞机
            Instantiate(selectedPrefab, randomPosition, Quaternion.identity);
            // 生成敌人数量增加
            EnemyNumber++;
        }
        else
        {
            Debug.Log("这次没有生成敌人。");
        }
    }
}
