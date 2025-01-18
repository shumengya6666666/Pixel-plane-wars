using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public float Generation_Probability = 0.5f; // ���ߵ����ɸ���
    public float Disappear_Time = 5f; //������ʧ��ʱ��
    public Sprite itemSprite;  // �������õ��ߵ�ͼƬ

    void Start()
    {
        Destroy(gameObject, Disappear_Time);

        // ���õ��ߵ�ͼƬ
        if (itemSprite != null)
        {
            GetComponent<SpriteRenderer>().sprite = itemSprite;
        }
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
            Trigger_prop_function(collision.gameObject);

        }
    }

    //���ֵ��߹�����չ�˷���
    // ���÷������Ϊ virtual�������ڼ̳�������д
    protected virtual void Trigger_prop_function(GameObject gameObject)
    {
        // ����ʲô���������ɼ̳е�����ʵ��
    }
}
