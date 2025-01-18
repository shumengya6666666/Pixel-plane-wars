using UnityEngine;
using System.Collections;

public class SpeedItem : Item
{
    public float speedBoost = 1.5f; // 增加的速度倍数
    public float duration = 3f; // 持续时间
    private float timer = 0f; // 计时器
    private bool isBoosting = false; // 是否处于加速状态
    private float originalSpeed; // 保存玩家的原始速度

    protected override void Trigger_prop_function(GameObject gameObject)
    {
        PlayerPlane playerPlane = gameObject.GetComponent<PlayerPlane>(); // 获取玩家飞机组件
        if (playerPlane != null && !isBoosting) // 确保加速还没有激活
        {
            isBoosting = true;
            originalSpeed = playerPlane.forwardSpeed; // 保存原始速度
            playerPlane.forwardSpeed *= speedBoost; // 增加玩家的移动速度
            GameManager.Instance.UpdatePlayerHealth(playerPlane.health); // 更新UI
            Debug.Log("拾取了加速道具，速度增加：" + speedBoost);
        }
    }

    void Update()
    {
        if (isBoosting)
        {
            timer += Time.deltaTime; // 计时器增加
            if (timer >= duration) // 如果加速时间到了
            {
                ResetSpeed(); // 恢复原始速度
            }
        }
    }

    private void ResetSpeed()
    {
        PlayerPlane playerPlane = FindObjectOfType<PlayerPlane>(); // 获取玩家飞机对象
        if (playerPlane != null)
        {
            playerPlane.forwardSpeed = originalSpeed; // 恢复原始速度
            isBoosting = false; // 停止加速
            timer = 0f; // 重置计时器
            Debug.Log("加速结束，恢复正常速度");
        }
    }
}
