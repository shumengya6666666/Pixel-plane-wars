using UnityEngine;

public class RadiationBomb : Item
{

    protected override void Trigger_prop_function(GameObject gameObject)
    {
        PlayerPlane playerPlane = gameObject.GetComponent<PlayerPlane>(); // ��ȡ��ҷɻ����
        if (playerPlane != null)
        {
            playerPlane.ShootRadial(18);
            Debug.Log("һ�ַ���״�ı�ը��");
        }
    }
}
