using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct PoisonData
{
    [HideInInspector]
    public float damage;

    [HideInInspector] public float time;
}

public class PoisonBehavior : MonoBehaviour
{
    public static PoisonBehavior instance;
    public PoisonData poison;

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
    void Start()
    {
        SetDefaultDamage();

        poison.time = 0f;
	}

    void CountTime()
    {
        poison.time += 1f * Time.deltaTime;
    }
	
	// Update is called once per frame
	void Update ()
    {
        CountTime();
        HurtPlayer();
	}

    void HurtPlayer()
    {
        PlayerData.instance.TakeDamage(poison.damage);
    }

    void SetDefaultDamage()
    {
        if (DifficultySelection.instance.easyMode)
        {
            poison.damage = 0.5f; // If easy difficulty
        }
        else if (DifficultySelection.instance.normalMode || DifficultySelection.instance.hardMode)
        {
            poison.damage = 1f; // If normal or hard difficulty
        }
        else if (DifficultySelection.instance.insaneMode)
        {
            poison.damage = 2f; // If insane difficulty
        }
    }
}
