using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;

    [SerializeField] private AudioMixer masterMixer;
    [SerializeField] private AudioMixer vfxMixer;
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider vfxSlider;

    private float masterValue;
    private float vfxValue;
    // Start is called before the first frame update
    void Awake()
    {
        masterMixer.GetFloat("masterVolume",out masterValue);
        masterSlider.value = masterValue;

        vfxMixer.GetFloat("vfxVolume", out vfxValue);
        vfxSlider.value = vfxValue;
    }
    private void Update()
    {
        if (PlayerHealth.instance.isDie)
        {
            audioSource.Stop();
        }
    }
    public void SetMasterVolume()
    {
        masterMixer.SetFloat("masterVolume",masterSlider.value);
    }
    public void SetVFXVolume()
    {
        vfxMixer.SetFloat("vfxVolume", vfxSlider.value);
    }
}
