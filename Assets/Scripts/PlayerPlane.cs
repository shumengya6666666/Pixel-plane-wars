using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerPlane : MonoBehaviour
{
    // ���б������������
    public float forwardSpeed = 10f; // ��ǰ�ƶ��ٶ�
    public float rotationSpeed = 100f; // ��ת�ٶ�
    public Transform firePoint; // �����ӵ���λ��
    public float bulletSpeed = 10f; // �ӵ��ٶ�
    public GameObject bulletPrefab; // �ӵ�Ԥ����
    public float shootInterval = 1f; // ÿ�����һ��
    public int health = 100;
    public int level = 0;
    public int experience = 0;
    public int money = 0;

    // ������ر���
    public float accelerationSpeed = 5f;  // ����ʱ������
    public float maxSpeed = 20f; // ����ٶ�����

    // ˽�б���
    private Rigidbody2D rb;
    private Transform cameraTransform;
    private float lastShootTime = 0f; // �ϴ������ʱ��
    private float currentSpeed; // ��ǰǰ���ٶ�
    private float lastAccelerationTime = 0f; //��¼���ٵ�ʱ��
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.interpolation = RigidbodyInterpolation2D.Interpolate; // ʹ�ò�ֵ
        cameraTransform = Camera.main.transform;
        GameManager.Instance.UpdatePlayerHealth(health);
        GameManager.Instance.UpdateMoney(money);
        GameManager.Instance.UpdatePlayerExperience(experience);
        GameManager.Instance.UpdatePlayerLevel(level);

        currentSpeed = forwardSpeed; // ��ʼ����ǰ�ٶ�
    }

    void Update()
    {
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
        if (Input.GetKey(KeyCode.W)&&GameManager.Instance.Money>0)
        {
            // ���ӵ�ǰ�ٶȣ���಻��������ٶ�
            currentSpeed = Mathf.Min(currentSpeed + accelerationSpeed * Time.deltaTime, maxSpeed);

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

    // �������ʱ��Ҫִ�еĲ���
    private void Accelerate()
    {
        // �ڴ�ִ�м���ʱ���߼�
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
        // ��ͨ�����Ĭ����1���ӵ���
        ShootSingleBullet();
    }

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
            Debug.Log("ײ���ӵ���" + collision.gameObject);
            GameManager.Instance.UpdatePlayerHealth(health);
            Destroy(collision.gameObject); // �����ӵ�
        }
    }

    public void EndGame()
    {
        // �����Ϸ��������־
        Debug.Log("-----------��Ϸ����-----------");

        // ������Ϊ "GameOverScene" �ĳ���
        SceneManager.LoadScene("GameOverScene");

        // ֹͣ��Ϸ�Ľ���
        Time.timeScale = 0;  // ��ͣ��Ϸ
    }

    // �������Ƿ�����
    public void isKill(int health)
    {
        // ���Ѫ��Ϊ0����Ϸ����
        if (health <= 0)
        {
            Debug.Log("----------�������----------");
            EndGame(); // ���� EndGame ������������Ϸ���������س���
        }
    }
}
