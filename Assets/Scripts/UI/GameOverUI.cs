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
            // 检查 TextMeshPro 组件是否已赋值
            if (survivalTimeText != null)
            {
                survivalTimeText.text = "存活时间: " + GameManager.Instance.Survival_Time;
            }


            if (playerLevelText != null)
            {
                playerLevelText.text = "等级: " + GameManager.Instance.PlayerLevel;
            }

            if (playerMoneyText != null)
            {
                playerMoneyText.text = "金钱: " + GameManager.Instance.Money;
            }
        }
    }

}

