using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public float Generation_Probability = 0.5f; // 道具的生成概率
    public float Disappear_Time = 5f; //道具消失的时间
    public Sprite itemSprite;  // 用来设置道具的图片

    void Start()
    {
        Destroy(gameObject, Disappear_Time);

        // 设置道具的图片
        if (itemSprite != null)
        {
            GetComponent<SpriteRenderer>().sprite = itemSprite;
        }
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
            Trigger_prop_function(collision.gameObject);

        }
    }

    //各种道具功能扩展此方法
    // 将该方法标记为 virtual，允许在继承类中重写
    protected virtual void Trigger_prop_function(GameObject gameObject)
    {
        // 基类什么都不做，由继承的类来实现
    }
}
