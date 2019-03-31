using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    public float rotationSpeed = 5f;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        RotateSun();
	}

    void RotateSun()
    {
        transform.Rotate(rotationSpeed * Time.deltaTime, 0f, 0f, Space.World);
    }
}
