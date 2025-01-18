using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public float Generation_Probability = 0.5f; // 道具的生成概率
    public float Disappear_Time = 5f; //道具消失的时间

    void Start()
    {
        Destroy(gameObject, Disappear_Time);
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
        else if (collision.gameObject.CompareTag("PlayerPlane"))
        {
            Destroy(gameObject);
        }
    }
}
