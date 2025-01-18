using UnityEngine;
using static UnityEditor.Progress;

public class ItemSpawnArea : MonoBehaviour
{
    public float spawnAreaSize = 10f; // ��������ı߳�
    public float spawnInterval = 1f;  // ���ɼ��ʱ��
    public GameObject itemPrefab;     // ������Ʒ��Ԥ����
    private int itemNumber = 0;       // ��Ʒ������
    private float probability;

    void Start()
    {
        // ��������������Ʒ
        InvokeRepeating("SpawnItem", 0f, spawnInterval);
    }

    void OnDrawGizmos()
    {
        // ���� Gizmo ��ɫ
        Gizmos.color = Color.green;

        // ����һ�����������򣬱�ʾ��Ʒ���ɵ�����
        Gizmos.DrawWireCube(transform.position, new Vector3(spawnAreaSize, spawnAreaSize, 0f));
    }

    public void SpawnItem()
    {
        // ������� X �� Z ���꣬ȷ����������������
        float x = Random.Range(-spawnAreaSize / 2f, spawnAreaSize / 2f);
        float z = Random.Range(-spawnAreaSize / 2f, spawnAreaSize / 2f);

        // ������Ʒ�����λ��
        Vector3 randomPosition = new Vector3(x, 0f, z) + transform.position;

        // ������ɵ�λ��
        Debug.Log("��һ����Ʒ������: " + randomPosition);

        // ��ȡ��Ʒ����� Item ��������������ɸ���
        Item item = itemPrefab.GetComponent<Item>();
        if (item != null)
        {
           // probability = item.Generation_Probability;
        }

        // ������Ʒ�ĸ���
        float generationProbability = Random.Range(0f, 1f); // ����һ�� [0, 1) ֮������ֵ

        // ������Ʒ���ɸ���Ϊ 50%
        if (generationProbability <= probability)
        {
            // ʵ������Ʒ
            Instantiate(itemPrefab, randomPosition, Quaternion.identity);
            // ������Ʒ��������
            itemNumber++;
        }
        else
        {
            Debug.Log("���û��������Ʒ��");
        }
    }
}