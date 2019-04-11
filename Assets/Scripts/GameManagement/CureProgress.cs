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
        switch (DifficultySelection.instance.difficulty)
        {
            case DifficultySelection.Difficulties.easy:
                completionIncrement = 10f;
                break;
            case DifficultySelection.Difficulties.normal:
                completionIncrement = 6.66666667f;
                break;
            case DifficultySelection.Difficulties.hard:
                completionIncrement = 5f;
                break;
            case DifficultySelection.Difficulties.insane:
                completionIncrement = 4f;
                break;
        }

        completionPercentage = 0f;
        cureProgress.text = string.Format("{0:0}%", completionPercentage);
        cureProgress.color = Color.red;
    }

    public void IncreaseProgress()
    {
        animator.SetTrigger("increase");

        completionPercentage += completionIncrement;

        cureProgress.text = string.Format("{0:0}%", completionPercentage);

        if (completionPercentage >= 99f)
        {
            SetupWin();
        }
    }

    void SetupWin()
    {
        // Find and destroy all gas masks so they don't mess with the poison damage
        GameObject[] masks = GameObject.FindGameObjectsWithTag("GasMask"); 
        foreach (GameObject mask in masks)
        {
            Destroy(mask);
        }

        // Make player immune to the poison
        PoisonBehavior.instance.damage = 0f;

        PortalBehaviour portal = GameObject.FindObjectOfType<PortalBehaviour>();
        portal.readyToFinish = true;
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
