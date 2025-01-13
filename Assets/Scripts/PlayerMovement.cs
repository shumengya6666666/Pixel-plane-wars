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

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.interpolation = RigidbodyInterpolation2D.Interpolate; // ʹ�ò�ֵ
        cameraTransform = Camera.main.transform;
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
        if (collision.gameObject.CompareTag("AirWall"))
        {
            Debug.Log("������!,ԭ��ײ���˿���ǽ"); // ȷ�������ǽ��ײ
            EndGame();
        }
    }

    void EndGame()
    {
        // ��Ϸ�����Ĵ����߼�
        Debug.Log("-----------��Ϸ����-----------");
        // �������������Ϸ�����Ĵ���������ʾ��Ϸ�������桢��ͣ��Ϸ�ȡ�
    }
}
