using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public int Bullet_Damage = 5;
    public float Disappear_Time = 3;

    void Start()
    {
        Destroy(gameObject, Disappear_Time); 
    }

    void Update()
    {
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("AirWall"))
        {
            // 如果是，销毁子弹
            Destroy(gameObject);
        }
    }
}
