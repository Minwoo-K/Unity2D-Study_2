using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PausePanel : MonoBehaviour
{
    [SerializeField]
    private GameObject pauseOverlayPanel;
    [SerializeField]
    private Animator animator;

    public void OnResumeButton()
    {
        PauseOff();
    }

    public void OnRestartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnExitButton()
    {
        SceneManager.LoadScene(0);
    }

    public void PauseOn()
    {
        pauseOverlayPanel.SetActive(true);
        animator.SetBool("PanelOn", true);
    }

    public void PauseOff()
    {
        pauseOverlayPanel.SetActive(false);
        animator.SetBool("PanelOn", false);
        gameObject.SetActive(false);
    }

}
