using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : MonoBehaviour
{
    public AudioSource audio;
    public AudioClip gunShot;
    public LayerMask enemyLayer;
    MeshRenderer mesh;
    DifficultySelection difficulty;

    [SerializeField] float soundIntensity = 5f;
    bool firing;

	// Use this for initialization
	void Start ()
    {
		if (audio == null)
        {
            audio = GetComponent<AudioSource>();
        }

        mesh = GetComponent<MeshRenderer>();
        difficulty = FindObjectOfType<DifficultySelection>();

        if (difficulty.easyMode || difficulty.normalMode)
        {
            mesh.enabled = false;
            this.enabled = false;
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        firing = Input.GetMouseButtonDown(0);

        if (firing)
        {
            Fire();
        }
	}

    void Fire()
    {
        audio.PlayOneShot(gunShot);

        // Returns an array of colliders within the sound's range
        Collider[] enemies = Physics.OverlapSphere(gameObject.transform.position, soundIntensity, enemyLayer);

        // Applies the enemies within the array to a for loop
        for (int i = 0; i < enemies.Length; i++)
        {
            // Gets the AI script on each enemy and makes them aware of the player
            enemies[i].GetComponent<ParasiteBehaviour>().MakeAwareOfPlayer();
        }
    }
}
