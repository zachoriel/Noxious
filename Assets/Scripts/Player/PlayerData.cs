using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerData : MonoBehaviour, IHealth
{
    public static PlayerData instance;

    [Header("Health")]
    public float health;
    public float startHealth = 100f;
    public float minHealth = 0f;
    public Image healthBar;
    public TextMeshProUGUI healthText;
    public bool godMode = false;

    [Header("Stamina")]
    public float stamina;
    public float startStamina = 100f;
    public float minStamina = 0f;
    public Image staminaBar;
    public TextMeshProUGUI staminaText;

    [Header("Components")]
    public Animator animator;
    public SphereCollider detectionCollider;

    float lowHealth;
    int points; // I don't actually remember what this was supposed to be for?
    float walkingDetectionRadius = 7f;
    float runningDetectionRadius = 15f;
    [HideInInspector] public bool running = false;

    void Awake()
    {
        #region Singleton
        if (instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
        #endregion

        animator = GameObject.FindGameObjectWithTag("Progress").GetComponent<Animator>();
    }

    void Start()
    {
        health = startHealth;
        stamina = startStamina;
        lowHealth = startHealth * 0.25f;

        HandleUI();

        if (DifficultySelection.instance != null)
        {
            Destroy(DifficultySelection.instance.gameObject, 5f);
        }
    }

    // Updates the UI to match player's stats whenever a stat changes
    public void HandleUI()
    {
        #region Health UI
        healthBar.fillAmount = health / startHealth;
        healthText.text = Mathf.RoundToInt(health).ToString() + "%";
        health = Mathf.Clamp(health, minHealth, startHealth);
        #endregion

        #region Stamina UI
        staminaBar.fillAmount = stamina / startStamina;
        staminaText.text = Mathf.RoundToInt(stamina).ToString() + "%";
        stamina = Mathf.Clamp(stamina, minStamina, startStamina);
        #endregion
    }

    void Update()
    {
        //SoundDetection();
        UseStamina();
    }

    #region Stealth Mechanic
    // Application of player stealth profile (see next method)
    void SoundDetection()
    {
        if (GetPlayerStealthProfile() == 0)
        {
            detectionCollider.radius = walkingDetectionRadius;
        }
        else if (GetPlayerStealthProfile() == 1)
        {
            detectionCollider.radius = runningDetectionRadius;
        }
    }

    // Used for stealth mechanics, determines how "loud" the player is when moving
    int GetPlayerStealthProfile()
    {
        if (!running)
        {
            return 0;
        }
        else
        {
            return 1;
        }
    }

    // For aggroing enemies if within range
    //void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag("Parasite"))
    //    {
    //        other.GetComponent<ParasiteBehaviour>().MakeAwareOfPlayer();
    //    }
    //}
    #endregion

    #region Health Mechanics
    // Called whenever the player takes damage, passes in an 'amount' parameter for custom damage
    public void TakeDamage(float amount)
    {
        health -= amount * Time.deltaTime;
        HandleUI();

        // If player is low on health, start animating healthbar flash
        if (health <= lowHealth)
        {
            animator.SetBool("lowHealth", true);
        }
        // Otherwise.... *dramatic pause*.... don't
        else
        {
            animator.SetBool("lowHealth", false);
        }

        // If the player is out of health
        if (health <= 0f)
        {
            // If god mode isn't enabled
            if (!godMode)
            {
                // Calls the PlayerData Die() method
                Die();
            }
        }
    }

    // Called when the player picks up an injection syringe. Adds health to the player
    public void AddHealth(float amount)
    {
        health += amount;
        HandleUI();

        // If player is low on health, start animating healthbar flash
        if (health <= lowHealth)
        {
            animator.SetBool("lowHealth", true);
        }
        // Otherwise.... *dramatic pause*.... don't
        else
        {
            animator.SetBool("lowHealth", false);
        }
    }

    // Kills the player
    public void Die()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SceneFader.instance.FadeTo("LoseScene");
    }
    #endregion

    #region Stamina Mechanics
    void UseStamina()
    {
        // If the player is running
        if (running)
        {
            StopAllCoroutines();
            stamina -= 10f * Time.deltaTime;
            HandleUI();
        }
        // If player isn't running and has used stamina, start regeneration after x seconds
        else if (!running && stamina < startStamina)
        {
            StartCoroutine(RegenStamina(2.5f));
        }
        // If the player isn't running and has full stamina
        else if (!running && stamina >= startStamina)
        {
            StopAllCoroutines();
        }
    }

    // Regenerates player's stamina after (regenWaitTime) seconds of not using stamina
    public IEnumerator RegenStamina(float regenWaitTime)
    {
        yield return new WaitForSeconds(regenWaitTime);
        stamina += 10f * Time.deltaTime;
        HandleUI();
    }
    #endregion

    void ResetDifficultySelection()
    {
        DifficultySelection.instance.difficulty = DifficultySelection.Difficulties.normal;
    }
}
