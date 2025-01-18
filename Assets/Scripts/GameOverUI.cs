using UnityEngine;
using TMPro;  // ���� TextMeshPro �����ռ�

public class GameOverUI : MonoBehaviour
{
    public TMP_Text survivalTimeText;  // ʹ�� TMP_Text ��� Text
    public TMP_Text playerLevelText;
    public TMP_Text playerMoneyText;

    private void Start()
    {
        // ��� GameManager ʵ���Ƿ���ڣ�Ȼ���ȡ����
        if (GameManager.Instance != null)
        {
            // ��� TextMeshPro ����Ƿ��Ѹ�ֵ
            if (survivalTimeText != null)
            {
                survivalTimeText.text = "���ʱ��: " + GameManager.Instance.Survival_Time;
            }
            else
            {
                Debug.LogError("survivalTimeText .");
            }

            if (playerLevelText != null)
            {
                playerLevelText.text = "�ȼ�: " + GameManager.Instance.PlayerLevel;
            }
            else
            {
                Debug.LogError("playerLevelText is not assigned in the Inspector.");
            }

            if (playerMoneyText != null)
            {
                playerMoneyText.text = "��Ǯ: " + GameManager.Instance.Money;
            }
            else
            {
                Debug.LogError("playerMoneyText is not assigned in the Inspector.");
            }
        }
        else
        {
            Debug.LogError("GameManager instance not found.");
        }
    }

}

