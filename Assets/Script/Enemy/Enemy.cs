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

    public float zigzagAmplitude = 1f;
    public float zigzagFrequency = 2f;
    public float curveRadius = 2f;
    public float curveSpeed = 1f;

    private MovePattern movePattern;
    private float timeOffset; // 

    public GameManager gameManager;

    public Image barImage; // HPバー本体のImage
    public AudioClip damageSE;
    public AudioClip deadSE;
    private AudioSource audioSource;

    private Animator animator;

    public enum MovePattern
    {
        Straight,
        Zigzag,
        Curve
    }


    void Start() {
        currentHP = maxHP;
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        // ランダムで動きパターンを選択
        movePattern = (MovePattern)Random.Range(0, System.Enum.GetValues(typeof(MovePattern)).Length);
        timeOffset = Random.Range(50f, 100f); //
    }
    void Update()
    {
        if (target != null)
        {
            //Vector2 dir = (target.position - transform.position).normalized;
            //transform.position += (Vector3)dir * speed * Time.deltaTime;
        }
        switch (movePattern)
        {
            case MovePattern.Straight:
                MoveStraight();
                break;
            case MovePattern.Zigzag:
                MoveZigzag();
                break;
            case MovePattern.Curve:
                MoveCurve();
                break;
        }
    }

    // 4. 敵の当たり判定設定
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") ) {
            animator.SetBool("Atk", true); // アニメーションをAttackに切り替え
            var player = other.GetComponent<Player>();
            if (player != null) {
                player.TakeDamage(1);
                Destroy(gameObject,1);
            }
            audioSource.PlayOneShot(damageSE);
        }
    }
    public void TakeDamage(float damage) {
        currentHP -= damage;
        audioSource.PlayOneShot(damageSE);
        float ratio = Mathf.Clamp01((float)currentHP / maxHP);
        barImage.rectTransform.sizeDelta = new Vector2(ratio * barImage.rectTransform.sizeDelta.x,barImage.rectTransform.sizeDelta.y); // 

        if (currentHP <= 0) {
            GameManager.Instance.AddScore(scoreValue);
            audioSource.PlayOneShot(deadSE);
            Destroy(gameObject,0.3f);
            //TODO : エフェクト
        }
    }
    void MoveStraight()
    {
        Vector2 dir = (target.position - transform.position).normalized;
        transform.position += (Vector3)dir * speed * Time.deltaTime;
    }

    void MoveZigzag()
    {
        Vector2 dir = (target.position - transform.position).normalized;
        Vector2 perp = new Vector2(-dir.y, dir.x); // 垂直方向
        float zigzag = Mathf.Sin((Time.time + timeOffset) * zigzagFrequency) * zigzagAmplitude;
        Vector3 move = (Vector3)dir + (Vector3)perp * zigzag;
        transform.position += move.normalized * speed * Time.deltaTime;
    }

    void MoveCurve()
    {
        Vector2 dir = (target.position - transform.position).normalized;
        Vector2 perp = new Vector2(-dir.y, dir.x); // 垂直方向
        float angle = Mathf.Sin((Time.time + timeOffset) * curveSpeed) * curveRadius;
        Vector3 move = Quaternion.AngleAxis(angle, Vector3.forward) * dir;
        transform.position += move.normalized * speed * Time.deltaTime;
    }
}
