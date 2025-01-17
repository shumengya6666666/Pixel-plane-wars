using UnityEngine;

public class StartGameButton : MonoBehaviour
{
    // ��Ҫ��Inspector�з�����Щ����
    public GameObject startMenuUI; // ��ʼ�˵���UI����
    public GameObject gameUI;      // ��Ϸ�������UI����

    // ����������ɰ�ť��OnClick�¼�����
    public void OnStartButtonClick()
    {
        // ���UI�����Ƿ��ѷ���
        if (startMenuUI != null && gameUI != null)
        {
            // ���ؿ�ʼ�˵�
            startMenuUI.SetActive(false);

            // ��ʾ��ϷUI
            gameUI.SetActive(true);
        }
        else
        {
            Debug.LogError("UI objects not assigned in the Inspector.");
        }
    }
}