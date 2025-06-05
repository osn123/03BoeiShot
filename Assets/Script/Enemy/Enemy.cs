using DG.Tweening.Core.Easing;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public Transform target;
    public float speed = 2f;

    public float maxHP = 3;
    private float currentHP;
    public int scoreValue = 100;
    public GameManager gameManager;

    public Image barImage; // HPバー本体のImage
    public AudioClip damageSE;
    private AudioSource audioSource;

    void Start() {
        currentHP = maxHP;
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
        if (other.CompareTag("Player") ) {
            var player = other.GetComponent<Player>();
            if (player != null) {
                player.TakeDamage(1);
                Destroy(gameObject);
            }            
        }
    }
    public void TakeDamage(float damage) {
        currentHP -= damage;
       // audioSource.PlayOneShot(damageSE);
        float ratio = Mathf.Clamp01((float)currentHP / maxHP);
        barImage.rectTransform.sizeDelta = new Vector2(ratio * barImage.rectTransform.sizeDelta.x,barImage.rectTransform.sizeDelta.y); // 

        if (currentHP <= 0) {
            GameManager.Instance.AddScore(scoreValue);
            Destroy(gameObject);
            //TODO : エフェクト
        }
    }
}
