using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CureProgress : MonoBehaviour
{
    public static CureProgress instance;

    public TextMeshProUGUI cureProgress;
    public Animator animator;

    float completionIncrement;
    float completionPercentage;
    float oneThirdComplete = 33f;
    float twoThirdsComplete = 66f;

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
        if (DifficultySelection.instance.easyMode || DifficultySelection.instance.normalMode)
        {
            completionIncrement = 10f; // If easy or normal difficulty
        }
        else if (DifficultySelection.instance.hardMode || DifficultySelection.instance.insaneMode)
        {
            completionIncrement = 5f; // If hard or insane difficulty
        }

        cureProgress.text = completionPercentage.ToString() + "%";
        cureProgress.color = Color.red;
    }

    public void IncreaseProgress()
    {
        animator.SetTrigger("increase");

        completionPercentage += completionIncrement;

        cureProgress.text = completionPercentage.ToString() + "%";
    }

    void Update()
    {
        if (completionPercentage < oneThirdComplete)
        {
            cureProgress.color = Color.red;
        }
        else if (completionPercentage < twoThirdsComplete && completionPercentage > oneThirdComplete)
        {
            cureProgress.color = Color.yellow;
        }
        else if (completionPercentage > twoThirdsComplete)
        {
            cureProgress.color = Color.green;
        }
    }
}
