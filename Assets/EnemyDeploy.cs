using UnityEngine;

public class FastEnemyPlane : EnemyPlane
{
    // 快速敌人特有的属性
    public float additionalSpeed = 5f;

    // 重写 Start 方法来修改基础类中的一些参数
    new void Start()
    {
        base.Start(); // 调用基类的 Start 方法

        // 设置更高的速度
        speed += additionalSpeed;
    }

    // 如果需要，可以重写 Update 方法来实现不同的行为
    new void Update()
    {
        base.Update(); // 保留基类的移动和攻击逻辑

        // 你可以在此添加其他行为，例如快速敌人的特殊行为
        // 比如快速改变移动方向
    }
}

public class TankEnemyPlane : EnemyPlane
{
    // 强力敌人特有的属性
    public int additionalHealth = 50;
    public float attackDamageMultiplier = 2f;

    // 重写 Start 方法
    new void Start()
    {
        base.Start();

        // 设置更高的生命值
        enemyHealth += additionalHealth;

        // 修改攻击力（可以通过修改其他属性或方法实现）
        bulletSpeed *= attackDamageMultiplier;
    }

    // 重写 Update 方法
    new void Update()
    {
        base.Update(); // 保留基类的行为

        // 强力敌人的特殊攻击方式，或者其他额外行为
    }

    // 重写 OnCollisionEnter2D 方法来改变敌人死亡时的行为
    new void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision); // 调用基类的碰撞处理

        // 例如可以添加一些强力敌人碰撞后的特殊处理
    }
}
