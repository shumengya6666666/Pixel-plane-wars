using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float forwardSpeed = 10f; // ��ǰ�ƶ��ٶ�
    public float rotationSpeed = 100f; // ��ת�ٶ�
    public Transform firePoint; // �����
    public float bulletSpeed = 10f; // �ӵ��ٶ�
    //����ҵĸ��ڵ���������ӵ�Ԥ����
    public GameObject bulletPrefab; // �ӵ�Ԥ����
    //��ȡ�����Ӧ�����
    private Rigidbody2D rb;
    private Transform cameraTransform;

    public float shootInterval = 1f; // ÿ�����һ��
    private float lastShootTime = 0f; // �ϴ������ʱ��

    public int health = 100;
    public int level = 0;
    public int experience = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.interpolation = RigidbodyInterpolation2D.Interpolate; // ʹ�ò�ֵ
        cameraTransform = Camera.main.transform;
        GameManager.Instance.UpdatePlayerHealth(health);
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Space) )
        {
            if (Time.time - lastShootTime >= shootInterval) {
                Shoot();
                lastShootTime = Time.time; // �����ϴ����ʱ��
            }


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
    void Shoot()
    {
        // �ӵ���������Ϊ���ǰ���ķ���
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
