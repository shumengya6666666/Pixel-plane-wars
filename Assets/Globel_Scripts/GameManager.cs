using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }  // 单例模式
    public Vector3 PlayerPosition { get; private set; }  // 存储玩家位置

    void Awake()
    {
        // 确保只有一个 GameManager 实例
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);  // 如果已经有一个实例，销毁当前实例
        }

        // 不销毁该物体，以便在场景切换时仍然存在
        DontDestroyOnLoad(gameObject);
    }

    public void UpdatePlayerPosition(Vector3 newPosition)
    {
        PlayerPosition = newPosition;  // 更新玩家位置
    }
}
