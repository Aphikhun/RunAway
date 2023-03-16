using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class SoundVfx
{
    public string name;

    public AudioClip audioClip;
    public AudioMixerGroup audioMixer;

    [HideInInspector]
    public AudioSource audioSource;
}
