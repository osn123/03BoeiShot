using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class GameManager : MonoBehaviour {
    public static GameManager Instance; // シングルトン
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timeText;

    public GameObject gameOverPanel;
    public GameObject gameClearPanel;

    private int score = 0;
    public int clearScore = 1000;

    private float timeLeft = 60f;

    public Player player;
    public Enemy enemy;

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
        gameOverPanel.SetActive(false);
        gameClearPanel.SetActive(false);
    }

    void Update() {
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
        gameOverPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void GameClear() {
        gameClearPanel.SetActive(true);
        Time.timeScale = 0f;
    }

}
