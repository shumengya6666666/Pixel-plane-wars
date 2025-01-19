using UnityEngine;
using System.Collections;

public class SpeedItem : Item
{
    public float speedBoost = 1.5f; // ���ӵ��ٶȱ���
    public float duration = 3f; // ����ʱ��
    private float timer = 0f; // ��ʱ��
    private bool isBoosting = false; // �Ƿ��ڼ���״̬
    private float originalSpeed; // ������ҵ�ԭʼ�ٶ�

    protected override void Trigger_prop_function(GameObject gameObject)
    {
        PlayerPlane playerPlane = gameObject.GetComponent<PlayerPlane>(); // ��ȡ��ҷɻ����
        if (playerPlane != null && !isBoosting) // ȷ�����ٻ�û�м���
        {
            isBoosting = true;
            originalSpeed = playerPlane.forwardSpeed; // ����ԭʼ�ٶ�
            playerPlane.forwardSpeed *= speedBoost; // ������ҵ��ƶ��ٶ�
            GameManager.Instance.UpdatePlayerHealth(playerPlane.health); // ����UI
            Debug.Log("ʰȡ�˼��ٵ��ߣ��ٶ����ӣ�" + speedBoost);
        }
    }

    void Update()
    {
        if (isBoosting)
        {
            timer += Time.deltaTime; // ��ʱ������
            if (timer >= duration) // �������ʱ�䵽��
            {
                ResetSpeed(); // �ָ�ԭʼ�ٶ�
            }
        }
    }

    private void ResetSpeed()
    {
        PlayerPlane playerPlane = FindObjectOfType<PlayerPlane>(); // ��ȡ��ҷɻ�����
        if (playerPlane != null)
        {
            playerPlane.forwardSpeed = originalSpeed; // �ָ�ԭʼ�ٶ�
            isBoosting = false; // ֹͣ����
            timer = 0f; // ���ü�ʱ��
            Debug.Log("���ٽ������ָ������ٶ�");
        }
    }
}
