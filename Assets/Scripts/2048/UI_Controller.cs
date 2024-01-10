using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_Controller : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI currentScore;
    [SerializeField]
    private TextMeshProUGUI highestScore;
    [SerializeField]
    private GameObject gameOverPanel;

    public void UpdateScore(int score)
    {
        currentScore.text = score.ToString();
    }

    public void UpdateHighestScore(int score)
    {
        highestScore.text = score.ToString();
    }

    public void OnGameOver()
    {
        gameOverPanel.SetActive(true);
    }

    public void OnHomeButton()
    {
        SceneManager.LoadScene(2);
    }

    public void OnYesButton()
    {
        SceneManager.LoadScene(3);
    }

    public void OnNoButton()
    {
        SceneManager.LoadScene(2);
    }
}
