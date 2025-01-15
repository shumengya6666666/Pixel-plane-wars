using System.Collections;
using System.Collections.Generic;
using TMPro;  // ���� TextMeshPro �����ռ�
using UnityEngine;

public class GUI : MonoBehaviour
{
    private TextMeshProUGUI Player_Pos;  // �洢 TextMeshProUGUI ���
    private TextMeshProUGUI Enemy_Num;
    private TextMeshProUGUI Player_Health;

    void Start()
    {
        // ʹ�� GetComponent ��ȡ TextMeshProUGUI ���
        Player_Pos = GameObject.Find("Player_Pos")?.GetComponent<TextMeshProUGUI>();
        Enemy_Num = GameObject.Find("Enemy_Num")?.GetComponent<TextMeshProUGUI>();
        Player_Health = GameObject.Find("Health")?.GetComponent<TextMeshProUGUI>();
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
        }
    }
}
