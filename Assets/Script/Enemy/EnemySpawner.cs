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

        // 画面の上下左右どこから出すかをランダムで決定
        int side = Random.Range(1, 4);
        Vector2 pos = Vector2.zero;
        switch (side) {
            case 1: // 上
                pos.x = Random.Range(-camWidth / 2,camWidth / 2);
                pos.y = camHeight / 2 + 1f;
                break;
            //case 1: // 下
            //    pos.x = Random.Range(-camWidth / 2,camWidth / 2);
            //    pos.y = -camHeight / 2 - 1f;
            //    break;
            case 2: // 左
                pos.x = -camWidth / 2 - 1f;
                pos.y = Random.Range(-camHeight / 2,camHeight / 2);
                break;
            case 3: // 右
                pos.x = camWidth / 2 + 1f;
                pos.y = Random.Range(-camHeight / 2,camHeight / 2);
                break;
        }
        // カメラの中心座標を加算
        Vector2 camCenter = cam.transform.position;
        return pos + camCenter;
    }
}
