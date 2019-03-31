using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonBehavior : MonoBehaviour
{
    public static PoisonBehavior instance;

    public float damage;

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
	}
	
	// Update is called once per frame
	void Update ()
    {
        HurtPlayer();
	}

    void HurtPlayer()
    {
        PlayerData.instance.TakeDamage(damage);
    }

    void SetDefaultDamage()
    {
        switch (DifficultySelection.instance.difficulty)
        {
            case DifficultySelection.Difficulties.easy:
                damage = 0.5f;
                break;
            case DifficultySelection.Difficulties.normal:
                damage = 1f;
                break;
            case DifficultySelection.Difficulties.hard:
                damage = 1f;
                break;
            case DifficultySelection.Difficulties.insane:
                damage = 2f;
                break;
        }
    }
}
