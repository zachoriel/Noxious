using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CureIngredient : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CureProgress.instance.IncreaseProgress();
            Destroy(gameObject);
        }
    }     
}
