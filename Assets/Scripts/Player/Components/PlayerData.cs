using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerData : MonoBehaviour
{
    public static PlayerData instance;

    #region Parameters And Components
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

    [HideInInspector] public float lowHealth;
    [HideInInspector] public int points;
    [HideInInspector] public bool running = false;
    [HideInInspector] public float walkingDetectionRadius = 7f;
    [HideInInspector] public float runningDetectionRadius = 15f;
    #endregion

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

        healthBar = GameObject.FindGameObjectWithTag("HealthBar").GetComponent<Image>();
        healthText = GameObject.FindGameObjectWithTag("HealthText").GetComponent<TextMeshProUGUI>();
        staminaBar = GameObject.FindGameObjectWithTag("StaminaBar").GetComponent<Image>();
        staminaText = GameObject.FindGameObjectWithTag("StaminaText").GetComponent<TextMeshProUGUI>();
        animator = GameObject.FindGameObjectWithTag("Progress").GetComponent<Animator>();
    }

    void Start()
    {
        Destroy(DifficultySelection.instance.gameObject, 5f);
    }

    #region Methods
    // Used for stealth mechanics, determines how "loud" the player is when moving
    public int GetPlayerStealthProfile()
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
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Parasite"))
        {
            other.GetComponent<ParasiteBehaviour>().MakeAwareOfPlayer();
        }
    }

    // Called whenever the player takes damage, passes in an 'amount' parameter for custom damage
    public void TakeDamage(float amount)
    {
        health -= amount * Time.deltaTime;
        HandleUI();
    }

    // Kills the player
    public void Die()
    {
        ResetDifficultySelection();

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // Temporary lose 
        SceneManager.LoadScene("MainMenu"); // Thoughts: coughing/dying sounds play, camera does some frantic looking around (animation), 
                                            // there's an bursting sound (Skyrim Vampire Lord), the camera zooms out, and spawns in a new
                                            // parasite, which is supposed to be the player. Lose UI (text & buttons) then fade in, prompting 
                                            // the player to either quit or return to the main menu.
    }

    void ResetDifficultySelection()
    {
        DifficultySelection.instance.easyMode = false;
        DifficultySelection.instance.normalMode = false;
        DifficultySelection.instance.hardMode = false;
        DifficultySelection.instance.insaneMode = false;
    }

    // Called when the player picks up an injection syringe. Adds health to the player
    public void AddHealth(float amount)
    {
        health += amount;
        HandleUI();
    }

    // Called in 'ECSPlayerController' whenever the player uses stamina (sprinting)
    public void UseStamina()
    {
        stamina -= 10f * Time.deltaTime;
        HandleUI();
    }

    // Called in 'PlayerStats', regenerates player's stamina after (regenWaitTime) seconds of not using stamina
    public IEnumerator RegenStamina(float regenWaitTime) // BUG: code is reiterating after regenWaitTime before stam is full, increasing regen rate
    {
        // While stamina isn't full
        while (stamina <= startStamina)
        {
            yield return new WaitForSeconds(regenWaitTime);
            stamina += 10f * Time.deltaTime;
            HandleUI();
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

    // Called in 'PlayerStats' in OnStartRunning() because it looks cleaner that way
    public void SetPlayerStats()
    {
        health = startHealth;
        stamina = startStamina;

        HandleUI();
    }
    #endregion
}
