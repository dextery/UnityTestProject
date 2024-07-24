using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverScreen : MonoBehaviour
{
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private TextMeshProUGUI highScoreStatus;
    [SerializeField] private GameObject buttonToHide;

    private bool highScoreStat = false;
    void Start()
    {
        PlayerScript.GameOverEvent+= PlayerScript_GameOver;
        buttonToHide = GameObject.FindGameObjectWithTag("Exit");
    }
    private void PlayerScript_GameOver(float value)
    {
        buttonToHide.gameObject.SetActive(false);
        gameOverPanel.SetActive(true);
        highScoreStat = HighScore.Instance.isHighScore();
        if (highScoreStat)
        {
            highScoreStatus.text = "You got a high score!";
        }
        else
        {
            highScoreStatus.text = "You did not get a high score!";
        }
        HighScore.Instance.resetHighScoreBool();
        //PlayerScript.GameOverEvent-= PlayerScript_GameOver;
    }
}
