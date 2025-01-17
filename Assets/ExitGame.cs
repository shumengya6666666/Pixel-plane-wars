using UnityEngine;
using UnityEngine.UI;

public class ExitGame : MonoBehaviour
{
    // �˳���ť������
    public Button exitButton;

    void Start()
    {
        // Ϊ��ť��ӵ���¼�����
        exitButton.onClick.AddListener(OnExitButtonClick);
    }

    // �����ťʱ�˳���Ϸ
    void OnExitButtonClick()
    {
        // ȷ���˳���Ϸʱ����ʾ�����ڱ༭ģʽ���˳�ʱ��
#if UNITY_EDITOR
        // �ڱ༭ģʽ��ֹͣ����
        UnityEditor.EditorApplication.isPlaying = false;
#else
            // �ڹ�������Ϸ���˳�
            Application.Quit();
#endif
    }
}
