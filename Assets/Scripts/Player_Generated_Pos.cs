using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Generated_Pos : MonoBehaviour
{
    public GameObject playerPrefab; // 玩家飞机的预制体
    public Transform[] spawnPoints; // 存储生成点的数组

    private GameObject playerInstance; // 玩家实例

    void Start()
    {
        // 调用函数在随机位置生成玩家飞机
        SpawnPlayerAtRandomPosition();
    }

    void SpawnPlayerAtRandomPosition()
    {
        if (spawnPoints.Length > 0)
        {
            // 随机选择一个生成点
            int randomIndex = Random.Range(0, spawnPoints.Length);
            Transform spawnPoint = spawnPoints[randomIndex];

            // 在选定的位置生成玩家飞机，并获取实例对象
            playerInstance = Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);

            // 输出生成的玩家飞机实例的位置
            Debug.Log("玩家生成位置：" + playerInstance.transform.position);

            // 更新全局玩家位置
            if (GameManager.Instance != null)
            {
                GameManager.Instance.UpdatePlayerPosition(playerInstance.transform.position);
            }
        }
        else
        {
            Debug.LogError("生成点数组为空！");
        }
    }

    void Update()
    {
        // 每帧更新全局玩家位置
        if (playerInstance != null)
        {
            GameManager.Instance.UpdatePlayerPosition(playerInstance.transform.position);
        }
        else
        {
            Debug.LogWarning("玩家飞机实例尚未生成！");
        }
    }
}
