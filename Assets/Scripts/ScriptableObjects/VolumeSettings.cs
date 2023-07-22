using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "VolumeSettings", menuName = "Audio/Volume Settings")]
public class VolumeSettings : ScriptableObject
{
    [Range(0f, 1f)]
    public float musicVolumeLevel = 1.0f;
    [Range(0f, 1f)]
    public float soundFXVolumeLevel = 1.0f;
}
