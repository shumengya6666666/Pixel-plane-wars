using UnityEngine;
using System.Collections;
using static PlayerPlane;

public class MiniItem : Item
{

    protected override void Trigger_prop_function(GameObject gameObject)
    {
        PlayerPlane playerPlane = gameObject.GetComponent<PlayerPlane>(); // 获取玩家飞机组件
        if (playerPlane != null)
        {
            playerPlane.AddBuff(BuffType.MiniBoost, 3f); // 持续5秒的速度提升
        }
    }

}


