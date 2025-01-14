using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float forwardSpeed = 10f; // 向前移动速度
    public float rotationSpeed = 100f; // 旋转速度
    public Transform firePoint; // 发射点
    public float bulletSpeed = 10f; // 子弹速度
    //在玩家的父节点里面添加子弹预制体
    public GameObject bulletPrefab; // 子弹预制体
    //获取玩家相应的组件
    private Rigidbody2D rb;
    private Transform cameraTransform;

    public float shootInterval = 1f; // 每秒射击一次
    private float lastShootTime = 0f; // 上次射击的时间

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.interpolation = RigidbodyInterpolation2D.Interpolate; // 使用插值
        cameraTransform = Camera.main.transform;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Space) )
        {
            if (Time.time - lastShootTime >= shootInterval) {
                Shoot();
                lastShootTime = Time.time; // 更新上次射击时间
            }


        }
    }

    void FixedUpdate()
    {
        Vector2 forwardMovement = transform.up * forwardSpeed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + forwardMovement);

        float rotation = 0f;
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            rotation = rotationSpeed * Time.fixedDeltaTime;
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            rotation = -rotationSpeed * Time.fixedDeltaTime;
        }
        rb.MoveRotation(rb.rotation + rotation);
    }

    void LateUpdate()
    {
        cameraTransform.position = new Vector3(transform.position.x, transform.position.y, cameraTransform.position.z);
    }

    //这种是朝着玩家鼠标所指的方向生成子弹-----------------------------------------------------------
    //void Shoot()
    //{
    //    Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //    Vector2 direction = (mousePosition - firePoint.position).normalized;

    //    GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
    //    Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
    //    bulletRb.velocity = direction * bulletSpeed;

    //    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
    //    bullet.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    //}

    //这种是朝着玩家前进的方向生成子弹---------------------------------------------------------------
    void Shoot()
    {
        // 子弹方向设置为玩家前进的方向
        Vector2 direction = transform.up; // 玩家前进的方向

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
        bulletRb.velocity = direction * bulletSpeed;

        // 保持子弹与玩家的前进方向一致
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        bullet.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("与碰撞体相撞： " + collision.gameObject.name); // 添加调试信息
        if (collision.gameObject.CompareTag("AirWall"))
        {
            Debug.Log("你死了!,原因：撞到了空气墙"); // 确认与空气墙碰撞
            EndGame();
        }
    }

    void EndGame()
    {
        // 游戏结束的处理逻辑
        Debug.Log("-----------游戏结束-----------");
        // 可以添加其他游戏结束的处理，例如显示游戏结束界面、暂停游戏等。
    }
}
