using UnityEngine;

public class HealthItem : Item
{
    public int healthIncrease = 20; // ���ӵ�����ֵ

    protected override void Trigger_prop_function(GameObject gameObject)
    {
        PlayerPlane playerPlane = gameObject.GetComponent<PlayerPlane>(); // ��ȡ��ҷɻ����
        if (playerPlane != null)
        {
            playerPlane.health += healthIncrease; // ��������
            GameManager.Instance.UpdatePlayerHealth(playerPlane.health); // ����UI
            Debug.Log("ʰȡ���������ߣ���������ֵ��" + healthIncrease);
        }
    }
}
