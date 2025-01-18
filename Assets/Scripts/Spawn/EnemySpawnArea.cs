using UnityEngine;

public class EnemySpawnArea : MonoBehaviour
{
    public float spawnRadius = 10f;  // ��������뾶
    public float spawnInterval = 1f; // ���ɼ��ʱ��
    public GameObject[] enemyPrefabs;  // ����Ԥ��������
    private int EnemyNumber = 0;
    private float Probability;

    void Start()
    {
        // �������ɵ��˷ɻ�
        InvokeRepeating("SpawnEnemy", 0f, spawnInterval);
    }

    void OnDrawGizmos()
    {
        // ���� Gizmo ��ɫ
        Gizmos.color = Color.red;

        // ����һ���뾶Ϊ spawnRadius ��Բ�Σ���ʾ�������ɵ�����
        Gizmos.DrawWireSphere(transform.position, spawnRadius);
    }

    public void SpawnEnemy()
    {
        // ����ǶȺͰ뾶
        float angle = Random.Range(0f, 2 * Mathf.PI);  // ����Ƕ�
        float radius = Random.Range(0f, spawnRadius);  // ����뾶��ȷ��������Բ������

        // ת��Ϊ�ѿ�������ϵ (X, Z)
        float x = transform.position.x + radius * Mathf.Cos(angle);
        float y = transform.position.y + radius * Mathf.Sin(angle);

        // ���ɵ��˷ɻ������λ��
        Vector3 randomPosition = new Vector3(x, y, 0f);

        // ������ɵ�λ��
        Debug.Log("��һ������������: " + randomPosition);

        GameManager.Instance.UpdateEnemyNumber(EnemyNumber);

        // �ӵ���Ԥ�������������ѡ��һ��
        int index = Random.Range(0, enemyPrefabs.Length);
        GameObject selectedPrefab = enemyPrefabs[index];

        // ��ȡ���˶���� EnemyPlane ��������������ɸ���
        EnemyPlane enemyPlane = selectedPrefab.GetComponent<EnemyPlane>();
        if (enemyPlane != null)
        {
            Probability = enemyPlane.Generation_Probability;
        }

        // ���ɵ��˵ĸ���
        float generationProbability = Random.Range(0f, 1f); // ����һ�� [0, 1) ֮������ֵ

        // ����������ɸ���Ϊ 50%
        if (generationProbability <= Probability)
        {
            // ʵ�������˷ɻ�
            Instantiate(selectedPrefab, randomPosition, Quaternion.identity);
            // ���ɵ�����������
            EnemyNumber++;
        }
        else
        {
            Debug.Log("���û�����ɵ��ˡ�");
        }
    }
}
