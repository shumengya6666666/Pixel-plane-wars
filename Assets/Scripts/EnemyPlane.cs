using UnityEngine;
using static EnemyPlane;
using static UnityEditor.Progress;

public class EnemyPlane : MonoBehaviour
{
    // 外部变量 可配置的参数
    public bool isCanSuicide = true; // 是否敌人消失
    public int SuicideTime = 20; // 敌人消失的时间
    public float speed = 9f; // 移动速度
    public int enemyHealth = 20; //敌人生命值
    public float rotationSpeed = 200f; // 提高旋转速度使追踪更敏捷
    public GameObject bulletPrefab; // 子弹预制体
    public float bulletSpeed = 10f; // 子弹速度
    public float fireRate = 1f; // 发射子弹的间隔时间
    public float attackRange = 10f; // 敌人的攻击范围，达到此距离才可以发射子弹
    public int Generated_Money = 10; // 敌人掉落的金币数
    public int Generated_Experience = 5; // 敌人掉落的经验值
    public float Generation_Probability = 0.5f; // 敌人的生成概率
    public enum AttackType //敌人攻击方式
    {
        SingleShot,    // 普通攻击
        Shotgun,       // 散弹射击
        Radial,        // 辐射射击
        Horizontal     // 水平射击
    }
    public AttackType attackType = AttackType.SingleShot; // 默认使用普通攻击



    // 内部变量

    private Transform player; // 玩家对象的引用
    private float nextFireTime = 0f; // 下一次发射时间

    void Start()
    {
        // 尝试查找玩家对象
        GameObject playerObj = GameObject.FindGameObjectWithTag("PlayerPlane");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }

        if (isCanSuicide)
        {
            Destroy(gameObject, SuicideTime); // 设定在指定时间后销毁敌人飞机
        }

        // 通过概率来决定是否生成敌人
        if (Random.value > Generation_Probability)
        {
            Destroy(gameObject); // 如果不符合生成条件，销毁敌人
        }
    }

    void Update()
    {
        isKill(enemyHealth);

        // 如果玩家引用无效，尝试重新获取（处理玩家可能被重新创建的情况）
        if (player == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("PlayerPlane");
            if (playerObj != null)
            {
                player = playerObj.transform;
            }
        }

        // 追踪玩家
        if (player != null)
        {
            // 计算敌人飞机与玩家的方向
            Vector2 direction = (player.position - transform.position).normalized;

            // 计算目标角度，并调整偏移以匹配默认朝上
            float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
            float currentAngle = transform.eulerAngles.z;
            // 计算逐渐逼近目标角度的新角度
            float newAngle = Mathf.MoveTowardsAngle(currentAngle, targetAngle, rotationSpeed * Time.deltaTime);
            transform.eulerAngles = new Vector3(0, 0, newAngle);

            // 持续向前移动（沿着飞机自身朝向的方向，即向上）
            transform.Translate(Vector2.up * speed * Time.deltaTime);

            // 计算敌人与玩家的距离
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);

            // 判断敌人是否足够接近玩家才发射子弹
            if (distanceToPlayer < attackRange && Mathf.Abs(Mathf.DeltaAngle(transform.eulerAngles.z, targetAngle)) < 5f && Time.time > nextFireTime)
            {
                switch (attackType)
                {
                    case AttackType.SingleShot:
                        FireBullet();
                        break;
                    case AttackType.Shotgun:
                        ShootShotgun(5);  // 假设散弹枪发射 5 粒子弹
                        break;
                    case AttackType.Radial:
                        ShootRadial(12);  // 假设辐射射击发射 12 粒子弹
                        break;
                    case AttackType.Horizontal:
                        ShootHorizontal(3);  // 假设水平射击发射 3 粒子弹
                        break;
                }
                nextFireTime = Time.time + fireRate; // 设置下一次发射时间
            }
        }
    }


    // 发射子弹的方法
    void FireBullet()
    {
        ShootSingleBullet();
    }

    //普通攻击
    private void ShootSingleBullet()
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

            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
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

            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
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



    // 撞到物体时的处理
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("AirWall")) // 与空气墙碰撞
        {
            Debug.Log("敌人飞机撞到墙了！");
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("PlayerPlane")) // 与玩家飞机碰撞
        {
            Debug.Log("敌人飞机与玩家碰撞！");
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("PlayerBullet")) // 与玩家子弹碰撞
        {

            Destroy(collision.gameObject); // 销毁子弹
            PlayerBullet playerbullet = collision.gameObject.GetComponent<PlayerBullet>();

            if (playerbullet != null)
            {
                enemyHealth -= playerbullet.Bullet_Damage;
            }
            GameManager.Instance.AddMoney(Generated_Money);
            GameManager.Instance.AddPlayerExperience(Generated_Experience);
        }
    }


    void isKill(int health)
    {
        // 如果血量为0，游戏结束
        if (health <= 0)
        {
            Debug.Log("----------敌人死亡----------");
            Destroy(gameObject);
        }
    }
}
