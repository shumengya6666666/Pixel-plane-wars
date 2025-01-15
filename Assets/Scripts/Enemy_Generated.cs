using UnityEngine;

public class EnemySpawnArea : MonoBehaviour
{
    public float spawnRadius = 10f;  // 生成区域半径
    public float spawnInterval = 1f; // 生成间隔时间
    public GameObject enemyPrefab;   // 敌人飞机的预制体
    private int EnemyNumber = 0;

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

        // 生成敌人飞机
        Vector3 randomPosition = new Vector3(x, y, 0f);

        // 输出生成的位置
        Debug.Log("下一个敌人生成在: " + randomPosition);
        EnemyNumber++;
        GameManager.Instance.UpdateEnemyNumber(EnemyNumber);

        // 实例化敌人飞机
        Instantiate(enemyPrefab, randomPosition, Quaternion.identity);
    }


}
