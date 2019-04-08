using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DifficultySelection : MonoBehaviour
{
    public static DifficultySelection instance;

    public enum Difficulties
    {
        none,
        easy,
        normal,
        hard,
        insane
    };

    public Difficulties difficulty;

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

        difficulty = Difficulties.normal;
    }

    // Use this for initialization
    void Start ()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void StartEasyGame()
    {
        difficulty = Difficulties.easy;
        SceneManager.LoadScene("MainScene");
    }
    public void StartNormalGame()
    {
        difficulty = Difficulties.normal;
        SceneManager.LoadScene("MainScene");
    }
    public void StartHardGame()
    {
        difficulty = Difficulties.hard;
        SceneManager.LoadScene("MainScene");
    }
    public void StartInsaneGame()
    {
        difficulty = Difficulties.insane;
        SceneManager.LoadScene("MainScene");
    }
}
