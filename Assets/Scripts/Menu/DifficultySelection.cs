using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DifficultySelection : MonoBehaviour
{
    public static DifficultySelection instance;

    [Header("Difficulties")]
    public bool easyMode;
    public bool normalMode;
    public bool hardMode;
    public bool insaneMode;

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

    // Use this for initialization
    void Start ()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void StartEasyGame()
    {
        easyMode = true;
        SceneManager.LoadScene("MainScene");
    }
    public void StartNormalGame()
    {
        normalMode = true;
        SceneManager.LoadScene("MainScene");
    }
    public void StartHardGame()
    {
        hardMode = true;
        SceneManager.LoadScene("MainScene");
    }
    public void StartInsaneGame()
    {
        insaneMode = true;
        SceneManager.LoadScene("MainScene");
    }
}
