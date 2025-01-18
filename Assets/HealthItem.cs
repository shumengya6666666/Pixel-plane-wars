using UnityEngine;

public class HealthItem : Item
{
    public int healthIncrease = 20; // 增加的生命值

    protected override void Trigger_prop_function(GameObject gameObject)
    {
        PlayerPlane playerPlane = gameObject.GetComponent<PlayerPlane>(); // 获取玩家飞机组件
        if (playerPlane != null)
        {
            playerPlane.health += healthIncrease; // 增加生命
            GameManager.Instance.UpdatePlayerHealth(playerPlane.health); // 更新UI
            Debug.Log("拾取了生命道具，增加生命值：" + healthIncrease);
        }
    }
}
