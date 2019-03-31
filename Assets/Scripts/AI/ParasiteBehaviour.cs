using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ParasiteBehaviour : MonoBehaviour
{
    NavMeshAgent agent;

    [Header("AI Settings")]
    [SerializeField] float fieldOfView = 120f;
    [SerializeField] float viewDistance = 50f;
    [SerializeField] float detectionRadius = 30f;
    [SerializeField] float chasingViewDistance = 80f;
    [SerializeField] float wanderRadius = 15f;
    [SerializeField] float wanderSpeed = 1f;
    [SerializeField] float chaseSpeed = 5f;

    bool awareOfPlayer = false;
    Vector3 wanderPoint;
    float insaneModeMultiplier = 10;


	// Use this for initialization
	void Start ()
    {
        agent = GetComponent<NavMeshAgent>();

        #region Difficulty Setting
        if (DifficultySelection.instance.hardMode)
        {
            HardModeSettings();
        }
        else if (DifficultySelection.instance.insaneMode)
        {
            InsaneModeSettings();
        }
        #endregion
    }

    void EnemyBehavior()
    {
        // If the AI can see the player...
        if (awareOfPlayer)
        {
            // Chase the player!
            Chase();
        }
        // If the AI cannot see the player...
        else
        {
            // Search for the player and wander around! 
            ScanForPlayer();
            Wander();
        }
    }

    #region Wandering AI
    // Checks to see if the player is in the AI's line of sight
    void ScanForPlayer()
    {
        // If the player is in the AI's line of sight...
        if (Vector3.Angle(Vector3.forward, transform.InverseTransformPoint(PlayerData.instance.transform.position)) < fieldOfView / 2f)
        {
            // If the player is within the AI's view distance...
            if (Vector3.Distance(PlayerData.instance.transform.position, transform.position) < viewDistance)
            {
                // Scan all colliders within a radius...
                Collider[] colliders = Physics.OverlapSphere(transform.position, detectionRadius, -1);
                foreach (var item in colliders)
                {
                    // If the OverlapSphere hits the player...
                    if (item.transform.CompareTag("Player"))
                    {
                        // The AI can see the player! *whew*
                        MakeAwareOfPlayer();
                    }
                }
            }
        }
    }

    // Makes the AI wander in random directions
    void Wander()
    {
        agent.speed = wanderSpeed;

        // If the AI has reached it's wander point...
        if (Vector3.Distance(transform.position, wanderPoint) < 2f)
        {
            // Generate a new wander point!
            wanderPoint = RandomWanderPoint();
        }
        // If the AI has not reached it's wander point...
        else
        {
            // Move towards it!
            agent.SetDestination(wanderPoint);
        }
    }

    // Generates random points for the AI to wander towards
    Vector3 RandomWanderPoint()
    {
        Vector3 randomPoint = (Random.insideUnitSphere * wanderRadius) + transform.position;
        NavMeshHit navHit;
        NavMesh.SamplePosition(randomPoint, out navHit, wanderRadius, -1);
        return new Vector3(navHit.position.x, navHit.position.y, navHit.position.z);
    }
    #endregion

    #region Attacking AI
    // Makes the AI chase the player
    void Chase()
    {
        agent.speed = chaseSpeed;
        agent.SetDestination(PlayerData.instance.transform.position);

        // If the player gets far enough away from the AI...
        if (Vector3.Distance(PlayerData.instance.transform.position, transform.position) > chasingViewDistance)
        {
            // The AI no longer knows where the player is and resumes wandering
            MakeUnawareOfPlayer();
        }
    }
    #endregion

    #region Detection Functions
    // Makes the AI aware of the player (who'd've thunk it?)
    public void MakeAwareOfPlayer()
    {
        awareOfPlayer = true;
    }

    // Makes the AI unaware of the player (see above)
    void MakeUnawareOfPlayer()
    {
        awareOfPlayer = false;
    }
    #endregion
	
	void FixedUpdate ()
    {
        EnemyBehavior();
    }

    #region Settings
    // Sets values for hard mode AI up in Start
    void HardModeSettings()
    {        
        awareOfPlayer = false; // AI cannot see player from the start of game
        wanderPoint = RandomWanderPoint(); // Sets a random wander point for the AI
    }

    // Sets values for insane mode AI up in Start
    void InsaneModeSettings()
    {       
        awareOfPlayer = true; // All AI can see the player from the start of game        
        wanderPoint = RandomWanderPoint(); // Sets a random wander point for the AI

        // Sets AI's detection parameters super high so that they basically always detect the player
        fieldOfView = fieldOfView = 360f;
        viewDistance = viewDistance * insaneModeMultiplier;
        detectionRadius = detectionRadius * insaneModeMultiplier;
        chasingViewDistance = chasingViewDistance * insaneModeMultiplier;
    }
    #endregion
}
