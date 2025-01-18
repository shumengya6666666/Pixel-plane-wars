using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get;set; }  // 单例模式
    public Vector3 PlayerPosition { get; set; }  // 存储玩家位置
    public int EnemyNumvber { get;set; }
    public int PlayerHealth { get; set; }
    public int PlayerExperience { get; set; }
    public int PlayerLevel { get;set; }
    public int Money { get;  set; }
    public string Survival_Time { get;set; }
    public float timer; // 计时器变量

    private void Start()
    {
        // 设置全局重力为 (0, 0, 0)
        Physics.gravity = Vector3.zero;
    }


    void Awake()
    {
        // 确保只有一个 GameManager 实例
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);  // 如果已经有一个实例，销毁当前实例
        }

        // 不销毁该物体，以便在场景切换时仍然存在
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        // 增加计时器的时间
        timer += Time.deltaTime;

        // 计算小时、分钟和秒
        int hours = Mathf.FloorToInt(timer / 3600);
        int minutes = Mathf.FloorToInt((timer % 3600) / 60);
        int seconds = Mathf.FloorToInt(timer % 60);

        // 格式化为 "00:00:00" 的形式
        Survival_Time = string.Format("{0:D2}:{1:D2}:{2:D2}", hours, minutes, seconds);

    }

    public void UpdatePlayerPosition(Vector3 newPosition)
    {
        PlayerPosition = newPosition;  // 更新玩家位置
    }

    public void UpdateEnemyNumber(int newNumber)
    {
        EnemyNumvber = newNumber;  // 更新
    }

    public void UpdatePlayerHealth(int newHealth)
    {
        PlayerHealth = newHealth;  // 更新玩家
    }

    public void UpdatePlayerExperience(int newExperience)
    {
        PlayerExperience = newExperience;  // 更新玩家
    }

    public void UpdatePlayerLevel(int newPlayerLevel)
    {
        PlayerLevel = newPlayerLevel;  // 更新玩家
    }

    public void UpdateMoney(int newMoney)
    {
        Money = newMoney;
    }

    public void AddMoney(int newMoney)
    {
        Money += newMoney;
    }

    public void AddPlayerExperience(int newExperience)
    {
        PlayerExperience += newExperience;
    }

}
