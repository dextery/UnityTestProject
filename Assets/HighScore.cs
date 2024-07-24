using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HighScore : MonoBehaviour
{
    public static HighScore Instance {get; private set;}
    PlayerScript player;

    private static float highScore;
    private static bool newHighScore;

    [SerializeField] private TextMeshProUGUI highScoreText;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        PlayerScript.GameOverEvent+= PlayerScript_GameOver;
        Debug.Log("Did a thing");
        if(PlayerPrefs.HasKey("Highscore"))
        {
            highScore = PlayerPrefs.GetFloat("Highscore"); 
        }
        else
        {
            highScore = 0;
        }
    }
    public void Update() 
    {
        if (highScoreText != null)
        {
            highScoreText.text = "High score: "+ highScore.ToString();
        }
        else
        {
            GameObject scoretext = GameObject.FindGameObjectWithTag("ScoreText");
            if (scoretext!=null)
            {
                highScoreText = scoretext.GetComponent<TextMeshProUGUI>();
            }
        }
    }

    public void SetNewHighScore(float value) 
    {
        highScore = value;
        newHighScore = true;
    }
    public float GetHighScore() 
    {
        return highScore;
    }
    public bool isHighScore() 
    {
        return newHighScore;
    }
    public void resetHighScoreBool() 
    {
        newHighScore=false;
    }
    private void PlayerScript_GameOver(float value)
    {
        Debug.Log("Whopee hooray, event fired");
        if (value > PlayerPrefs.GetFloat("Highscore"))
        {
            PlayerPrefs.SetFloat("Highscore", value);
            SetNewHighScore(value);
        }
        PlayerScript.GameOverEvent -= PlayerScript_GameOver;
    }
}
