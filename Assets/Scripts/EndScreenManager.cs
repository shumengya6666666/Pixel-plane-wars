using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // 仍然需要引用 UnityEngine.UI，因为按钮组件属于这个命名空间
using TMPro; // 引用 TextMeshPro 命名空间

public class EndScreenManager : MonoBehaviour
{
    public Button returnToMainMenuButton; // 按钮引用

    private void Start()
    {
        // 添加按钮点击事件监听器
        if (returnToMainMenuButton != null)
        {
            returnToMainMenuButton.onClick.AddListener(ReturnToMainMenu);
        }
        else
        {
            Debug.LogError("Return to Main Menu Button is not assigned.");
        }
    }

    private void ReturnToMainMenu()
    {
        // 恢复游戏时间（如果在游戏结束时暂停了时间）
        Time.timeScale = 0;

        // 加载主菜单场景
        SceneManager.LoadScene("Main");
    }
}