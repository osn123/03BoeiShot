using UnityEngine;

public class Player : MonoBehaviour {
    public GameObject bulletPrefab; // 弾のプレハブ
    public float bulletSpeed = 10f;
    public float lifetime = 3f; 

    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            // マウス位置をワールド座標に変換
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = Mathf.Abs(Camera.main.transform.position.z); // カメラからの距離
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);

            // 発射方向を計算
            Vector2 dir = (worldPos - transform.position).normalized;

            // 弾を生成して発射
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            if (rb != null) {
                rb.linearVelocity = dir * bulletSpeed;
                Destroy(bullet,lifetime); // lifetime後に弾を破棄
            }
        }
    }
}
