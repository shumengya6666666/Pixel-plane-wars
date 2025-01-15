using UnityEngine;

public class EnemySpawnArea : MonoBehaviour
{
    public float spawnRadius = 10f;  // ��������뾶
    public float spawnInterval = 1f; // ���ɼ��ʱ��
    public GameObject enemyPrefab;   // ���˷ɻ���Ԥ����
    private int EnemyNumber = 0;

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

        // ���ɵ��˷ɻ�
        Vector3 randomPosition = new Vector3(x, y, 0f);

        // ������ɵ�λ��
        Debug.Log("��һ������������: " + randomPosition);
        EnemyNumber++;
        GameManager.Instance.UpdateEnemyNumber(EnemyNumber);

        // ʵ�������˷ɻ�
        Instantiate(enemyPrefab, randomPosition, Quaternion.identity);
    }


}
