using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerPlane : MonoBehaviour
{
    // 公有变量，玩家配置
    public float forwardSpeed = 6f; // 向前移动速度
    public float rotationSpeed = 100f; // 旋转速度
    public Transform firePoint; // 发射子弹的位置
    public float bulletSpeed = 10f; // 子弹速度
    public float shootInterval = 1f; // 每秒射击一次
    public int health = 100;
    public int level = 0;
    public int money = 0;
    public GameObject bulletPrefab; // 子弹预制体
    public List<int> experienceToLevelUp = new List<int> { 30, 50, 100, 150, 220, 300, 400, 550 };  // 每个等级需要的经验
    public float accelerationSpeed = 3f;  // 加速时的增量
    public float maxSpeed = 12f; // 最大速度限制，设置为forwardSpeed的两倍
    // 新增：音效相关变量
    public AudioClip shootSound; // 子弹发射的音效
    public enum AttackType //敌人攻击方式
    {
        SingleShot,    // 普通攻击
        Shotgun_3,       // 散弹射击
        Shotgun_5,       // 散弹射击
        Radial_12,        // 辐射射击
        Radial_18,        // 辐射射击
        Horizontal_2,   // 水平射击
        Horizontal_3     // 水平射击
    }
    public AttackType attackType = AttackType.SingleShot; // 默认使用普通攻击

    // 私有变量
    private AudioSource audioSource; // 用于播放音效的 AudioSource
    private Rigidbody2D rb;
    private Transform cameraTransform;
    private float lastShootTime = 0f; // 上次射击的时间
    private float currentSpeed; // 当前前进速度
    private float lastAccelerationTime = 0f; // 记录加速的时间

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.interpolation = RigidbodyInterpolation2D.Interpolate; // 使用插值
        cameraTransform = Camera.main.transform;
        GameManager.Instance.UpdatePlayerHealth(health);
        GameManager.Instance.UpdateMoney(money);
        GameManager.Instance.UpdatePlayerLevel(level);

        currentSpeed = forwardSpeed; // 初始化当前速度

        // 获取音频源组件
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {


        // 检查是否升级
        CheckLevelUp();

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
        if (Input.GetKey(KeyCode.W) && GameManager.Instance.Money > 0)
        {
            // 增加当前速度，最多不超过最大速度（最大为forwardSpeed的两倍）
            currentSpeed = Mathf.Min(currentSpeed + accelerationSpeed * Time.deltaTime, forwardSpeed * 2);

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

    private void Accelerate()
    {
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
        // 播放射击音效
        audioSource.PlayOneShot(shootSound);

        switch (attackType)
        {
            case AttackType.SingleShot:
                ShootSingleBullet();
                break;
            case AttackType.Shotgun_3:
                ShootShotgun(3);
                break;
            case AttackType.Shotgun_5:
                ShootShotgun(5);  // 假设散弹枪发射 5 粒子弹
                break;
            case AttackType.Radial_12:
                ShootRadial(12);  // 假设辐射射击发射 12 粒子弹
                break;
            case AttackType.Radial_18:
                ShootRadial(18);
                break;
            case AttackType.Horizontal_2:
                ShootHorizontal(2);
                break;
            case AttackType.Horizontal_3:
                ShootHorizontal(3);  // 假设水平射击发射 3 粒子弹
                break;
        }
    }

    //----------------攻击方式，待补充-------------------------------------------
    //普通攻击
    public void ShootSingleBullet()
    {
        Vector2 direction = transform.up; // 玩家前进的方向

        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
        bulletRb.velocity = direction * bulletSpeed;

        // 保持子弹与玩家的前进方向一致
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        bullet.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }
    // 散弹射击
    public void ShootShotgun(int shotgunPellets)
    {
        // 散弹的发射角度范围
        float angleStep = 10f;  // 每颗子弹之间的角度差
        float spread = (shotgunPellets - 1) * angleStep / 2;

        // 遍历发射多颗子弹
        for (int i = 0; i < shotgunPellets; i++)
        {
            float currentAngle = -spread + i * angleStep;
            Vector2 direction = Quaternion.Euler(0, 0, currentAngle) * transform.up;

            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
            bulletRb.velocity = direction * bulletSpeed;

            // 旋转子弹，使其与发射方向一致
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            bullet.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }
    }
    // 辐射射击（360度发射）
    public void ShootRadial(int radialPellets)
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

            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
            bulletRb.velocity = direction * bulletSpeed;

            // 旋转子弹，使其与发射方向一致
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            bullet.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }
    }
    // 水平射击（发射多个平行子弹）
    public void ShootHorizontal(int bulletCount)
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
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();

            // 设置子弹的速度
            bulletRb.velocity = direction * bulletSpeed;

            // 计算偏移位置，左右偏移
            Vector2 bulletOffset = (i - (bulletCount - 1) / 2f) * offset; // 根据子弹数量动态计算偏移

            // 修改子弹的位置
            bullet.transform.position = (Vector2)transform.position + bulletOffset;

            // 子弹的角度保持一致
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            bullet.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }
    }
    //----------------攻击方式，待补充-------------------------------------------


    void OnCollisionEnter2D(Collision2D collision)
    {


        Debug.Log("与碰撞体相撞： " + collision.gameObject.name);

        // 检测是否与敌人飞机碰撞
        if (collision.gameObject.CompareTag("EnemyPlane"))
        {
            health -= 10;
            GameManager.Instance.UpdatePlayerHealth(health);
            Debug.Log("玩家受伤，剩余生命: " + health);
        }
        else if (collision.gameObject.CompareTag("AirWall"))
        {
            Debug.Log("你死了!,原因：撞到了空气墙");
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
        Debug.Log("-----------游戏结束-----------");
        SceneManager.LoadScene("GameOverScene");
        Time.timeScale = 0;  // 暂停游戏
    }

    public void isKill(int health)
    {
        if (health <= 0)
        {
            Debug.Log("----------玩家死亡----------");
            EndGame();
        }
    }

    void CheckLevelUp()
    {
        if (GameManager.Instance.PlayerExperience >= experienceToLevelUp[level])
        {
            level++;
            GameManager.Instance.PlayerExperience = 0;

            switch (level)
            {
                case 1:
                    health += 18;
                    break;
                case 2:
                    health += 20;
                    forwardSpeed += 1;
                    attackType = AttackType.Horizontal_2;
                    break;
                case 3:
                    health += 20;
                    attackType = AttackType.Horizontal_3;
                    break;
                case 4:
                    break;
                case 5:
                    health += 36;
                    break;
                case 6:
                    health += 15;
                    break;
                case 7:
                    attackType = AttackType.Shotgun_5;
                    break;
                case 8:
                    health += 60;
                    break;

            }

            GameManager.Instance.UpdatePlayerHealth(health);
            GameManager.Instance.UpdatePlayerLevel(level);
        }
    }
}
