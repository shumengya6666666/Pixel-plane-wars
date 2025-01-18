using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerPlane : MonoBehaviour
{
    // ���б������������
    public float forwardSpeed = 6f; // ��ǰ�ƶ��ٶ�
    public float rotationSpeed = 100f; // ��ת�ٶ�
    public Transform firePoint; // �����ӵ���λ��
    public float bulletSpeed = 10f; // �ӵ��ٶ�
    public float shootInterval = 1f; // ÿ�����һ��
    public int health = 100;
    public int level = 0;
    public int money = 0;
    public GameObject bulletPrefab; // �ӵ�Ԥ����
    public List<int> experienceToLevelUp = new List<int> { 30, 50, 100, 150, 220, 300, 400, 550 };  // ÿ���ȼ���Ҫ�ľ���
    public float accelerationSpeed = 3f;  // ����ʱ������
    public float maxSpeed = 12f; // ����ٶ����ƣ�����ΪforwardSpeed������
    // ��������Ч��ر���
    public AudioClip shootSound; // �ӵ��������Ч
    public enum AttackType //���˹�����ʽ
    {
        SingleShot,    // ��ͨ����
        Shotgun_3,       // ɢ�����
        Shotgun_5,       // ɢ�����
        Radial_12,        // �������
        Radial_18,        // �������
        Horizontal_2,   // ˮƽ���
        Horizontal_3     // ˮƽ���
    }
    public AttackType attackType = AttackType.SingleShot; // Ĭ��ʹ����ͨ����

    // ˽�б���
    private AudioSource audioSource; // ���ڲ�����Ч�� AudioSource
    private Rigidbody2D rb;
    private Transform cameraTransform;
    private float lastShootTime = 0f; // �ϴ������ʱ��
    private float currentSpeed; // ��ǰǰ���ٶ�
    private float lastAccelerationTime = 0f; // ��¼���ٵ�ʱ��

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.interpolation = RigidbodyInterpolation2D.Interpolate; // ʹ�ò�ֵ
        cameraTransform = Camera.main.transform;
        GameManager.Instance.UpdatePlayerHealth(health);
        GameManager.Instance.UpdateMoney(money);
        GameManager.Instance.UpdatePlayerLevel(level);

        currentSpeed = forwardSpeed; // ��ʼ����ǰ�ٶ�

        // ��ȡ��ƵԴ���
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {


        // ����Ƿ�����
        CheckLevelUp();

        // ��¼��һ֡��ʱ��
        float currentTime = Time.time;

        // ����Ƿ��¿ո����������
        if (Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0))
        {
            // ����ʱ�ĳ������
            if (currentTime - lastShootTime >= shootInterval)
            {
                Shoot();
                lastShootTime = currentTime; // �����ϴ����ʱ��
            }
        }
        else if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            // ���ʱ����һ���ӵ�
            Shoot();
            lastShootTime = currentTime; // �����ϴ����ʱ��
        }

        // ���ٿ��ƣ���סW��ʱ����
        if (Input.GetKey(KeyCode.W) && GameManager.Instance.Money > 0)
        {
            // ���ӵ�ǰ�ٶȣ���಻��������ٶȣ����ΪforwardSpeed��������
            currentSpeed = Mathf.Min(currentSpeed + accelerationSpeed * Time.deltaTime, forwardSpeed * 2);

            // ÿ��ִ�м��ٵĲ���
            if (currentTime - lastAccelerationTime >= 1f)
            {
                Accelerate();
                lastAccelerationTime = currentTime; // ���¼��ٵ�ʱ��
            }
        }
        else
        {
            // ���û�а�סW�����ָ���Ĭ���ٶ�
            currentSpeed = forwardSpeed;
        }
    }

    private void Accelerate()
    {
        Debug.Log("������...");
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
        // ���������Ч
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
                ShootShotgun(5);  // ����ɢ��ǹ���� 5 ���ӵ�
                break;
            case AttackType.Radial_12:
                ShootRadial(12);  // �������������� 12 ���ӵ�
                break;
            case AttackType.Radial_18:
                ShootRadial(18);
                break;
            case AttackType.Horizontal_2:
                ShootHorizontal(2);
                break;
            case AttackType.Horizontal_3:
                ShootHorizontal(3);  // ����ˮƽ������� 3 ���ӵ�
                break;
        }
    }

    //----------------������ʽ��������-------------------------------------------
    //��ͨ����
    public void ShootSingleBullet()
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
    public void ShootShotgun(int shotgunPellets)
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
    public void ShootRadial(int radialPellets)
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
    public void ShootHorizontal(int bulletCount)
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
    //----------------������ʽ��������-------------------------------------------


    void OnCollisionEnter2D(Collision2D collision)
    {


        Debug.Log("����ײ����ײ�� " + collision.gameObject.name);

        // ����Ƿ�����˷ɻ���ײ
        if (collision.gameObject.CompareTag("EnemyPlane"))
        {
            health -= 10;
            GameManager.Instance.UpdatePlayerHealth(health);
            Debug.Log("������ˣ�ʣ������: " + health);
        }
        else if (collision.gameObject.CompareTag("AirWall"))
        {
            Debug.Log("������!,ԭ��ײ���˿���ǽ");
            EndGame();
        }
        else if (collision.gameObject.CompareTag("EnemyBullet"))
        {
            health -= 5;
            Debug.Log("ײ���ӵ���" + collision.gameObject);
            GameManager.Instance.UpdatePlayerHealth(health);
            Destroy(collision.gameObject); // �����ӵ�
        }
    }

    public void EndGame()
    {
        Debug.Log("-----------��Ϸ����-----------");
        SceneManager.LoadScene("GameOverScene");
        Time.timeScale = 0;  // ��ͣ��Ϸ
    }

    public void isKill(int health)
    {
        if (health <= 0)
        {
            Debug.Log("----------�������----------");
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
