using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Generated_Pos : MonoBehaviour
{
    public GameObject playerPrefab; // ��ҷɻ���Ԥ����
    public Transform[] spawnPoints; // �洢���ɵ������

    private GameObject playerInstance; // ���ʵ��

    void Start()
    {
        // ���ú��������λ��������ҷɻ�
        SpawnPlayerAtRandomPosition();
    }

    void SpawnPlayerAtRandomPosition()
    {
        if (spawnPoints.Length > 0)
        {
            // ���ѡ��һ�����ɵ�
            int randomIndex = Random.Range(0, spawnPoints.Length);
            Transform spawnPoint = spawnPoints[randomIndex];

            // ��ѡ����λ��������ҷɻ�������ȡʵ������
            playerInstance = Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);

            // ������ɵ���ҷɻ�ʵ����λ��
            Debug.Log("�������λ�ã�" + playerInstance.transform.position);

            // ����ȫ�����λ��
            if (GameManager.Instance != null)
            {
                GameManager.Instance.UpdatePlayerPosition(playerInstance.transform.position);
            }
        }
        else
        {
            Debug.LogError("���ɵ�����Ϊ�գ�");
        }
    }

    void Update()
    {
        // ÿ֡����ȫ�����λ��
        if (playerInstance != null)
        {
            GameManager.Instance.UpdatePlayerPosition(playerInstance.transform.position);
        }
        else
        {
            Debug.LogWarning("��ҷɻ�ʵ����δ���ɣ�");
        }
    }
}
