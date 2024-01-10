using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainScene : MonoBehaviour
{
    [SerializeField]
    private Sprite[] spritesMatrix;
    [SerializeField]
    private Image matrix_Image;
    [SerializeField]
    private TextMeshProUGUI matrix_Text;

    private int matrixIndex = 0;

    public void OnLeftButton()
    {
        matrixIndex = matrixIndex > 0 ? matrixIndex-1 : spritesMatrix.Length-1;

        matrix_Image.sprite = spritesMatrix[matrixIndex];
        matrix_Text.text = spritesMatrix[matrixIndex].name;
    }

    public void OnRightButton()
    {
        matrixIndex = matrixIndex == spritesMatrix.Length-1 ? 0 : matrixIndex+1;

        matrix_Image.sprite = spritesMatrix[matrixIndex];
        matrix_Text.text = spritesMatrix[matrixIndex].name;
    }

    public void OnStartButton()
    {
        PlayerPrefs.SetInt("BoardCount", matrixIndex + 3);
        SceneManager.LoadScene(3);
    }

    public void OnExitButton()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.ExitPlaymode();
        #else
            Application.Quit();
        #endif
    }
}
