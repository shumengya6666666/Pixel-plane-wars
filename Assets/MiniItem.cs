using UnityEngine;
using System.Collections;
using static PlayerPlane;

public class MiniItem : Item
{

    protected override void Trigger_prop_function(GameObject gameObject)
    {
        PlayerPlane playerPlane = gameObject.GetComponent<PlayerPlane>(); // ��ȡ��ҷɻ����
        if (playerPlane != null)
        {
            playerPlane.AddBuff(BuffType.MiniBoost, 3f); // ����5����ٶ�����
        }
    }

}


