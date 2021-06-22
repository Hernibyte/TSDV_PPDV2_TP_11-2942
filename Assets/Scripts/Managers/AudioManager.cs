﻿using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    private static AudioManager instance;

    public static AudioManager Get()
    {
        return instance;
    }

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public Sound[] sounds;
    void Start()
    {
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.picth;
            s.source.loop = s.loop;
        }
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        if (s != null)
        {
            //if (!s.source.isPlaying)
                s.source.Play();
        }
    }
    public void Pause(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        if (s != null)
        {
            if (s.source.isPlaying)
                s.source.Pause();
        }
    }
    public void Resume(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        if (s != null)
        {
            if (s.source.isPlaying)
                s.source.Play();
        }
    }
    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        if (s != null)
        {
            if (s.source.isPlaying)
                s.source.Stop();
        }
    }

    public void StopAllSFX()
    {
        foreach (Sound s in sounds)
        {
            Stop(s.name);
        }
    }
}