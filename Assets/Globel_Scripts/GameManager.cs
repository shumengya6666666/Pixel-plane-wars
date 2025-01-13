using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }  // ����ģʽ
    public Vector3 PlayerPosition { get; private set; }  // �洢���λ��

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
}
