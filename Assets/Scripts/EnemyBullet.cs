using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public int Bullet_Damage = 5;
    public float Disappear_Time = 5;

    void Start()
    {
        // �� 5 ��������ӵ�����
        Destroy(gameObject, Disappear_Time); // 5f ��ʾ 5 ������ٸö���
    }

    void Update()
    {
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
