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
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                SceneFader.instance.FadeTo("WinScene");
            }
            else
            {
                animator.SetTrigger("Not Ready");
            }
        }
    }
}
