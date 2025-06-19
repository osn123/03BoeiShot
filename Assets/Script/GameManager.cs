using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class GameManager : MonoBehaviour {
    public static GameManager Instance; // シングルトン
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timeText;

    public GameObject gameStartPanel;
    public GameObject gameOverPanel;
    public GameObject gameClearPanel;

    public AudioClip clearSE;
    public AudioClip endSE;
    private AudioSource audioSource;

    private int score = 0;
    public int clearScore = 1000;

    private float timeLeft = 60f;

    public Player player;
    public Enemy enemy;
    bool startflg=false;

    void Awake() {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start() {
        score = 0;
        UpdateScoreText();
        UpdateTimeText();

       // gameStartPanel.SetActive(false);
        gameOverPanel.SetActive(false);
        gameClearPanel.SetActive(false);

        audioSource = GetComponent<AudioSource>();
    }

    void Update() {


        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
            Debug.Log("Quit Game."); // エディタで動作確認用
        }
        //if (Input.GetMouseButtonUp(0)&& startflg)
        //{
        //    gameStartPanel.SetActive(false);
        //    startflg = true;
        //}
        if (timeLeft > 0f) {
            timeLeft -= Time.deltaTime;
            if (timeLeft < 0f)
                timeLeft = 0f;
            UpdateTimeText();
        }
    }

    public void AddScore(int value) {
        score += value;
        UpdateScoreText();
        if (score >= clearScore) {
            GameClear();
        }
    }

    void UpdateScoreText() {
        if (scoreText != null) {
            scoreText.text = score.ToString(); // スコア: を追加
        }
    }

    void UpdateTimeText() {
        timeText.text = timeLeft.ToString("F1"); // 小数1桁
    }
    public void GameOver() {
        audioSource.PlayOneShot(endSE);
        gameOverPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void GameClear() {
        audioSource.PlayOneShot(clearSE);
        gameClearPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    // TODO: キー入力でゲーム開始

}
