using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {
    public GameObject bulletPrefab; // 弾のプレハブ
    public float bulletSpeed = 10f;
    public float lifetime = 3f;

    public int maxHP = 3;
    private int currentHP;
    public GameManager gameManager;
    public Image barImage; // HPバー本体のImage

    public AudioClip damageSE;
    private AudioSource audioSource;

    void Start() {
        currentHP = maxHP;
        audioSource = GetComponent<AudioSource>();
    }
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

    public void TakeDamage(int damage) {
        currentHP -= damage;
        audioSource.PlayOneShot(damageSE);

        float ratio = Mathf.Clamp01((float)currentHP / maxHP);
        //Debug.Log("ratio: " + ratio,gameObject);
        barImage.rectTransform.sizeDelta = new Vector2(ratio * barImage.rectTransform.sizeDelta.x,barImage.rectTransform.sizeDelta.y); // 
        //Debug.Log("barImage.rectTransform.sizeDelta.x: " + barImage.rectTransform.sizeDelta.x,gameObject);

        if (currentHP <= 0) {
            gameManager.GameOver();
            Destroy(gameObject);
        }
    }
}
