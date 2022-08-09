using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SettingMenu : MonoBehaviour
{
    [SerializeField] AudioMixer audioMixer;
    
    UIManager uIManager;

    private void Start() {
        uIManager = FindObjectOfType<UIManager>();
    }

    public void OnVolumeChange(float value)
    {
        Debug.Log(value);
        audioMixer.SetFloat("mainVolume", value);
    }

    public void OnResolutionChange(float value)
    {
        // Debug.Log(value);
        QualitySettings.SetQualityLevel((int) value);
    }
}
