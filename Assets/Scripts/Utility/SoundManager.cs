using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public VolumeSettings volumeSettings;

    private AudioSource gameMusicAudioSource;

    public Slider VolumeSlider;

    private void Awake()
    {
        gameMusicAudioSource = GetComponent<AudioSource>();
        UpdateVolume();        
    }
    private void Update()
    {
        UpdateVolume();
    }



    public void UpdateVolume()
    {
        if (VolumeSlider != null)
        {
            volumeSettings.musicVolumeLevel = VolumeSlider.value;
        }
        
        gameMusicAudioSource.volume = volumeSettings.musicVolumeLevel;
    }
}
