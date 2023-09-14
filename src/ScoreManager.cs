using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public Text scoreText;
    public Text highscoreText;

    public int score = 0;
    int highscore = 0;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        highscore = PlayerPrefs.GetInt("highscore", 0);
        UpdateHighscoreText(); 
    }

    public void AddPoints()
    {
        score += 10;
        scoreText.text = score.ToString() + " Points";
        if (score > highscore)
        {
            highscore = score; 
            PlayerPrefs.SetInt("highscore", highscore);
            UpdateHighscoreText(); 
        }
    }

    public void ResetScore()
    {
        score = 0;
        scoreText.text = score.ToString() + " Points";
    }

   public void UpdateHighscoreText()
    {
        highscoreText.text = "Highscore " + highscore.ToString();
    }
    
    
}
