using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timeText;

    private int score = 0;
    private float timeLeft = 60f;

    void Start() {
        UpdateScoreText();
        UpdateTimeText();
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
    }

    void UpdateScoreText() {
        scoreText.text = score.ToString("N0"); // ƒJƒ“ƒ}‹æØ‚è
    }

    void UpdateTimeText() {
        timeText.text = timeLeft.ToString("F1"); // ¬”1Œ…
    }
}
