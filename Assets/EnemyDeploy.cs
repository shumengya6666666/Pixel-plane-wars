using UnityEngine;

public class FastEnemyPlane : EnemyPlane
{
    // ���ٵ������е�����
    public float additionalSpeed = 5f;

    // ��д Start �������޸Ļ������е�һЩ����
    new void Start()
    {
        base.Start(); // ���û���� Start ����

        // ���ø��ߵ��ٶ�
        speed += additionalSpeed;
    }

    // �����Ҫ��������д Update ������ʵ�ֲ�ͬ����Ϊ
    new void Update()
    {
        base.Update(); // ����������ƶ��͹����߼�

        // ������ڴ����������Ϊ��������ٵ��˵�������Ϊ
        // ������ٸı��ƶ�����
    }
}

public class TankEnemyPlane : EnemyPlane
{
    // ǿ���������е�����
    public int additionalHealth = 50;
    public float attackDamageMultiplier = 2f;

    // ��д Start ����
    new void Start()
    {
        base.Start();

        // ���ø��ߵ�����ֵ
        enemyHealth += additionalHealth;

        // �޸Ĺ�����������ͨ���޸��������Ի򷽷�ʵ�֣�
        bulletSpeed *= attackDamageMultiplier;
    }

    // ��д Update ����
    new void Update()
    {
        base.Update(); // �����������Ϊ

        // ǿ�����˵����⹥����ʽ����������������Ϊ
    }

    // ��д OnCollisionEnter2D �������ı��������ʱ����Ϊ
    new void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision); // ���û������ײ����

        // ����������һЩǿ��������ײ������⴦��
    }
}
