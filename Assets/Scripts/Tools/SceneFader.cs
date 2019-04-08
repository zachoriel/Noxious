using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneFader : MonoBehaviour
{
    public static SceneFader instance;

    [Header("Scene Transition Setup")]
    public Image img;
    public AnimationCurve curve;

    void Awake()
    {
        #region Singleton
        if (instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
        #endregion
    }

    // Plays the FadeIn animation at start. Used for the backend of transitioning from main menu to level
    void Start()
    {
        StartCoroutine(FadeIn());
    }

    // Call this method as part of the play button (or whatever you're using to transition)
    public void FadeTo(string scene)
    {
        StartCoroutine(FadeOut(scene));
    }

    // Animates the black image's alpha down to 0
    IEnumerator FadeIn()
    {
        float t = 1f;

        while (t > 0)
        {
            t -= Time.deltaTime;
            float a = curve.Evaluate(t);
            img.color = new Color(0f, 0f, 0f, a);   // rgba 
            yield return 0;        // Skip to the next frame
        }
    }

    // Animates the black image's alpha up to 255
    IEnumerator FadeOut(string scene)
    {
        float t = 0f;

        while (t < 1)
        {
            t += Time.deltaTime;
            float a = curve.Evaluate(t);
            img.color = new Color(0f, 0f, 0f, a);   // rgba 
            yield return 0;        // Skip to the next frame
        }

        // Loads the given scene after animation has played
        SceneManager.LoadScene(scene);
    }
}