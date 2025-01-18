using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public float Generation_Probability = 0.5f; // ���ߵ����ɸ���
    public float Disappear_Time = 5f; //������ʧ��ʱ��

    void Start()
    {
        Destroy(gameObject, Disappear_Time);
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        // �����ײ���Ƿ��б�ǩ "air_wall"
        if (collision.gameObject.CompareTag("AirWall"))
        {
            // ����ǣ������ӵ�
            Destroy(gameObject);
            //Debug.Log("�ӵ�ײ���˿���ǽ");
        }
        else if (collision.gameObject.CompareTag("PlayerPlane"))
        {
            Destroy(gameObject);
        }
    }
}
