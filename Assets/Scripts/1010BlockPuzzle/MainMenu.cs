using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI highestScoreText;

    private void Awake()
    {
        highestScoreText.text = "<size=50>BEST SCORE</size>\n" + PlayerPrefs.GetInt("HighestScore").ToString();
    }

    public void ButtonGameStart()
    {
        SceneManager.LoadScene(1);
    }

    public void ButtonExit()
    {
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                Application.Quit();
        #endif
    }
}
