using DG.Tweening.Core.Easing;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform target;
    public float speed = 2f;

    public int maxHP = 3;
    private int currentHP;
    public int scoreValue = 100;
    public GameManager gameManager;

    void Start() {
        currentHP = maxHP;
        gameManager=gameObject.GetComponent<GameManager>();
    }
    void Update()
    {
        if (target != null)
        {
            Vector2 dir = (target.position - transform.position).normalized;
            transform.position += (Vector3)dir * speed * Time.deltaTime;
        }
    }

    // 4. 敵の当たり判定設定
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerBullet"))
        {
            TakeDamage(1); // 弾に当たったらダメージを受ける
            //Destroy(gameObject); // 弾に当たったら消える
        }
        if (other.CompareTag("Player") ) {
            Destroy(gameObject);
        }
    }
    void TakeDamage(int damage) {
        currentHP -= damage;
        if (currentHP <= 0) {
            //gameManager.AddScore(scoreValue);
            Destroy(gameObject);
        }
    }
}
