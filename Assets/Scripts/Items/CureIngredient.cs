using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CureIngredient : MonoBehaviour
{
    public AudioSource audioSource;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            audioSource.Play();

            DisableIngredient();

            CureProgress.instance.IncreaseProgress();

            Destroy(gameObject, 1f);
        }
    }

    void DisableIngredient()
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
