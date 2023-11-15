using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausePanel : MonoBehaviour
{
    [SerializeField]
    private GameObject pausedBackground;
    [SerializeField]
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void PauseON()
    {
        pausedBackground.SetActive(true);
        gameObject.SetActive(true);
        animator.SetTrigger("PauseON");
    }
    
    public void PauseOFF()
    {
        animator.SetTrigger("PauseOFF");
    }

    public void AfterPauseOFF()
    {
        pausedBackground.SetActive(false);
        gameObject.SetActive(false);
    }

}
