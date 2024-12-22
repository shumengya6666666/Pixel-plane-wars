using UnityEngine;

public class StartGameButton : MonoBehaviour
{
    // 需要在Inspector中分配这些对象
    public GameObject startMenuUI; // 开始菜单的UI对象
    public GameObject gameUI;      // 游戏主界面的UI对象

    // 这个方法将由按钮的OnClick事件调用
    public void OnStartButtonClick()
    {
        // 检查UI对象是否已分配
        if (startMenuUI != null && gameUI != null)
        {
            // 隐藏开始菜单
            startMenuUI.SetActive(false);

            // 显示游戏UI
            gameUI.SetActive(true);
        }
        else
        {
            Debug.LogError("UI objects not assigned in the Inspector.");
        }
    }
}