using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnInterval = 2f;
    public float spawnRange = 8f; // 画面端の範囲
    public Transform target;      // プレイヤーや拠点

    void Start() {
        InvokeRepeating("SpawnEnemy",1f,spawnInterval);
    }

    void SpawnEnemy() {
        Vector2 spawnPos = new Vector2(Random.Range(-spawnRange, spawnRange), Random.Range(-spawnRange, spawnRange));
        GameObject enemy = Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
        enemy.GetComponent<Enemy>().target = target;
    }
}
