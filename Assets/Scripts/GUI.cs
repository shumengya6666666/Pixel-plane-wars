using System.Collections;
using System.Collections.Generic;
using TMPro;  // ���� TextMeshPro �����ռ�
using UnityEngine;

public class GUI : MonoBehaviour
{
    private TextMeshProUGUI Player_Pos;  // �洢 TextMeshProUGUI ���
    private TextMeshProUGUI Enemy_Num;
    private TextMeshProUGUI Player_Health;
    private TextMeshProUGUI Player_Money;
    private TextMeshProUGUI Player_Experience;
    private TextMeshProUGUI Player_Level;

    void Start()
    {
        // ʹ�� GetComponent ��ȡ TextMeshProUGUI ���
        Player_Pos = GameObject.Find("Player_Pos")?.GetComponent<TextMeshProUGUI>();
        Enemy_Num = GameObject.Find("Enemy_Num")?.GetComponent<TextMeshProUGUI>();
        Player_Health = GameObject.Find("Health")?.GetComponent<TextMeshProUGUI>();
        Player_Money = GameObject.Find("Money")?.GetComponent<TextMeshProUGUI>();
        Player_Experience = GameObject.Find("Experience")?.GetComponent<TextMeshProUGUI>();
        Player_Level = GameObject.Find("Level")?.GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        // ��ȡȫ�ֵ����λ��
        if (GameManager.Instance != null)
        {
            Vector3 playerPosition = GameManager.Instance.PlayerPosition;

            // ����UI��ʾ���λ��
            if (Player_Pos != null)
            {
                Player_Pos.text = "�������: (" + Mathf.RoundToInt(playerPosition.x) + "," + Mathf.RoundToInt(playerPosition.y) + ")";
                //Player_Pos.text = "�������: (" + playerPosition.x.ToString("F0") + "," + playerPosition.y.ToString("F0") + ")";

            }

            if (Enemy_Num != null)
            {
                Enemy_Num.text = "��������: "+ GameManager.Instance.EnemyNumvber;
            }

            if (Player_Health != null)
            {
                Player_Health.text = "����ֵ: " + GameManager.Instance.PlayerHealth;
            }

            if(Player_Money != null)
            {
                Player_Money.text = "��ң�"+GameManager.Instance.Money;
            }

            if (Player_Experience != null)
            {
                Player_Experience.text = "���飺" + GameManager.Instance.PlayerExperience;
            }

            if (Player_Level != null) {
                Player_Level.text = "�ȼ���" + GameManager.Instance.PlayerLevel;
            }

        }
    }
}
