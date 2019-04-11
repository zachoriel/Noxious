using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("Configurations & Setup")]
    public KeyCode[] pauseButtons;

    [Header("UI Elements")]
    public Canvas pauseCanvas;
    [Space]
    public Image[] images;
    [Space]
    public TextMeshProUGUI[] texts;

    [Header("Colours")]
    public Color pauseTextStartColor;
    public Color pauseTextEndColor;
    public Color resumeButtonStartColor;
    public Color resumeButtonEndColor;
    public Color settingsButtonStartColor;
    public Color settingsButtonEndColor;
    public Color quitButtonStartColor;
    public Color quitButtonEndColor;
    public Color buttonTextStartColor;
    public Color buttonTextEndColor;

    [Header("Fade Settings")]
    public float fadeInTime = 1f;
    public float fadeOutTime = 2f;

    Vector3 zero = Vector3.zero;
    Vector3 one = Vector3.one;
    Vector3 fullSize = new Vector3(1.5f, 1.5f, 1.5f);
    bool input, paused;


    void Awake()
    {
        AudioSource[] audioSources = FindObjectsOfType<AudioSource>();
        for (int i = 0; i < audioSources.Length; i++)
        {
            audioSources[i].volume = SettingsManager.instance.volSetting;
        }
    }

    void Start()
    {
        if (pauseButtons[0] == KeyCode.None)
            pauseButtons[0] = KeyCode.Escape;
        if (pauseButtons[1] == KeyCode.None)
            pauseButtons[1] = KeyCode.P;

        paused = false;
    }

    void Update()
    {
        input = Input.GetKeyDown(pauseButtons[0]) || Input.GetKeyDown(pauseButtons[1]);

        if (input && !paused)
        {
            StopCoroutine(FadeOut(images, texts));
            StartCoroutine(FadeIn(images, texts));
        }

        if (Input.GetKeyDown(KeyCode.Backspace) || Input.GetKeyDown(KeyCode.Tab))
        {
            Back();
        }
    }

    IEnumerator FadeIn(Image[] images, TextMeshProUGUI[] texts)
    {
        paused = true;
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        for (float time = 0.01f; time < fadeInTime; time += 0.1f)
        {
            images[0].color = Color.Lerp(resumeButtonStartColor, resumeButtonEndColor, time / fadeInTime);
            images[1].color = Color.Lerp(settingsButtonStartColor, settingsButtonEndColor, time / fadeInTime);
            images[2].color = Color.Lerp(quitButtonStartColor, quitButtonEndColor, time / fadeInTime);
            for (int i = 0; i < images.Length; i++)
            {
                images[i].gameObject.transform.localScale = Vector3.Lerp(zero, fullSize, time / fadeInTime);
            }

            texts[0].color = Color.Lerp(pauseTextStartColor, pauseTextEndColor, time / fadeInTime);
            texts[0].gameObject.transform.localScale = Vector3.Lerp(zero, one, time / fadeInTime);
            for (int j = 1; j < texts.Length; j++)
            {
                texts[j].color = Color.Lerp(buttonTextStartColor, buttonTextEndColor, time / fadeInTime);
                texts[j].gameObject.transform.localScale = Vector3.Lerp(zero, one, time / fadeInTime);
            }

            yield return null;
        }

        StopCoroutine(FadeIn(images, texts));
    }

    public IEnumerator FadeOut(Image[] images, TextMeshProUGUI[] texts)
    {
        paused = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        for (float time = 0.01f; time < fadeOutTime; time += 0.1f)
        {
            images[0].color = Color.Lerp(resumeButtonEndColor, resumeButtonStartColor, time / fadeOutTime);
            images[1].color = Color.Lerp(settingsButtonEndColor, settingsButtonStartColor, time / fadeOutTime);
            images[2].color = Color.Lerp(quitButtonEndColor, quitButtonStartColor, time / fadeOutTime);
            for (int i = 0; i < images.Length; i++)
            {
                images[i].gameObject.transform.localScale = Vector3.Lerp(fullSize, zero, time / fadeOutTime);
            }

            texts[0].color = Color.Lerp(pauseTextEndColor, pauseTextStartColor, time / fadeOutTime);
            texts[0].gameObject.transform.localScale = Vector3.Lerp(one, zero, time / fadeOutTime);
            for (int j = 1; j < texts.Length; j++)
            {
                texts[j].color = Color.Lerp(buttonTextEndColor, buttonTextStartColor, time / fadeOutTime);
                texts[j].gameObject.transform.localScale = Vector3.Lerp(one, zero, time / fadeOutTime);
            }

            yield return null;
        }

        Time.timeScale = 1f;

        StopCoroutine(FadeOut(images, texts));
    }

    public void ResumeGame()
    {
        StopCoroutine(FadeIn(images, texts));
        StartCoroutine(FadeOut(images, texts));
    }

    public void SettingsMenu()
    {
        pauseCanvas.sortingOrder = 0;
        SettingsManager.instance.settingsPanel.SetActive(true);
    }

    public void Back()
    {
        pauseCanvas.sortingOrder = 1;
        SettingsManager.instance.settingsPanel.SetActive(false);
    }

    public void QuitGame()
    {
        StopCoroutine(FadeIn(images, texts));
        StartCoroutine(FadeOut(images, texts));
        SceneFader.instance.FadeTo("MainMenu");
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
