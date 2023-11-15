using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_Manager : MonoBehaviour
{
    [Header("UI Configuration")]
    [SerializeField]
    private TextMeshProUGUI currentScoreText;
    [SerializeField]
    private TextMeshProUGUI highestScoreText;

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
}
