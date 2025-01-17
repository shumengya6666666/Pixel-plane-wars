using System.Collections;
using System.Collections.Generic;
using TMPro;  // 引入 TextMeshPro 命名空间
using UnityEngine;

public class GUI : MonoBehaviour
{
    private TextMeshProUGUI Player_Pos;  // 存储 TextMeshProUGUI 组件
    private TextMeshProUGUI Enemy_Num;
    private TextMeshProUGUI Player_Health;
    private TextMeshProUGUI Player_Money;
    private TextMeshProUGUI Player_Experience;
    private TextMeshProUGUI Player_Level;

    void Start()
    {
        // 使用 GetComponent 获取 TextMeshProUGUI 组件
        Player_Pos = GameObject.Find("Player_Pos")?.GetComponent<TextMeshProUGUI>();
        Enemy_Num = GameObject.Find("Enemy_Num")?.GetComponent<TextMeshProUGUI>();
        Player_Health = GameObject.Find("Health")?.GetComponent<TextMeshProUGUI>();
        Player_Money = GameObject.Find("Money")?.GetComponent<TextMeshProUGUI>();
        Player_Experience = GameObject.Find("Experience")?.GetComponent<TextMeshProUGUI>();
        Player_Level = GameObject.Find("Level")?.GetComponent<TextMeshProUGUI>();
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

            if(Player_Money != null)
            {
                Player_Money.text = "金币："+GameManager.Instance.Money;
            }

            if (Player_Experience != null)
            {
                Player_Experience.text = "经验：" + GameManager.Instance.PlayerExperience;
            }

            if (Player_Level != null) {
                Player_Level.text = "等级：" + GameManager.Instance.PlayerLevel;
            }

        }
    }
}
