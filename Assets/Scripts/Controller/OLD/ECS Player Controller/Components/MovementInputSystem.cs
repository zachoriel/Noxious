/*
   This was an attempt at implementing Unity's new Entity Component
   System. It worked (as it did with the original PlayerStats),
   but I decided that for the sake of keeping the code base consistent
   and - hopefully - somewhat easily readable, I would revert to normal
   OOP methods. See: CameraMove for new camera controls system.
*/

//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using Unity.Entities;

//public class MovementInputSystem : ComponentSystem
//{
//    struct Group
//    {
//        public MovementParameters input;
//    }

//    protected override void OnUpdate()
//    {
//        foreach (var entity in GetEntities<Group>())
//        {
//            entity.input.horizontalMovement = Input.GetAxisRaw("Horizontal");
//            entity.input.verticalMovement = Input.GetAxisRaw("Vertical");            
//        }
//    }
//}
