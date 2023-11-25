using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_Manager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI text_CurrentScore;
    [SerializeField]
    private TextMeshProUGUI text_BestScore;

    public void SetCurrentScore(int score)
    {
        text_CurrentScore.text = score.ToString();
    }

    public void SetBestScore(int score)
    {
        text_BestScore.text = PlayerPrefs.GetInt("BestScore").ToString();
    }
}
