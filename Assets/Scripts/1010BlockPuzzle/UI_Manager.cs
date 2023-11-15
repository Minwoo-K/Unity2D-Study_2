using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_Manager : MonoBehaviour
{
    [Header("UI Configuration")]
    [SerializeField]
    private TextMeshProUGUI currentScoreText;
    [SerializeField]
    private TextMeshProUGUI highestScoreText;
    [SerializeField]
    private PausePanel pausePanel;

    public int CurrentScore { get; set; }
    public int HighestScore { get; set; }

    private void Awake()
    {
        CurrentScore = 0;
        HighestScore = PlayerPrefs.GetInt("HighestScore");
        highestScoreText.text = HighestScore.ToString();
    }

    private void Update()
    {
        currentScoreText.text = CurrentScore.ToString();
    }

    public void OnPauseButton()
    {
        pausePanel.PauseON();
    }

    public void OnButtonResume()
    {
        pausePanel.PauseOFF();
    }

    public void OnButtonNewGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnButtonExit()
    {
        SceneManager.LoadScene(0);
    }
}
