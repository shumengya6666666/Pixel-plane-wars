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


            if (playerLevelText != null)
            {
                playerLevelText.text = "�ȼ�: " + GameManager.Instance.PlayerLevel;
            }

            if (playerMoneyText != null)
            {
                playerMoneyText.text = "��Ǯ: " + GameManager.Instance.Money;
            }
        }
    }

}

