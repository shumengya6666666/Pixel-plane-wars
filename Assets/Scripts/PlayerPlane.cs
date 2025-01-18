using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlane : MonoBehaviour
{
    //���б������������
    public float forwardSpeed = 10f; // ��ǰ�ƶ��ٶ�
    public float rotationSpeed = 100f; // ��ת�ٶ�
    public Transform firePoint; // �����ӵ���λ��
    public float bulletSpeed = 10f; // �ӵ��ٶ�
    public GameObject bulletPrefab; // �ӵ�Ԥ���� //����ҵĸ��ڵ���������ӵ�Ԥ����
    public float shootInterval = 1f; // ÿ�����һ��
    public int health = 100;
    public int level = 0;
    public int experience = 0;
    public int money = 0;

    //˽�б���
    //��ȡ�����Ӧ�����
    private Rigidbody2D rb;
    private Transform cameraTransform;
    private float lastShootTime = 0f; // �ϴ������ʱ��

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.interpolation = RigidbodyInterpolation2D.Interpolate; // ʹ�ò�ֵ
        cameraTransform = Camera.main.transform;
        GameManager.Instance.UpdatePlayerHealth(health);
        GameManager.Instance.UpdateMoney(money);
        GameManager.Instance.UpdatePlayerExperience(experience);
        GameManager.Instance.UpdatePlayerLevel(level);
    }

    void Update()
    {
        // ����Ƿ��¿ո����������
        if (Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0))
        {
            // ����ʱ�ĳ������
            if (Time.time - lastShootTime >= shootInterval)
            {
                Shoot();
                lastShootTime = Time.time; // �����ϴ����ʱ��
            }
        }
        else if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            // ���ʱ����һ���ӵ�
            Shoot();
            lastShootTime = Time.time; // �����ϴ����ʱ��
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

    //�����ǳ�����������ָ�ķ��������ӵ�-----------------------------------------------------------
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

    //�����ǳ������ǰ���ķ��������ӵ�---------------------------------------------------------------
    // �������
    public void Shoot()
    {

        // ��ͨ�����Ĭ����1���ӵ���
        // ShootSingleBullet();
        // ɢ�����
        //ShootShotgun(3);
        //ShootRadial(27);

        ShootHorizontal(5);
    }


    // ��ͨ���
    private void ShootSingleBullet()
    {
        Vector2 direction = transform.up; // ���ǰ���ķ���

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
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

            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
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

            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
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
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
            Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();

            // �����ӵ����ٶ�
            bulletRb.velocity = direction * bulletSpeed;

            // ����ƫ��λ�ã�����ƫ��
            Vector2 bulletOffset = (i - (bulletCount - 1) / 2f) * offset; // �����ӵ�������̬����ƫ��

            // �޸��ӵ���λ��
            bullet.transform.position = (Vector2)firePoint.position + bulletOffset;

            // �ӵ��ĽǶȱ���һ��
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            bullet.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }
    }



    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("����ײ����ײ�� " + collision.gameObject.name); // ��ӵ�����Ϣ

        // ����Ƿ�����˷ɻ���ײ
        if (collision.gameObject.CompareTag("EnemyPlane"))
        {
            // ��ҵ�Ѫ�����Ը�����Ҫ������Ѫ����ֵ
            health -= 10;
            GameManager.Instance.UpdatePlayerHealth(health);
            Debug.Log("������ˣ�ʣ������: " + health);


        }
        else if (collision.gameObject.CompareTag("AirWall"))
        {
            Debug.Log("������!,ԭ��ײ���˿���ǽ"); // ȷ�������ǽ��ײ
            EndGame();
        }
        else if (collision.gameObject.CompareTag("EnemyBullet"))
        {
            health -= 5;
            Debug.Log("ײ���ӵ���"+ collision.gameObject);
            GameManager.Instance.UpdatePlayerHealth(health);
            Destroy(collision.gameObject); // �����ӵ�
        }
    }


    void EndGame()
    {
        // ��Ϸ�����Ĵ����߼�
        Debug.Log("-----------��Ϸ����-----------");

        // ֹͣ��Ϸ�Ľ���
        Time.timeScale = 0;  // ��ͣ��Ϸ
    }

    void isKill(int health)
    {
        // ���Ѫ��Ϊ0����Ϸ����
        if (health <= 0)
        {
            Debug.Log("----------�������----------");
            EndGame();
        }
    }

}
