using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CureIngredient : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Collider[] colliders = GetComponents<Collider>();
            foreach (Collider collider in colliders)
            {
                collider.enabled = false;
            }
            CureProgress.instance.IncreaseProgress();
            Destroy(gameObject);
        }
    }     
}
