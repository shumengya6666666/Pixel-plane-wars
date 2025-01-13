using System.Collections;
using System.Collections.Generic;
using TMPro;  // 引入 TextMeshPro 命名空间
using UnityEngine;

public class GUI : MonoBehaviour
{
    private TextMeshProUGUI Player_Pos;  // 存储 TextMeshProUGUI 组件

    void Start()
    {
        // 使用 GetComponent 获取 TextMeshProUGUI 组件
        Player_Pos = GameObject.Find("Player_Pos")?.GetComponent<TextMeshProUGUI>();
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
        }
    }
}
