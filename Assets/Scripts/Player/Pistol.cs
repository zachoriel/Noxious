using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : MonoBehaviour
{
    [Header("Components & Dependencies")]
    public AudioClip gunShot;
    public LayerMask enemyLayer;
    public Transform player;
    public Transform firePos;
    Animator animator;
    AudioSource audioSource;
    MeshRenderer mesh;

    [Header("Gun Settings")]
    [SerializeField] float firingDistance = 200f;
    [SerializeField] float bulletDamage = 25f;
    [SerializeField] float soundIntensity = 5f;

    [Header("Effects")]
    public ParticleSystem muzzleFlash;
    public GameObject hitEffect;

    public bool readyToFire;
    bool firing;

	// Use this for initialization
	void Start ()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        //mesh = GetComponent<MeshRenderer>();

        if (DifficultySelection.instance.difficulty == DifficultySelection.Difficulties.easy || 
            DifficultySelection.instance.difficulty == DifficultySelection.Difficulties.normal)
        {
            this.gameObject.SetActive(false);
            //mesh.enabled = false;
            //this.enabled = false;
        }

        readyToFire = true;
	}
	
	// Update is called once per frame
	void Update ()
    {
        GunInput(); 
	}

    void GunInput()
    {
        firing = Input.GetMouseButtonDown(0);

        if (firing && readyToFire)
        {
            Fire();
        }
    }

    void Fire()
    {
        readyToFire = false;
        animator.SetTrigger("fireWeapon");
        StartCoroutine(Cooldown());
        audioSource.PlayOneShot(gunShot);
        muzzleFlash.Play();

        // Returns an array of colliders within the sound's range
        Collider[] enemies = Physics.OverlapSphere(gameObject.transform.position, soundIntensity, enemyLayer);

        for (int i = 0; i < enemies.Length; i++)
        {
            // Gets the AI script on each enemy and makes them aware of the player
            enemies[i].GetComponent<ParasiteBehaviour>().MakeAwareOfPlayer();
        }

        // Shoots out a raycast which searches for enemies
        RaycastHit hit;
        if (Physics.Raycast(transform.position, player.forward, out hit, Mathf.Infinity))
        {
            ParasiteBehaviour parasiteBehaviour = hit.transform.gameObject.GetComponent<ParasiteBehaviour>();
            ParasiteHealth parasiteHealth = hit.transform.gameObject.GetComponent<ParasiteHealth>();
            Collider collider = hit.collider;

            if (parasiteBehaviour != null)
            {
                parasiteBehaviour.MakeAwareOfPlayer();
            }
            if (parasiteHealth != null)
            {
                parasiteHealth.TakeDamage(bulletDamage);
            }

            GameObject newHit = Instantiate(hitEffect, hit.point, Quaternion.identity);
            Destroy(newHit, 2f);
        }
    }

    IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(0.4f);
        readyToFire = true;
        yield return null;
    }
}
