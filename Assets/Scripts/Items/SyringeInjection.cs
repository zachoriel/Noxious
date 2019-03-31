using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyringeInjection : MonoBehaviour
{   
    float healthToAdd;

	// Use this for initialization
	void Start ()
    {
        healthToAdd = PlayerData.instance.startHealth * 0.2f;
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            AddHealth();
            Destroy(gameObject);
        }
    }

    void AddHealth()
    {
        PlayerData.instance.AddHealth(healthToAdd);
    }
}
