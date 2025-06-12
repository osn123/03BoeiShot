using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    //public int damage = 1;
    public float damage ;
    public GameObject effect;
    public Sprite normalSprite;      // 通常のスプライト
    public Sprite weakSprite;        // damage<4用のスプライト

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        // 初期スプライト設定
        if (damage < 4)
        {
            spriteRenderer.sprite = weakSprite;
        }
        else
        {
            spriteRenderer.sprite = normalSprite;
        }
    }

        void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
                //Rigidbody2D rb = this.GetComponent<Rigidbody2D>();
                //enemy.TakeDamage((int)rb.linearVelocity.magnitude);
            }
            // エフェクトを発生させて1秒後に消す
            GameObject ef = Instantiate(effect, transform.position, Quaternion.identity);
            Renderer renderer = ef.GetComponent<Renderer>();
            renderer.sortingOrder = 10;
            Destroy(ef, 1f);
            if (damage < 4)
            {
            Destroy(gameObject);
            }
        }
    }
}
