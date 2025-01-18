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
            // �� GameManager ��ȡ���ݲ���ʾ�� UI ��
            survivalTimeText.text = "���ʱ��: " + GameManager.Instance.Survival_Time;
            playerLevelText.text = "�ȼ�: " + GameManager.Instance.PlayerLevel;
            playerMoneyText.text = "��Ǯ: " + GameManager.Instance.Money;
        }
        else
        {
            Debug.LogError("GameManager instance not found.");
        }
    }
}

