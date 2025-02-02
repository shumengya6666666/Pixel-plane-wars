using UnityEngine;
using System.Collections;
using static PlayerPlane;

public class HealthRecovery : Item
{

    protected override void Trigger_prop_function(GameObject gameObject)
    {
        PlayerPlane playerPlane = gameObject.GetComponent<PlayerPlane>(); // 获取玩家飞机组件
        if (playerPlane != null)
        {
            playerPlane.AddBuff(BuffType.HealthRegen, 10f); // 持续5秒的速度提升
        }
    }

}


