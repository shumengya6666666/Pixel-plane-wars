using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlane : MonoBehaviour
{
    //共有变量，玩家配置
    public float forwardSpeed = 10f; // 向前移动速度
    public float rotationSpeed = 100f; // 旋转速度
    public Transform firePoint; // 发射子弹的位置
    public float bulletSpeed = 10f; // 子弹速度
    public GameObject bulletPrefab; // 子弹预制体 //在玩家的父节点里面添加子弹预制体
    public float shootInterval = 1f; // 每秒射击一次
    public int health = 100;
    public int level = 0;
    public int experience = 0;
    public int money = 0;

    //私有变量
    //获取玩家相应的组件
    private Rigidbody2D rb;
    private Transform cameraTransform;
    private float lastShootTime = 0f; // 上次射击的时间

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.interpolation = RigidbodyInterpolation2D.Interpolate; // 使用插值
        cameraTransform = Camera.main.transform;
        GameManager.Instance.UpdatePlayerHealth(health);
        GameManager.Instance.UpdateMoney(money);
        GameManager.Instance.UpdatePlayerExperience(experience);
        GameManager.Instance.UpdatePlayerLevel(level);
    }

    void Update()
    {
        // 检查是否按下空格键或鼠标左键
        if (Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0))
        {
            // 长按时的持续射击
            if (Time.time - lastShootTime >= shootInterval)
            {
                Shoot();
                lastShootTime = Time.time; // 更新上次射击时间
            }
        }
        else if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            // 点击时发射一颗子弹
            Shoot();
            lastShootTime = Time.time; // 更新上次射击时间
        }
    }


    void FixedUpdate()
    {
        isKill(health);

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
    // 射击方法
    public void Shoot()
    {

        // 普通射击（默认射1颗子弹）
        // ShootSingleBullet();
        // 散弹射击
        //ShootShotgun(3);
        //ShootRadial(27);

        ShootHorizontal(5);
    }


    // 普通射击
    private void ShootSingleBullet()
    {
        Vector2 direction = transform.up; // 玩家前进的方向

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
        bulletRb.velocity = direction * bulletSpeed;

        // 保持子弹与玩家的前进方向一致
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        bullet.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    // 散弹射击
    private void ShootShotgun(int shotgunPellets)
    {
        // 散弹的发射角度范围
        float angleStep = 10f;  // 每颗子弹之间的角度差
        float spread = (shotgunPellets - 1) * angleStep / 2;

        // 遍历发射多颗子弹
        for (int i = 0; i < shotgunPellets; i++)
        {
            float currentAngle = -spread + i * angleStep;
            Vector2 direction = Quaternion.Euler(0, 0, currentAngle) * transform.up;

            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
            Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
            bulletRb.velocity = direction * bulletSpeed;

            // 旋转子弹，使其与发射方向一致
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            bullet.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }
    }


    // 辐射射击（360度发射）
    private void ShootRadial(int radialPellets)
    {
        // 确保辐射射击至少发射一个子弹
        if (radialPellets <= 0) radialPellets = 1;

        float angleStep = 360f / radialPellets;  // 计算子弹之间的角度差

        // 遍历发射所有子弹
        for (int i = 0; i < radialPellets; i++)
        {
            // 计算每个子弹的发射角度
            float currentAngle = i * angleStep;
            Vector2 direction = Quaternion.Euler(0, 0, currentAngle) * transform.up;

            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
            Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
            bulletRb.velocity = direction * bulletSpeed;

            // 旋转子弹，使其与发射方向一致
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            bullet.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }
    }


    // 水平射击（发射多个平行子弹）
    private void ShootHorizontal(int bulletCount)
    {
        Vector2 direction = transform.up; // 玩家前进的方向

        // 偏移量
        float spreadAngle = 0.5f; // 子弹之间的偏移量，单位是世界坐标

        // 计算偏移方向
        Vector2 offset = new Vector2(-direction.y, direction.x) * spreadAngle; // 获取方向的垂直向量并进行偏移

        // 创建并设置每个子弹
        for (int i = 0; i < bulletCount; i++)
        {
            // 创建子弹
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
            Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();

            // 设置子弹的速度
            bulletRb.velocity = direction * bulletSpeed;

            // 计算偏移位置，左右偏移
            Vector2 bulletOffset = (i - (bulletCount - 1) / 2f) * offset; // 根据子弹数量动态计算偏移

            // 修改子弹的位置
            bullet.transform.position = (Vector2)firePoint.position + bulletOffset;

            // 子弹的角度保持一致
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            bullet.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }
    }



    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("与碰撞体相撞： " + collision.gameObject.name); // 添加调试信息

        // 检测是否与敌人飞机碰撞
        if (collision.gameObject.CompareTag("EnemyPlane"))
        {
            // 玩家掉血，可以根据需要调整扣血的数值
            health -= 10;
            GameManager.Instance.UpdatePlayerHealth(health);
            Debug.Log("玩家受伤，剩余生命: " + health);


        }
        else if (collision.gameObject.CompareTag("AirWall"))
        {
            Debug.Log("你死了!,原因：撞到了空气墙"); // 确认与空气墙碰撞
            EndGame();
        }
        else if (collision.gameObject.CompareTag("EnemyBullet"))
        {
            health -= 5;
            Debug.Log("撞到子弹："+ collision.gameObject);
            GameManager.Instance.UpdatePlayerHealth(health);
            Destroy(collision.gameObject); // 销毁子弹
        }
    }


    void EndGame()
    {
        // 游戏结束的处理逻辑
        Debug.Log("-----------游戏结束-----------");

        // 停止游戏的进行
        Time.timeScale = 0;  // 暂停游戏
    }

    void isKill(int health)
    {
        // 如果血量为0，游戏结束
        if (health <= 0)
        {
            Debug.Log("----------玩家死亡----------");
            EndGame();
        }
    }

}
