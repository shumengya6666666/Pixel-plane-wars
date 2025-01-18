using UnityEngine;
using System.Collections;

public class SpeedItem : Item
{
    public float speedBoost = 2f; // 增加的速度倍数
    public float duration = 5f; // 持续时间
    private float timer = 0f; // 计时器
    private bool isBoosting = false; // 是否处于加速状态

    protected override void Trigger_prop_function(GameObject gameObject)
    {
        PlayerPlane playerPlane = gameObject.GetComponent<PlayerPlane>(); // 获取玩家飞机组件
        if (playerPlane != null && !isBoosting) // 确保加速还没有激活
        {
            isBoosting = true;
            playerPlane.forwardSpeed *= speedBoost; // 增加玩家的移动速度
            GameManager.Instance.UpdatePlayerHealth(playerPlane.health); // 更新UI
            Debug.Log("拾取了加速道具，速度增加：" + speedBoost);
            timer = 0f; // 重置计时器
        }
    }

    void Update()
    {
        if (isBoosting)
        {
            timer += Time.deltaTime; // 增加计时器
            if (timer >= duration)
            {
                PlayerPlane playerPlane = FindObjectOfType<PlayerPlane>(); // 获取玩家飞机
                if (playerPlane != null)
                {
                    playerPlane.forwardSpeed /= speedBoost; // 恢复速度
                    Debug.Log("加速效果结束，速度恢复正常");
                    isBoosting = false; // 结束加速状态
                }
            }
        }
    }
}
