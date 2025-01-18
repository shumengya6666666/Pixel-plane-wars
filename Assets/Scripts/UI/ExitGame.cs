using UnityEngine;
using UnityEngine.UI;

public class ExitGame : MonoBehaviour
{
    // 退出按钮的引用
    public Button exitButton;

    void Start()
    {
        // 为按钮添加点击事件监听
        exitButton.onClick.AddListener(OnExitButtonClick);
    }

    // 点击按钮时退出游戏
    void OnExitButtonClick()
    {
        // 确保退出游戏时的提示（如在编辑模式下退出时）
#if UNITY_EDITOR
        // 在编辑模式下停止播放
        UnityEditor.EditorApplication.isPlaying = false;
#else
            // 在构建的游戏中退出
            Application.Quit();
#endif
    }
}
