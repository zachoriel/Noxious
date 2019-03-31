using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

public class CameraLook : ComponentSystem
{	
    public struct Components
    {
        public CameraData camera;
        public Transform transform;
    }

    protected override void OnStartRunning()
    {
        foreach (var entity in GetEntities<Components>())
        {
            entity.camera.SetCursor();
        }
    }

    protected override void OnUpdate()
    {
        foreach (var entity in GetEntities<Components>())
        {
            var cam = entity.camera; // For simplified typing
            var deltaTime = Time.smoothDeltaTime;

            if (cam.axes == CameraData.RotationAxis.MouseX)
            {
                cam.transform.Rotate(0, Input.GetAxis("Mouse X") * cam.sensHorizontal * deltaTime, 0);
            }
            else if (cam.axes == CameraData.RotationAxis.MouseY)
            {
                cam._rotationX -= Input.GetAxis("Mouse Y") * cam.sensVertical * deltaTime;
                cam._rotationX = Mathf.Clamp(cam._rotationX, cam.minMaxVert.x, cam.minMaxVert.y);

                float rotationY = cam.transform.localEulerAngles.y;

                cam.transform.localEulerAngles = new Vector3(cam._rotationX, rotationY, 0);
            }
        }
    }
}
