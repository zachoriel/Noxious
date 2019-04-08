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
    }

    // Use this for initialization
    void Start ()
    {
        DontDestroyOnLoad(gameObject);
    }
}
