using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }  // ����ģʽ
    public Vector3 PlayerPosition { get; private set; }  // �洢���λ��
    public int EnemyNumvber { get; private set; }
    public int PlayerHealth { get; private set; }
    public int PlayerExperience { get; private set; }
    public int PlayerLevel { get; private set; }

    public int Money { get; private set; }

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
