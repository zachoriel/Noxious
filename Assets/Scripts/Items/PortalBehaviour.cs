using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalBehaviour : MonoBehaviour
{
    public Animator animator;

    BoxCollider collider;

    [HideInInspector] public bool readyToFinish = false;

	// Use this for initialization
	void Start ()
    {
        collider = GetComponent<BoxCollider>();
        animator = GameObject.FindGameObjectWithTag("NotReadyText").GetComponent<Animator>();
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (readyToFinish)
            {
                SceneManager.LoadScene("MainMenu");
            }
            else
            {
                animator.SetTrigger("Not Ready");
            }
        }
    }
}
