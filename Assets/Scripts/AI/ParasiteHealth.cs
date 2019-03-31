using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ParasiteHealth : MonoBehaviour
{
    [Header("Health Stats")]
    [SerializeField] float startHealth = 100f;
    [SerializeField] float currentHealth;
    float minHealth = 0f;

    [Header("UI")]
    [SerializeField] Image healthBar;

    float destroyTimer = 5f;

	// Use this for initialization
	void Start ()
    {
        currentHealth = startHealth;
        HandleUI();
	}

    void HandleUI()
    {
        healthBar.fillAmount = currentHealth / startHealth;
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, minHealth, startHealth);
        HandleUI();

        if (currentHealth <= minHealth)
        {
            Die();
        }
    }

    // Disables the AI, plays death animation, destroys object
    void Die()
    {
        ParasiteBehaviour parasite = GetComponent<ParasiteBehaviour>();
        parasite.enemyStates = ParasiteBehaviour.EnemyStates.Dead;
        // TODO: Death animation
        Destroy(gameObject, destroyTimer);
    }
}
