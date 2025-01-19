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
    private TextMeshProUGUI Player_Survival_Time;
    private TextMeshProUGUI Item_Num;
    private TextMeshProUGUI FPS;
    private float deltaTime = 0.0f;
    private float fps;

    private Transform EnemyBullets;
    private Transform Items;
    private Transform Enemys;

    int EnemyBullets_Num = 0;
    int Items_Num = 0;
    int Enemys_Num = 0;

    void Start()
    {
        EnemyBullets = GameObject.Find("EnemyBullets").transform;
        Items = GameObject.Find("Items").transform;
        Enemys = GameObject.Find("Enemys").transform;



        // 使用 GetComponent 获取 TextMeshProUGUI 组件
        Player_Pos = GameObject.Find("Player_Pos")?.GetComponent<TextMeshProUGUI>();
        Enemy_Num = GameObject.Find("Enemy_Num")?.GetComponent<TextMeshProUGUI>();
        Player_Health = GameObject.Find("Health")?.GetComponent<TextMeshProUGUI>();
        Player_Money = GameObject.Find("Money")?.GetComponent<TextMeshProUGUI>();
        Player_Experience = GameObject.Find("Experience")?.GetComponent<TextMeshProUGUI>();
        Player_Level = GameObject.Find("Level")?.GetComponent<TextMeshProUGUI>();
        Player_Survival_Time = GameObject.Find("Timer")?.GetComponent<TextMeshProUGUI>();
        Item_Num = GameObject.Find("Item_Num")?.GetComponent<TextMeshProUGUI>();
        FPS = GameObject.Find("FPS")?.GetComponent<TextMeshProUGUI>();

        // 开始每一秒调用一次 UpdateUI
        InvokeRepeating("UpdateUI", 1f, 1f);
    }

    void UpdateUI()
    {

        EnemyBullets_Num = EnemyBullets.childCount;
        Items_Num = Items.childCount;
        Enemys_Num = Enemys.childCount;

        // 获取全局的玩家位置
        if (GameManager.Instance != null)
        {
            Vector3 playerPosition = GameManager.Instance.PlayerPosition;

            // 更新UI显示玩家位置
            if (Player_Pos != null)
            {
                Player_Pos.text = "(" + Mathf.RoundToInt(playerPosition.x) + "," + Mathf.RoundToInt(playerPosition.y) + ")";
            }

            if (Enemy_Num != null)
            {
                Enemy_Num.text = "" + Enemys_Num;
            }

            if (Player_Health != null)
            {
                Player_Health.text = "" + GameManager.Instance.PlayerHealth;
            }

            if (Player_Money != null)
            {
                Player_Money.text = "" + GameManager.Instance.Money;
            }

            if (Player_Experience != null)
            {
                Player_Experience.text = "" + GameManager.Instance.PlayerExperience;
            }

            if (Player_Level != null)
            {
                Player_Level.text = "" + GameManager.Instance.PlayerLevel;
            }

            if (Player_Survival_Time != null)
            {
                Player_Survival_Time.text = "" + GameManager.Instance.Survival_Time;
            }

            if (Item_Num != null)
            {
                Item_Num.text = "" + Items_Num;
            }

            if (FPS != null)
            {
                FPS.text = "" + Mathf.Ceil(fps).ToString();
            }
        }
    }

    private void Update()
    {
        // 累加每一帧的时间
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;

        // 计算FPS，1/Time.deltaTime
        fps = 1.0f / deltaTime;
    }
}
