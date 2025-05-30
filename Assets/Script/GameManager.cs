using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timeText;

    public GameObject gameOverPanel;
    public GameObject gameClearPanel;

    private int score = 0;
    public int clearScore = 1000;

    private float timeLeft = 60f;

    public Player player;
    public Enemy enemy;


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
        scoreText.text = score.ToString("N0"); // ÉJÉìÉ}ãÊêÿÇË
    }

    void UpdateTimeText() {
        timeText.text = timeLeft.ToString("F1"); // è¨êî1åÖ
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
