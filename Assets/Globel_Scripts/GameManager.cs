using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get;set; }  // ����ģʽ
    public Vector3 PlayerPosition { get; set; }  // �洢���λ��
    public int EnemyNumvber { get;set; }
    public int PlayerHealth { get; set; }
    public int PlayerExperience { get; set; }
    public int PlayerLevel { get;set; }
    public int Money { get;  set; }
    public string Survival_Time { get;set; }
    public float timer; // ��ʱ������

    private void Start()
    {
        // ����ȫ������Ϊ (0, 0, 0)
        Physics.gravity = Vector3.zero;
    }


    void Awake()
    {
        // ȷ��ֻ��һ�� GameManager ʵ��
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);  // ����Ѿ���һ��ʵ�������ٵ�ǰʵ��
        }

        // �����ٸ����壬�Ա��ڳ����л�ʱ��Ȼ����
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        // ���Ӽ�ʱ����ʱ��
        timer += Time.deltaTime;

        // ����Сʱ�����Ӻ���
        int hours = Mathf.FloorToInt(timer / 3600);
        int minutes = Mathf.FloorToInt((timer % 3600) / 60);
        int seconds = Mathf.FloorToInt(timer % 60);

        // ��ʽ��Ϊ "00:00:00" ����ʽ
        Survival_Time = string.Format("{0:D2}:{1:D2}:{2:D2}", hours, minutes, seconds);

    }

    public void UpdatePlayerPosition(Vector3 newPosition)
    {
        PlayerPosition = newPosition;  // �������λ��
    }

    public void UpdateEnemyNumber(int newNumber)
    {
        EnemyNumvber = newNumber;  // ����
    }

    public void UpdatePlayerHealth(int newHealth)
    {
        PlayerHealth = newHealth;  // �������
    }

    public void UpdatePlayerExperience(int newExperience)
    {
        PlayerExperience = newExperience;  // �������
    }

    public void UpdatePlayerLevel(int newPlayerLevel)
    {
        PlayerLevel = newPlayerLevel;  // �������
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
