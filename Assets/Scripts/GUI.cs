using System.Collections;
using System.Collections.Generic;
using TMPro;  // 引入 TextMeshPro 命名空间
using UnityEngine;

public class GUI : MonoBehaviour
{
    private TextMeshProUGUI Player_Pos;  // 存储 TextMeshProUGUI 组件
    private TextMeshProUGUI Enemy_Num;
    private TextMeshProUGUI Player_Health;

    void Start()
    {
        // 使用 GetComponent 获取 TextMeshProUGUI 组件
        Player_Pos = GameObject.Find("Player_Pos")?.GetComponent<TextMeshProUGUI>();
        Enemy_Num = GameObject.Find("Enemy_Num")?.GetComponent<TextMeshProUGUI>();
        Player_Health = GameObject.Find("Health")?.GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        // 获取全局的玩家位置
        if (GameManager.Instance != null)
        {
            Vector3 playerPosition = GameManager.Instance.PlayerPosition;

            // 更新UI显示玩家位置
            if (Player_Pos != null)
            {
                Player_Pos.text = "玩家坐标: (" + Mathf.RoundToInt(playerPosition.x) + "," + Mathf.RoundToInt(playerPosition.y) + ")";
                //Player_Pos.text = "玩家坐标: (" + playerPosition.x.ToString("F0") + "," + playerPosition.y.ToString("F0") + ")";

            }

            if (Enemy_Num != null)
            {
                Enemy_Num.text = "敌人数量: "+ GameManager.Instance.EnemyNumvber;
            }

            if (Player_Health != null)
            {
                Player_Health.text = "生命值: " + GameManager.Instance.PlayerHealth;
            }
        }
    }
}
