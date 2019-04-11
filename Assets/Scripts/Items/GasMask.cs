using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasMask : MonoBehaviour
{
    public AudioSource audioSource;

    bool update = false;
    float time = 0f;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            audioSource.Play();
            DisableMask();
            ReduceDamage();
            Destroy(gameObject, 32f);
        }
    }

    void Count()
    {
        time += 1f * Time.deltaTime;

        if (time >= 30f)
        {
            IncreaseDamage();
        }
    }

    void ReduceDamage()
    {
        update = true;
        PoisonBehavior.instance.damage = PoisonBehavior.instance.damage / 2f;
    }
    void IncreaseDamage()
    {
        update = false;
        PoisonBehavior.instance.damage = PoisonBehavior.instance.damage * 2f;
        time = 0f;
    }

    void DisableMask()
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

    // Update is called once per frame
    void Update ()
    {
		if (update)
        {
            Count();
        }
	}
}
