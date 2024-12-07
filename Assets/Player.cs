using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f; // �ƶ��ٶ�
    public GameObject bulletPrefab; // �ӵ�Ԥ����
    public Transform firePoint; // �����
    public float bulletSpeed = 10f; // �ӵ��ٶ�

    private Rigidbody2D rb;
    private Vector2 movement;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // ��ȡ����
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // ���ո���Ƿ���
        if (Input.GetKey(KeyCode.Space))
        {
            Shoot();
        }
    }

    void FixedUpdate()
    {
        // �ƶ���ɫ
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    void Shoot()
    {
        // �������ָ��ķ���
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePosition - firePoint.position).normalized;

        // �����ӵ�
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
        bulletRb.velocity = direction * bulletSpeed;

        // ��ת�ӵ��ĳ���
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        bullet.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }
}
