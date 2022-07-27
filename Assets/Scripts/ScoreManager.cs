using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public int score;
    public int highScore;
    public GameObject scoreText;
    public GameObject goscoreText;
    public GameObject highScoreText;

    public void IncreaseScore() {
        score++;
        scoreText.GetComponent<TextMeshProUGUI>().SetText(score.ToString());
    }

    public void CheckForHighScore() {
        highScore = PlayerPrefs.GetInt("HighScore");
        if (score > highScore) {
            PlayerPrefs.SetInt("HighScore", score);
            highScoreText.GetComponent<TextMeshProUGUI>().SetText(score.ToString());
        }
        DisplayScore();
        highScoreText.GetComponent<TextMeshProUGUI>().SetText(highScore.ToString());
    }

    public void DisplayScore() {
        goscoreText.GetComponent<TextMeshProUGUI>().SetText(score.ToString());
    }
}
