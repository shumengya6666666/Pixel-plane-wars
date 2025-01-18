using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public int Bullet_Damage = 5;
    public float Disappear_Time = 5;

    void Start()
    {
        // 在 5 秒后销毁子弹对象
        Destroy(gameObject, Disappear_Time); // 5f 表示 5 秒后销毁该对象
    }

    void Update()
    {
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // 检查碰撞体是否有标签 "air_wall"
        if (collision.gameObject.CompareTag("AirWall"))
        {
            // 如果是，销毁子弹
            Destroy(gameObject);
            //Debug.Log("子弹撞到了空气墙");
        }
    }
}
