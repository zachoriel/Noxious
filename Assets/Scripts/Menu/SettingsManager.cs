using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    public static SettingsManager instance;

    public GameObject settingsPanel;
    public float mouseSens = 100f;
    public float volSetting = 0.5f;

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
        DontDestroyOnLoad(settingsPanel);
        DontDestroyOnLoad(this.gameObject);
    }

    public void ChangeSensitivity()
    {
        Slider sens = GameObject.FindGameObjectWithTag("Sensitivity Slider").GetComponent<Slider>();
        mouseSens = sens.value;

        CameraMove cam = FindObjectOfType<CameraMove>();
        cam.mouseSensitivity = mouseSens;
    }

    public void ChangeVolume()
    {
        Slider vol = GameObject.FindGameObjectWithTag("Volume Slider").GetComponent<Slider>();
        volSetting = vol.value;

        AudioSource[] audioSources = FindObjectsOfType<AudioSource>();
        for (int i = 0; i < audioSources.Length; i++)
        {
            audioSources[i].volume = volSetting;
        }
    }
}
