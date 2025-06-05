using UnityEngine;

public class EnemySpawner : MonoBehaviour {
    public GameObject enemyPrefab;
    public float spawnInterval = 2f;
    public Transform target;

    void Start() {
        //InvokeRepeating(nameof(SpawnEnemy),1f,spawnInterval);
        InvokeRepeating("SpawnEnemy",1f,spawnInterval);
    }

    void SpawnEnemy() {
        Vector2 spawnPos = GetRandomOutsidePosition();
        GameObject enemy = Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
        enemy.GetComponent<Enemy>().target = target;
    }

    Vector2 GetRandomOutsidePosition() {
        Camera cam = Camera.main;
        float camHeight = 2f * cam.orthographicSize;
        float camWidth = camHeight * cam.aspect;

        // ��ʂ̏㉺���E�ǂ�����o�����������_���Ō���
        int side = Random.Range(1, 4);
        Vector2 pos = Vector2.zero;
        switch (side) {
            case 1: // ��
                pos.x = Random.Range(-camWidth / 2,camWidth / 2);
                pos.y = camHeight / 2 + 1f;
                break;
            //case 1: // ��
            //    pos.x = Random.Range(-camWidth / 2,camWidth / 2);
            //    pos.y = -camHeight / 2 - 1f;
            //    break;
            case 2: // ��
                pos.x = -camWidth / 2 - 1f;
                pos.y = Random.Range(-camHeight / 2,camHeight / 2);
                break;
            case 3: // �E
                pos.x = camWidth / 2 + 1f;
                pos.y = Random.Range(-camHeight / 2,camHeight / 2);
                break;
        }
        // �J�����̒��S���W�����Z
        Vector2 camCenter = cam.transform.position;
        return pos + camCenter;
    }
}
