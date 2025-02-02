using UnityEngine;
using System.Collections;
using static PlayerPlane;

public class GiganticItem : Item
{

    protected override void Trigger_prop_function(GameObject gameObject)
    {
        PlayerPlane playerPlane = gameObject.GetComponent<PlayerPlane>(); // ��ȡ��ҷɻ����
        if (playerPlane != null)
        {
            playerPlane.AddBuff(BuffType.GiganticBoost, 3f); // ����5����ٶ�����
        }
    }

}


