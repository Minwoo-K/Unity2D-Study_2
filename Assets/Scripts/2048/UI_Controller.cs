using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_Controller : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI currentScore;
    [SerializeField]
    private TextMeshProUGUI highestScore;

    public void UpdateScore(int score)
    {
        currentScore.text = score.ToString();
    }

    public void UpdateHighestScore(int score)
    {
        highestScore.text = score.ToString();
    }
}
