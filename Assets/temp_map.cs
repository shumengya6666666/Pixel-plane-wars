using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempMap : MonoBehaviour
{
    public Sprite sprite;  // 用于存储精灵
    public GameObject spritePrefab;  // 用于创建精灵的预设

    void Start()
    {
        GenerateMap();
    }

    void GenerateMap()
    {
        // 4x4 遍历生成大地图
        int rows = 15;  // 行数
        int columns = 15;  // 列数

        // 获取精灵的尺寸（单位：世界坐标）
        float spriteWidth = sprite.bounds.size.x;
        float spriteHeight = sprite.bounds.size.y;

        // 使用精灵的宽高作为间距，确保它们紧密排列
        float spacingX = spriteWidth;  // 横向排列的间距
        float spacingY = spriteHeight;  // 纵向排列的间距

        // 遍历行列生成精灵
        for (int x = 0; x < columns; x++)  // 遍历列
        {
            for (int y = 0; y < rows; y++)  // 遍历行
            {
                // 创建一个新的游戏对象
                GameObject newTile = new GameObject("Tile_" + x + "_" + y);

                // 给这个对象添加精灵渲染器
                SpriteRenderer spriteRenderer = newTile.AddComponent<SpriteRenderer>();
                spriteRenderer.sprite = sprite;  // 设置精灵

                // 设置精灵的位置
                newTile.transform.position = new Vector3(x * spacingX, y * spacingY, 0);
            }
        }
    }
}
