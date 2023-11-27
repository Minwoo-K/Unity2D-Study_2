using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    [Header("In-Game")]
    [SerializeField]
    private TextMeshProUGUI text_CurrentScore;
    [SerializeField]
    private TextMeshProUGUI text_BestScore;
    [SerializeField]
    private PausePanel pausePanel;

    [Header("Game Over")]
    [SerializeField]
    private GameObject resultPanel;
    [SerializeField]
    private TextMeshProUGUI text_ResultScore;
    [SerializeField]
    private Image resultImage;
    [SerializeField]
    private Screenshot screenshot;

    public void SetCurrentScore(int score)
    {
        text_CurrentScore.text = score.ToString();
    }

    public void SetBestScore(int score)
    {
        text_BestScore.text = PlayerPrefs.GetInt("BestScore").ToString();
    }

    public void OnPauseButton()
    {
        pausePanel.gameObject.SetActive(true);
        pausePanel.PauseOn();
    }

    public void GameOver()
    {

        resultPanel.SetActive(true);
        text_ResultScore.text = text_CurrentScore.text;
    }
}
