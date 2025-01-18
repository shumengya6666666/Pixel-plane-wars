using UnityEngine;
using TMPro;  // 导入 TextMeshPro 命名空间

public class GameOverUI : MonoBehaviour
{
    public TMP_Text survivalTimeText;  // 使用 TMP_Text 替代 Text
    public TMP_Text playerLevelText;
    public TMP_Text playerMoneyText;

    private void Start()
    {
        // 检查 GameManager 实例是否存在，然后获取数据
        if (GameManager.Instance != null)
        {
            // 从 GameManager 获取数据并显示在 UI 上
            survivalTimeText.text = "存活时间: " + GameManager.Instance.Survival_Time;
            playerLevelText.text = "等级: " + GameManager.Instance.PlayerLevel;
            playerMoneyText.text = "金钱: " + GameManager.Instance.Money;
        }
        else
        {
            Debug.LogError("GameManager instance not found.");
        }
    }
}

