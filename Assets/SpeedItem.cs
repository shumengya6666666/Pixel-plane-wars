using UnityEngine;
using System.Collections;


public class SpeedItem : Item
{
    public float speedBoost = 2f; // 增加的速度倍数
    public float duration = 5f; // 持续时间

    protected override void Trigger_prop_function(GameObject gameObject)
    {
        PlayerPlane playerPlane = gameObject.GetComponent<PlayerPlane>(); // 获取玩家飞机组件
        if (playerPlane != null)
        {
            playerPlane.forwardSpeed *= speedBoost; // 增加玩家的移动速度
            GameManager.Instance.UpdatePlayerHealth(playerPlane.health); // 更新UI
            Debug.Log("拾取了加速道具，速度增加：" + speedBoost);

            // 恢复默认速度
            StartCoroutine(ResetSpeedAfterDuration(playerPlane));
        }
    }

    private IEnumerator ResetSpeedAfterDuration(PlayerPlane playerPlane)
    {
        yield return new WaitForSeconds(duration);
        playerPlane.forwardSpeed /= speedBoost; // 恢复速度
        Debug.Log("加速效果结束，速度恢复正常");
    }
}
