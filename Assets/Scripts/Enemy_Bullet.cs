using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Bullet : MonoBehaviour
{
    void Start()
    {
        // �� 5 ��������ӵ�����
        Destroy(gameObject, 5f); // 5f ��ʾ 5 ������ٸö���
    }

    void Update()
    {
        // ���������������ӵ����ƶ��߼�
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
    }
}
