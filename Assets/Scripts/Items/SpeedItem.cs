using UnityEngine;
using System.Collections;


public class SpeedItem : Item
{
    public float speedBoost = 2f; // ���ӵ��ٶȱ���
    public float duration = 5f; // ����ʱ��

    protected override void Trigger_prop_function(GameObject gameObject)
    {
        PlayerPlane playerPlane = gameObject.GetComponent<PlayerPlane>(); // ��ȡ��ҷɻ����
        if (playerPlane != null)
        {
            playerPlane.forwardSpeed *= speedBoost; // ������ҵ��ƶ��ٶ�
            GameManager.Instance.UpdatePlayerHealth(playerPlane.health); // ����UI
            Debug.Log("ʰȡ�˼��ٵ��ߣ��ٶ����ӣ�" + speedBoost);

            // �ָ�Ĭ���ٶ�
            StartCoroutine(ResetSpeedAfterDuration(playerPlane));
        }
    }

    private IEnumerator ResetSpeedAfterDuration(PlayerPlane playerPlane)
    {
        yield return new WaitForSeconds(duration);
        playerPlane.forwardSpeed /= speedBoost; // �ָ��ٶ�
        Debug.Log("����Ч���������ٶȻָ�����");
    }
}
