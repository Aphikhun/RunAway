using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXManager : MonoBehaviour
{
    public SoundVfx[] vfxs;

    public static VFXManager instance;
    void Awake()
    {
        instance = this;

        foreach(SoundVfx s in vfxs)
        {
            s.audioSource = gameObject.AddComponent<AudioSource>();
            s.audioSource.clip = s.audioClip;
        }
    }

    public void Play(string sound)
    {
        SoundVfx s = Array.Find(vfxs, item => item.name == sound);
        s.audioSource.Play();
    }
    public void Stop(string sound)
    {
        SoundVfx s = Array.Find(vfxs, item => item.name == sound);
        s.audioSource.Stop();
    }
}
