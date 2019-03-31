using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

public class MovementInputSystem : ComponentSystem
{
    struct Group
    {
        public MovementParameters input;
    }

    protected override void OnUpdate()
    {
        foreach (var entity in GetEntities<Group>())
        {
            entity.input.horizontalMovement = Input.GetAxisRaw("Horizontal");
            entity.input.verticalMovement = Input.GetAxisRaw("Vertical");            
        }
    }
}
