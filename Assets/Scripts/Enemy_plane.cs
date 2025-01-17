using UnityEngine;

public class EnemyPlane : MonoBehaviour
{
    // �ⲿ���� �����õĲ���
    public bool isCanSuicide = true;//�Ƿ������ʧ
    public int SuicideTime = 20;//������ʧ��ʱ��
    public float speed = 9f; // �ƶ��ٶ�
    public float rotationSpeed = 200f; // �����ת�ٶ�ʹ׷�ٸ�����
    public GameObject bulletPrefab; // �ӵ�Ԥ����
    public float bulletSpeed = 10f; // �ӵ��ٶ�
    public float fireRate = 1f; // �����ӵ��ļ��ʱ��
    public float attackRange = 10f;//���˵Ĺ�����Χ���ﵽ�˾���ſ��Է����ӵ�
    public int Generated_Money = 10;//���˵���Ľ����
    public int Generated_Experience = 5;//���˵���ľ���ֵ

    //�ڲ�����
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
    }

    void Update()
    {
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
                FireBullet();
                nextFireTime = Time.time + fireRate; // ������һ�η���ʱ��
            }
        }
    }



    // �����ӵ��ķ���
    void FireBullet()
    {
        if (bulletPrefab != null)
        {
            // ʵ�����ӵ������������ʼλ�úͽǶ�
            GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                // ���ӵ�һ����ʼ�ٶȣ����ŵ��˷ɻ���ǰ������
                rb.velocity = transform.up * bulletSpeed;
            }
        }
    }

    // ײ������ʱ�Ĵ���
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("AirWall"))//�����ǽ��ײ
        {
            Debug.Log("���˷ɻ�ײ��ǽ�ˣ�");
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("PlayerPlane"))//����ҷɻ���ײ
        {
            Debug.Log("���˷ɻ��������ײ��");
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("PlayerBullet"))//������ӵ���ײ
        {
            Destroy(gameObject);
            Destroy(collision.gameObject); // �����ӵ�
            GameManager.Instance.AddMoney(Generated_Money); 
            GameManager.Instance.AddPlayerExperience(Generated_Experience);
        }

    }
}
