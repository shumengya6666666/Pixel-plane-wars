using UnityEngine;
using UnityEngine.UI;

public class GameStartManager : MonoBehaviour
{
    public GameObject startPanel; // 开始界面的面板
    public Button startGameButton; // 开始游戏按钮

    void Start()
    {
        // 暂停游戏
        Time.timeScale = 0;

        // 确保面板显示
        startPanel.SetActive(true);

        // 为按钮添加点击事件
        startGameButton.onClick.AddListener(OnStartGameClicked);
    }

    void OnStartGameClicked()
    {
        // 恢复游戏
        Time.timeScale = 1;

        // 隐藏开始界面
        startPanel.SetActive(false);
    }
}
