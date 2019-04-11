using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyringeInjection : MonoBehaviour
{
    public AudioSource audioSource;

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
            audioSource.Play();

            DisablePotion();

            AddHealth();

            Destroy(gameObject, 1f);
        }
    }

    void AddHealth()
    {
        PlayerData.instance.AddHealth(healthToAdd);
    }

    void DisablePotion()
    {
        Collider[] colliders = GetComponents<Collider>();
        foreach (Collider collider in colliders)
        {
            collider.isTrigger = false;
            collider.enabled = false;
        }
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Rigidbody>().useGravity = false;
    }
}
