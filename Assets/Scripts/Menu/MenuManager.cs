using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [Header("Menu Dependencies")]
    public Animator animator;
    public Transform difficultyPanel;
    public GameObject instructionsPanel;
    public GameObject[] difficultyButtons;
    public int difficultySelected = 1;

    void Start()
    {
        Buttons(1);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void GotoDifficulty()
    {
        //Camera.main.transform.Rotate(0f, 90f, 0f, Space.World);
        animator.SetTrigger("difficulty");
    }

    public void Instructions()
    {
        instructionsPanel.SetActive(true);
    }

    public void Back()
    {
        instructionsPanel.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Buttons(int selection)
    {
        for (int i = 0; i < difficultyButtons.Length; i++)
        {
            if (i == selection)
            {
                difficultyButtons[i].SetActive(true);
                continue;
            }
            difficultyButtons[i].SetActive(false);
        }
    }

    public void RightArrow()
    {
        if (difficultySelected < 3)
        {
            difficultyButtons[difficultySelected].SetActive(false);
            difficultySelected++;
            difficultyButtons[difficultySelected].SetActive(true);
        }
        else if (difficultySelected >= 3)
        {
            difficultyButtons[difficultySelected].SetActive(false);
            difficultySelected = 0;
            difficultyButtons[difficultySelected].SetActive(true);
        }
    }
    public void LeftArrow()
    {
        if (difficultySelected > 0)
        {
            difficultyButtons[difficultySelected].SetActive(false);
            difficultySelected--;
            difficultyButtons[difficultySelected].SetActive(true);
        }
        else if (difficultySelected <= 0)
        {
            difficultyButtons[difficultySelected].SetActive(false);
            difficultySelected = 3;
            difficultyButtons[difficultySelected].SetActive(true);
        }
    }
}
