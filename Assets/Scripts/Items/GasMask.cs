using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasMask : MonoBehaviour
{
    bool update = false;
    float time = 0;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ReduceDamage();
            DisableMask();
            Destroy(gameObject, 22f);
        }
    }

    void Count()
    {
        time += 1 * Time.deltaTime;

        if (time >= 30)
        {
            IncreaseDamage();
        }
    }

    void ReduceDamage()
    {
        update = true;
        PoisonBehavior.instance.poison.damage = PoisonBehavior.instance.poison.damage / 2f;
    }
    void IncreaseDamage()
    {
        update = false;
        PoisonBehavior.instance.poison.damage = PoisonBehavior.instance.poison.damage * 2f;
    }

    void DisableMask()
    {
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Collider>().enabled = false;
        GetComponent<Rigidbody>().useGravity = false;
    }

    // Update is called once per frame
    void Update ()
    {
		if (!update)
        {
            return;
        }
        else
        {
            Count();
        }
	}
}
