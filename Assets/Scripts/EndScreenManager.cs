using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // ��Ȼ��Ҫ���� UnityEngine.UI����Ϊ��ť���������������ռ�
using TMPro; // ���� TextMeshPro �����ռ�

public class EndScreenManager : MonoBehaviour
{
    public Button returnToMainMenuButton; // ��ť����

    private void Start()
    {
        // ��Ӱ�ť����¼�������
        if (returnToMainMenuButton != null)
        {
            returnToMainMenuButton.onClick.AddListener(ReturnToMainMenu);
        }
        else
        {
            Debug.LogError("Return to Main Menu Button is not assigned.");
        }
    }

    private void ReturnToMainMenu()
    {
        // �ָ���Ϸʱ�䣨�������Ϸ����ʱ��ͣ��ʱ�䣩
        Time.timeScale = 0;

        // �������˵�����
        SceneManager.LoadScene("Main");
    }
}