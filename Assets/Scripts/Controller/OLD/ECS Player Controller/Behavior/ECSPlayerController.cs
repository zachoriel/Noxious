/*
   This was an attempt at implementing Unity's new Entity Component
   System. It worked (as it did with the original PlayerStats),
   but I decided that for the sake of keeping the code base consistent
   and - hopefully - somewhat easily readable, I would revert to normal
   OOP methods. See: PlayerMove for the new player controller.
*/

//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using Unity.Entities;

//[UpdateBefore(typeof(UnityEngine.Experimental.PlayerLoop.FixedUpdate))] // Turns OnUpdate into FixedUpdate
//public class ECSPlayerController : ComponentSystem
//{
//    public struct Components
//    {
//        public MovementParameters movement;
//        public Transform playerTransform;
//        public PlayerData playerData;
//    }

//    protected override void OnUpdate()
//    {
//        foreach (var entity in GetEntities<Components>())
//        {
//            var deltaTime = Time.fixedDeltaTime;

//            // Vertical movement physics
//            #region Up & Down Movement
//            // Normal forward movement (normal speed)
//            if (entity.movement.verticalMovement > 0)
//            {
//                entity.playerTransform.localPosition += entity.playerTransform.forward * entity.movement.moveSpeed * deltaTime;
//                entity.playerData.running = false;
//            }
//            // Fast forward movement (sprinting) & player has stamina
//            if (entity.movement.verticalMovement > 0 && Input.GetKey(KeyCode.LeftShift) && entity.playerData.stamina > 0)
//            {
//                entity.playerTransform.localPosition += entity.playerTransform.forward * entity.movement.sprintSpeed * deltaTime;
//                entity.playerData.UseStamina();
//                entity.playerData.running = true;
//            }
//            // Player has no stamina
//            else if (entity.playerData.stamina <= 0 || entity.movement.verticalMovement == 0)
//            {
//                entity.playerData.running = false;
//            }

//            // Normal backwards movement (slowed)
//            if (entity.movement.verticalMovement < 0)
//            {
//                entity.playerTransform.localPosition -= entity.playerTransform.forward * entity.movement.backwardSpeed * deltaTime;
//                entity.playerData.running = false;
//            }
//            // Fast backwards movement (normal speed) & player has stamina
//            if (entity.movement.verticalMovement < 0 && Input.GetKey(KeyCode.LeftShift) && entity.playerData.stamina > 0)
//            {
//                entity.playerTransform.localPosition -= entity.playerTransform.forward * entity.movement.moveSpeed * deltaTime;
//                entity.playerData.UseStamina();
//                entity.playerData.running = true;
//            }
//            // Player has no stamina
//            else if (entity.playerData.stamina <= 0 || entity.movement.verticalMovement == 0)
//            {
//                entity.playerData.running = false;
//            }
//            #endregion

//            // Horizontal movement physics
//            #region Sideways Movement
//            // Normal right movement (normal speed)
//            if (entity.movement.horizontalMovement > 0)
//            {
//                entity.playerTransform.localPosition += entity.playerTransform.right * entity.movement.moveSpeed * deltaTime;
//            }

//            // Normal left movement (normal speed)
//            if (entity.movement.horizontalMovement < 0)
//            {
//                entity.playerTransform.localPosition -= entity.playerTransform.right * entity.movement.moveSpeed * deltaTime;
//            }
//            #endregion

//            // Enemy aggro range 
//            #region Enemy Sound Detection
//            if (entity.playerData.GetPlayerStealthProfile() == 0)
//            {
//                entity.playerData.detectionCollider.radius = entity.playerData.walkingDetectionRadius;
//            }
//            else if (entity.playerData.GetPlayerStealthProfile() == 1)
//            {
//                entity.playerData.detectionCollider.radius = entity.playerData.runningDetectionRadius;
//            }
//            #endregion
//        }
//    }
//}
