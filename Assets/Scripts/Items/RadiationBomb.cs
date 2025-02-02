using UnityEngine;

public class RadiationBomb : Item
{

    protected override void Trigger_prop_function(GameObject gameObject)
    {
        PlayerPlane playerPlane = gameObject.GetComponent<PlayerPlane>(); // 获取玩家飞机组件
        if (playerPlane != null)
        {
            playerPlane.ShootRadial(18);
            Debug.Log("一轮辐射状的爆炸！");
        }
    }
}
