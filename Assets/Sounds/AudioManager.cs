using UnityEngine;
using System;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager backgroundMusic = null;
    public Sound[] sounds;

    public static AudioManager Instance
    {
        get { return backgroundMusic; }
    }

    void Awake()
    {
        if (backgroundMusic != null && backgroundMusic != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            backgroundMusic = this;
        }
        DontDestroyOnLoad(backgroundMusic);

        foreach (Sound sound in sounds)
        {
            sound.src = gameObject.AddComponent<AudioSource>();
            sound.src.clip = sound.clip;

            sound.src.volume = sound.volume;
            sound.src.pitch = sound.pitch;
        }
    }

    public void playSound(string id)
    {
        Sound s = Array.Find(sounds, sound => sound.name == id);
        s.src.Play();
    }
}