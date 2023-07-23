using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public VolumeSettings volumeSettings;

    private AudioSource gameMusicAudioSource;

    public Slider MusicVolumeSlider;
    public Slider SFXVolumeSlider;

    private void Awake()
    {
        gameMusicAudioSource = GetComponent<AudioSource>();
        if (this.gameObject.tag == "UI")
        {
            MusicVolumeSlider.value = volumeSettings.musicVolumeLevel;
            SFXVolumeSlider.value = volumeSettings.soundFXVolumeLevel;
        }
        UpdateVolume();        
    }
    private void Update()
    {
        UpdateVolume();
    }



    public void UpdateVolume()
    {
        if (MusicVolumeSlider != null)
        {
            volumeSettings.musicVolumeLevel = MusicVolumeSlider.value;
        }

        if (SFXVolumeSlider != null)
        {
            volumeSettings.soundFXVolumeLevel = SFXVolumeSlider.value;
        }

        if (this.gameObject.tag == "Music")
        {
            gameMusicAudioSource.volume = volumeSettings.musicVolumeLevel;
        }

        if (this.gameObject.tag == "SFX")
        {
            gameMusicAudioSource.volume = volumeSettings.soundFXVolumeLevel;
        }
        
        
    }
}
