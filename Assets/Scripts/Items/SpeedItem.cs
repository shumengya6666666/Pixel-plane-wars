using UnityEngine;
using System.Collections;

public class SpeedItem : Item
{
    public float speedBoost = 2f; // ���ӵ��ٶȱ���
    public float duration = 5f; // ����ʱ��
    private float timer = 0f; // ��ʱ��
    private bool isBoosting = false; // �Ƿ��ڼ���״̬

    protected override void Trigger_prop_function(GameObject gameObject)
    {
        PlayerPlane playerPlane = gameObject.GetComponent<PlayerPlane>(); // ��ȡ��ҷɻ����
        if (playerPlane != null && !isBoosting) // ȷ�����ٻ�û�м���
        {
            isBoosting = true;
            playerPlane.forwardSpeed *= speedBoost; // ������ҵ��ƶ��ٶ�
            GameManager.Instance.UpdatePlayerHealth(playerPlane.health); // ����UI
            Debug.Log("ʰȡ�˼��ٵ��ߣ��ٶ����ӣ�" + speedBoost);
            timer = 0f; // ���ü�ʱ��
        }
    }

    void Update()
    {
        if (isBoosting)
        {
            timer += Time.deltaTime; // ���Ӽ�ʱ��
            if (timer >= duration)
            {
                PlayerPlane playerPlane = FindObjectOfType<PlayerPlane>(); // ��ȡ��ҷɻ�
                if (playerPlane != null)
                {
                    playerPlane.forwardSpeed /= speedBoost; // �ָ��ٶ�
                    Debug.Log("����Ч���������ٶȻָ�����");
                    isBoosting = false; // ��������״̬
                }
            }
        }
    }
}
