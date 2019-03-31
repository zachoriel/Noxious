using UnityEngine;
using Unity.Entities;

public class PlayerStats : ComponentSystem
{
    public struct Components
    {
        public PlayerData playerData;
    }

    // Same as Start()
    protected override void OnStartRunning()
    {
        foreach (var entity in GetEntities<Components>())
        {
            entity.playerData.SetPlayerStats();
            entity.playerData.lowHealth = entity.playerData.startHealth * 0.25f; // Sets the player's low health value to 25% of whatever their starting health is
        }
    }

    // Same as Update()
    protected override void OnUpdate()
    {
        foreach (var entity in GetEntities<Components>())
        {
            var player = entity.playerData; // For simplified typing

            // If player isn't sprinting and has used stamina, start regeneration after x seconds
            if (!player.running && player.stamina < player.startStamina)
            {
                player.StartCoroutine(player.RegenStamina(5f));
            }
            // Otherwise, stop regeneration
            else
            {
                player.StopAllCoroutines();
            }

            // If player is low on health, start animating healthbar flash
            if (player.health <= player.lowHealth)
            {
                player.animator.SetBool("lowHealth", true);
            }
            // Otherwise.... *dramatic pause*.... don't
            else
            {
                player.animator.SetBool("lowHealth", false);
            }

            // If the player is out of health
            if (player.health <= 0f)
            {
                // If god mode isn't enabled
                if (!player.godMode)
                {
                    // Calls the PlayerData Die() method
                    player.Die();
                }
            }
        }
    }    
}
