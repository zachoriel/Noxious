using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuManager : MonoBehaviour
{
    [Header("Menu Dependencies")]
    public GameObject instructionsPanel;
    public GameObject settingsPanel;
    public TextMeshProUGUI difficultyText;
    public int difficultySelected = 1;
    public Color orange;


    void Awake()
    {
        Time.timeScale = 1f;
    }

    void Start()
    {
        DifficultySelection.instance.difficulty = DifficultySelection.Difficulties.normal;
        difficultyText.text = "Normal";
        difficultyText.color = Color.yellow;

        AudioSource[] audioSources = FindObjectsOfType<AudioSource>();
        for (int i = 0; i < audioSources.Length; i++)
        {
            audioSources[i].volume = SettingsManager.instance.volSetting;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
            RightArrow();
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            LeftArrow();

        if (Input.GetKeyDown(KeyCode.Backspace) || Input.GetKeyDown(KeyCode.Tab))
        {
            Back();
        }
    }

    public void PlayGame()
    {
        //SceneManager.LoadScene("MainScene");
        SceneFader.instance.FadeTo("MainScene");
    }

    public void Instructions()
    {
        instructionsPanel.SetActive(true);
    }

    public void Settings()
    {
        settingsPanel.SetActive(true);
    }

    public void Back()
    {
        instructionsPanel.SetActive(false);
        settingsPanel.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void RightArrow()
    {
        if (difficultySelected < 3)
        {
            difficultySelected++;
        }
        else if (difficultySelected >= 3)
        {
            difficultySelected = 0;
        }

        switch(difficultySelected)
        {
            case 0:
                difficultyText.text = "Easy";
                difficultyText.color = Color.green;
                DifficultySelection.instance.difficulty = DifficultySelection.Difficulties.easy;
                break;
            case 1:
                difficultyText.text = "Normal";
                difficultyText.color = Color.yellow;
                DifficultySelection.instance.difficulty = DifficultySelection.Difficulties.normal;
                break;
            case 2:
                difficultyText.text = "Hard";
                difficultyText.color = orange;
                DifficultySelection.instance.difficulty = DifficultySelection.Difficulties.hard;
                break;
            case 3:
                difficultyText.text = "Insane";
                difficultyText.color = Color.red;
                DifficultySelection.instance.difficulty = DifficultySelection.Difficulties.insane;
                break;
        }
    }
    public void LeftArrow()
    {
        if (difficultySelected > 0)
        {
            difficultySelected--;
        }
        else if (difficultySelected <= 0)
        {
            difficultySelected = 3;
        }

        switch (difficultySelected)
        {
            case 0:
                difficultyText.text = "Easy";
                difficultyText.color = Color.green;
                DifficultySelection.instance.difficulty = DifficultySelection.Difficulties.easy;
                break;
            case 1:
                difficultyText.text = "Normal";
                difficultyText.color = Color.yellow;
                DifficultySelection.instance.difficulty = DifficultySelection.Difficulties.normal;
                break;
            case 2:
                difficultyText.text = "Hard";
                difficultyText.color = orange;
                DifficultySelection.instance.difficulty = DifficultySelection.Difficulties.hard;
                break;
            case 3:
                difficultyText.text = "Insane";
                difficultyText.color = Color.red;
                DifficultySelection.instance.difficulty = DifficultySelection.Difficulties.insane;
                break;
        }
    }
}
