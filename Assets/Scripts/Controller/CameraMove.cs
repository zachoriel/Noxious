using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public float mouseSensitivity;
    string mouseXInput = "Mouse X";
    string mouseYInput = "Mouse Y";
    [SerializeField] bool inMenu;

    [SerializeField] Transform player;

    float xAxisClamp;

	void Awake ()
    {
        if (!inMenu)
        {
            LockCursor();
        }
        xAxisClamp = 0f;       
	}

    void Start()
    {
        mouseSensitivity = SettingsManager.instance.mouseSens;
    }

    void LockCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
	
	void Update ()
    {
        CameraRotation();
	}

    void CameraRotation()
    {
        float mouseX = Input.GetAxisRaw(mouseXInput) * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxisRaw(mouseYInput) * mouseSensitivity * Time.deltaTime;

        xAxisClamp += mouseY;

        if (xAxisClamp > 90f)
        {
            xAxisClamp = 90f;
            mouseY = 0f;
            ClampXAxisRotationToValue(270f);
        }
        else if (xAxisClamp < -90f)
        {
            xAxisClamp = -90f;
            mouseY = 0f;
            ClampXAxisRotationToValue(90f);
        }

        transform.Rotate(Vector3.left * mouseY);
        player.Rotate(Vector3.up * mouseX);
    }

    void ClampXAxisRotationToValue(float value)
    {
        Vector3 eulerRotation = transform.eulerAngles;
        eulerRotation.x = value;
        transform.eulerAngles = eulerRotation;
    }
}
