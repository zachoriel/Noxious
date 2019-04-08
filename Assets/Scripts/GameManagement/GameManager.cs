using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public KeyCode[] pauseButtons;
    public GameObject pauseMenu;
    bool input;


    void Start()
    {
        if (pauseButtons[0] == KeyCode.None)
            pauseButtons[0] = KeyCode.Escape;
        if (pauseButtons[1] == KeyCode.None)
            pauseButtons[1] = KeyCode.P;
    }

    void Update()
    {
        input = Input.GetKeyDown(pauseButtons[0]) || Input.GetKeyDown(pauseButtons[1]);

        if (input)
        {
            PauseGame();
        }
    }

    void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void QuitGame()
    {
        Time.timeScale = 1f;
        //DifficultySelection.instance.difficulty = DifficultySelection.Difficulties.normal;
        SceneFader.instance.FadeTo("MainMenu");
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
