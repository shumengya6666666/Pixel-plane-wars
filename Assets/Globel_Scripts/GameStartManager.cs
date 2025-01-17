using UnityEngine;
using UnityEngine.UI;

public class GameStartManager : MonoBehaviour
{
    public GameObject startPanel; // ��ʼ��������
    public Button startGameButton; // ��ʼ��Ϸ��ť

    void Start()
    {
        // ��ͣ��Ϸ
        Time.timeScale = 0;

        // ȷ�������ʾ
        startPanel.SetActive(true);

        // Ϊ��ť��ӵ���¼�
        startGameButton.onClick.AddListener(OnStartGameClicked);
    }

    void OnStartGameClicked()
    {
        // �ָ���Ϸ
        Time.timeScale = 1;

        // ���ؿ�ʼ����
        startPanel.SetActive(false);
    }
}
