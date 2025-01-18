using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerPlane : MonoBehaviour
{
    // 公有变量，玩家配置
    public float forwardSpeed = 10f; // 向前移动速度
    public float rotationSpeed = 100f; // 旋转速度
    public Transform firePoint; // 发射子弹的位置
    public float bulletSpeed = 10f; // 子弹速度
    public GameObject bulletPrefab; // 子弹预制体
    public float shootInterval = 1f; // 每秒射击一次
    public int health = 100;
    public int level = 0;
    public int experience = 0;
    public int money = 0;

    // 加速相关变量
    public float accelerationSpeed = 5f;  // 加速时的增量
    public float maxSpeed = 20f; // 最大速度限制

    // 私有变量
    private Rigidbody2D rb;
    private Transform cameraTransform;
    private float lastShootTime = 0f; // 上次射击的时间
    private float currentSpeed; // 当前前进速度
    private float lastAccelerationTime = 0f; //记录加速的时间
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.interpolation = RigidbodyInterpolation2D.Interpolate; // 使用插值
        cameraTransform = Camera.main.transform;
        GameManager.Instance.UpdatePlayerHealth(health);
        GameManager.Instance.UpdateMoney(money);
        GameManager.Instance.UpdatePlayerExperience(experience);
        GameManager.Instance.UpdatePlayerLevel(level);

        currentSpeed = forwardSpeed; // 初始化当前速度
    }

    void Update()
    {
        // 记录上一帧的时间
        float currentTime = Time.time;

        // 检查是否按下空格键或鼠标左键
        if (Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0))
        {
            // 长按时的持续射击
            if (currentTime - lastShootTime >= shootInterval)
            {
                Shoot();
                lastShootTime = currentTime; // 更新上次射击时间
            }
        }
        else if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            // 点击时发射一颗子弹
            Shoot();
            lastShootTime = currentTime; // 更新上次射击时间
        }

        // 加速控制：按住W键时加速
        if (Input.GetKey(KeyCode.W)&&GameManager.Instance.Money>0)
        {
            // 增加当前速度，最多不超过最大速度
            currentSpeed = Mathf.Min(currentSpeed + accelerationSpeed * Time.deltaTime, maxSpeed);

            // 每秒执行加速的操作
            if (currentTime - lastAccelerationTime >= 1f)
            {
                Accelerate();
                lastAccelerationTime = currentTime; // 更新加速的时间
            }
        }
        else
        {
            // 如果没有按住W键，恢复到默认速度
            currentSpeed = forwardSpeed;
        }
    }

    // 处理加速时需要执行的操作
    private void Accelerate()
    {
        // 在此执行加速时的逻辑
        Debug.Log("加速中...");
        GameManager.Instance.Money -= 1;
    }



    void FixedUpdate()
    {
        isKill(health);

        Vector2 forwardMovement = transform.up * currentSpeed * Time.fixedDeltaTime;
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

    public void Shoot()
    {
        // 普通射击（默认射1颗子弹）
        ShootSingleBullet();
    }

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
            Debug.Log("撞到子弹：" + collision.gameObject);
            GameManager.Instance.UpdatePlayerHealth(health);
            Destroy(collision.gameObject); // 销毁子弹
        }
    }

    public void EndGame()
    {
        // 输出游戏结束的日志
        Debug.Log("-----------游戏结束-----------");

        // 加载名为 "GameOverScene" 的场景
        SceneManager.LoadScene("GameOverScene");

        // 停止游戏的进行
        Time.timeScale = 0;  // 暂停游戏
    }

    // 检查玩家是否死亡
    public void isKill(int health)
    {
        // 如果血量为0，游戏结束
        if (health <= 0)
        {
            Debug.Log("----------玩家死亡----------");
            EndGame(); // 调用 EndGame 方法，触发游戏结束并加载场景
        }
    }
}
