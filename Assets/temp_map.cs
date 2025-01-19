using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempMap : MonoBehaviour
{
    public Sprite sprite;  // ���ڴ洢����
    public GameObject spritePrefab;  // ���ڴ��������Ԥ��

    void Start()
    {
        GenerateMap();
    }

    void GenerateMap()
    {
        // 4x4 �������ɴ��ͼ
        int rows = 15;  // ����
        int columns = 15;  // ����

        // ��ȡ����ĳߴ磨��λ���������꣩
        float spriteWidth = sprite.bounds.size.x;
        float spriteHeight = sprite.bounds.size.y;

        // ʹ�þ���Ŀ����Ϊ��࣬ȷ�����ǽ�������
        float spacingX = spriteWidth;  // �������еļ��
        float spacingY = spriteHeight;  // �������еļ��

        // �����������ɾ���
        for (int x = 0; x < columns; x++)  // ������
        {
            for (int y = 0; y < rows; y++)  // ������
            {
                // ����һ���µ���Ϸ����
                GameObject newTile = new GameObject("Tile_" + x + "_" + y);

                // �����������Ӿ�����Ⱦ��
                SpriteRenderer spriteRenderer = newTile.AddComponent<SpriteRenderer>();
                spriteRenderer.sprite = sprite;  // ���þ���

                // ���þ����λ��
                newTile.transform.position = new Vector3(x * spacingX, y * spacingY, 0);
            }
        }
    }
}
