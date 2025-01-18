using UnityEngine;
using static EnemyPlane;
using static UnityEditor.Progress;

public class EnemyPlane : MonoBehaviour
{
    // �ⲿ���� �����õĲ���
    public bool isCanSuicide = true; // �Ƿ������ʧ
    public int SuicideTime = 20; // ������ʧ��ʱ��
    public float speed = 9f; // �ƶ��ٶ�
    public int enemyHealth = 20; //��������ֵ
    public float rotationSpeed = 200f; // �����ת�ٶ�ʹ׷�ٸ�����
    public GameObject bulletPrefab; // �ӵ�Ԥ����
    public float bulletSpeed = 10f; // �ӵ��ٶ�
    public float fireRate = 1f; // �����ӵ��ļ��ʱ��
    public float attackRange = 10f; // ���˵Ĺ�����Χ���ﵽ�˾���ſ��Է����ӵ�
    public int Generated_Money = 10; // ���˵���Ľ����
    public int Generated_Experience = 5; // ���˵���ľ���ֵ
    public float Generation_Probability = 0.5f; // ���˵����ɸ���
    public enum AttackType //���˹�����ʽ
    {
        SingleShot,    // ��ͨ����
        Shotgun,       // ɢ�����
        Radial,        // �������
        Horizontal     // ˮƽ���
    }
    public AttackType attackType = AttackType.SingleShot; // Ĭ��ʹ����ͨ����



    // �ڲ�����

    private Transform player; // ��Ҷ��������
    private float nextFireTime = 0f; // ��һ�η���ʱ��

    void Start()
    {
        // ���Բ�����Ҷ���
        GameObject playerObj = GameObject.FindGameObjectWithTag("PlayerPlane");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }

        if (isCanSuicide)
        {
            Destroy(gameObject, SuicideTime); // �趨��ָ��ʱ������ٵ��˷ɻ�
        }

        // ͨ�������������Ƿ����ɵ���
        if (Random.value > Generation_Probability)
        {
            Destroy(gameObject); // ����������������������ٵ���
        }
    }

    void Update()
    {
        isKill(enemyHealth);

        // ������������Ч���������»�ȡ��������ҿ��ܱ����´����������
        if (player == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("PlayerPlane");
            if (playerObj != null)
            {
                player = playerObj.transform;
            }
        }

        // ׷�����
        if (player != null)
        {
            // ������˷ɻ�����ҵķ���
            Vector2 direction = (player.position - transform.position).normalized;

            // ����Ŀ��Ƕȣ�������ƫ����ƥ��Ĭ�ϳ���
            float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
            float currentAngle = transform.eulerAngles.z;
            // �����𽥱ƽ�Ŀ��Ƕȵ��½Ƕ�
            float newAngle = Mathf.MoveTowardsAngle(currentAngle, targetAngle, rotationSpeed * Time.deltaTime);
            transform.eulerAngles = new Vector3(0, 0, newAngle);

            // ������ǰ�ƶ������ŷɻ�������ķ��򣬼����ϣ�
            transform.Translate(Vector2.up * speed * Time.deltaTime);

            // �����������ҵľ���
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);

            // �жϵ����Ƿ��㹻�ӽ���Ҳŷ����ӵ�
            if (distanceToPlayer < attackRange && Mathf.Abs(Mathf.DeltaAngle(transform.eulerAngles.z, targetAngle)) < 5f && Time.time > nextFireTime)
            {
                switch (attackType)
                {
                    case AttackType.SingleShot:
                        FireBullet();
                        break;
                    case AttackType.Shotgun:
                        ShootShotgun(5);  // ����ɢ��ǹ���� 5 ���ӵ�
                        break;
                    case AttackType.Radial:
                        ShootRadial(12);  // �������������� 12 ���ӵ�
                        break;
                    case AttackType.Horizontal:
                        ShootHorizontal(3);  // ����ˮƽ������� 3 ���ӵ�
                        break;
                }
                nextFireTime = Time.time + fireRate; // ������һ�η���ʱ��
            }
        }
    }


    // �����ӵ��ķ���
    void FireBullet()
    {
        ShootSingleBullet();
    }

    //��ͨ����
    private void ShootSingleBullet()
    {
        Vector2 direction = transform.up; // ���ǰ���ķ���

        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
        bulletRb.velocity = direction * bulletSpeed;

        // �����ӵ�����ҵ�ǰ������һ��
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        bullet.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    // ɢ�����
    private void ShootShotgun(int shotgunPellets)
    {
        // ɢ���ķ���Ƕȷ�Χ
        float angleStep = 10f;  // ÿ���ӵ�֮��ĽǶȲ�
        float spread = (shotgunPellets - 1) * angleStep / 2;

        // �����������ӵ�
        for (int i = 0; i < shotgunPellets; i++)
        {
            float currentAngle = -spread + i * angleStep;
            Vector2 direction = Quaternion.Euler(0, 0, currentAngle) * transform.up;

            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
            bulletRb.velocity = direction * bulletSpeed;

            // ��ת�ӵ���ʹ���뷢�䷽��һ��
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            bullet.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }
    }


    // ���������360�ȷ��䣩
    private void ShootRadial(int radialPellets)
    {
        // ȷ������������ٷ���һ���ӵ�
        if (radialPellets <= 0) radialPellets = 1;

        float angleStep = 360f / radialPellets;  // �����ӵ�֮��ĽǶȲ�

        // �������������ӵ�
        for (int i = 0; i < radialPellets; i++)
        {
            // ����ÿ���ӵ��ķ���Ƕ�
            float currentAngle = i * angleStep;
            Vector2 direction = Quaternion.Euler(0, 0, currentAngle) * transform.up;

            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
            bulletRb.velocity = direction * bulletSpeed;

            // ��ת�ӵ���ʹ���뷢�䷽��һ��
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            bullet.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }
    }


    // ˮƽ�����������ƽ���ӵ���
    private void ShootHorizontal(int bulletCount)
    {
        Vector2 direction = transform.up; // ���ǰ���ķ���

        // ƫ����
        float spreadAngle = 0.5f; // �ӵ�֮���ƫ��������λ����������

        // ����ƫ�Ʒ���
        Vector2 offset = new Vector2(-direction.y, direction.x) * spreadAngle; // ��ȡ����Ĵ�ֱ����������ƫ��

        // ����������ÿ���ӵ�
        for (int i = 0; i < bulletCount; i++)
        {
            // �����ӵ�
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();

            // �����ӵ����ٶ�
            bulletRb.velocity = direction * bulletSpeed;

            // ����ƫ��λ�ã�����ƫ��
            Vector2 bulletOffset = (i - (bulletCount - 1) / 2f) * offset; // �����ӵ�������̬����ƫ��

            // �޸��ӵ���λ��
            bullet.transform.position = (Vector2)transform.position + bulletOffset;

            // �ӵ��ĽǶȱ���һ��
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            bullet.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }
    }



    // ײ������ʱ�Ĵ���
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("AirWall")) // �����ǽ��ײ
        {
            Debug.Log("���˷ɻ�ײ��ǽ�ˣ�");
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("PlayerPlane")) // ����ҷɻ���ײ
        {
            Debug.Log("���˷ɻ��������ײ��");
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("PlayerBullet")) // ������ӵ���ײ
        {

            Destroy(collision.gameObject); // �����ӵ�
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
        // ���Ѫ��Ϊ0����Ϸ����
        if (health <= 0)
        {
            Debug.Log("----------��������----------");
            Destroy(gameObject);
        }
    }
}
