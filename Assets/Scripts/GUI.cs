using System.Collections;
using System.Collections.Generic;
using TMPro;  // ���� TextMeshPro �����ռ�
using UnityEngine;

public class GUI : MonoBehaviour
{
    private TextMeshProUGUI Player_Pos;  // �洢 TextMeshProUGUI ���

    void Start()
    {
        // ʹ�� GetComponent ��ȡ TextMeshProUGUI ���
        Player_Pos = GameObject.Find("Player_Pos")?.GetComponent<TextMeshProUGUI>();
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
        }
    }
}
