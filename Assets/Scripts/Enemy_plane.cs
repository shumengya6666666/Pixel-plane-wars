using UnityEngine;

public class EnemyPlane : MonoBehaviour
{
    // 外部变量 可配置的参数
    public bool isCanSuicide = true;//是否敌人消失
    public int SuicideTime = 20;//敌人消失的时间
    public float speed = 9f; // 移动速度
    public float rotationSpeed = 200f; // 提高旋转速度使追踪更敏捷
    public GameObject bulletPrefab; // 子弹预制体
    public float bulletSpeed = 10f; // 子弹速度
    public float fireRate = 1f; // 发射子弹的间隔时间
    public float attackRange = 10f;//敌人的攻击范围，达到此距离才可以发射子弹
    public int Generated_Money = 10;//敌人掉落的金币数
    public int Generated_Experience = 5;//敌人掉落的经验值

    //内部变量
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
    }

    void Update()
    {
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
                FireBullet();
                nextFireTime = Time.time + fireRate; // 设置下一次发射时间
            }
        }
    }



    // 发射子弹的方法
    void FireBullet()
    {
        if (bulletPrefab != null)
        {
            // 实例化子弹，并设置其初始位置和角度
            GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                // 给子弹一个初始速度，沿着敌人飞机的前方发射
                rb.velocity = transform.up * bulletSpeed;
            }
        }
    }

    // 撞到物体时的处理
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("AirWall"))//与空气墙碰撞
        {
            Debug.Log("敌人飞机撞到墙了！");
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("PlayerPlane"))//与玩家飞机碰撞
        {
            Debug.Log("敌人飞机与玩家碰撞！");
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("PlayerBullet"))//与玩家子弹碰撞
        {
            Destroy(gameObject);
            Destroy(collision.gameObject); // 销毁子弹
            GameManager.Instance.AddMoney(Generated_Money); 
            GameManager.Instance.AddPlayerExperience(Generated_Experience);
        }

    }
}
