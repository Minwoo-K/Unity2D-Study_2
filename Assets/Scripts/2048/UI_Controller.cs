using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_Controller : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI currentScore;

    public void UpdateScore(int score)
    {
        currentScore.text = score.ToString();
    }
}
