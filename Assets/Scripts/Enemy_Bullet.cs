using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Bullet : MonoBehaviour
{
    void Start()
    {
        // 在 5 秒后销毁子弹对象
        Destroy(gameObject, 5f); // 5f 表示 5 秒后销毁该对象
    }

    void Update()
    {
        // 你可以在这里添加子弹的移动逻辑
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
