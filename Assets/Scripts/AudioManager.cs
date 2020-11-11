using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public AudioSource[] Music;
    public AudioSource[] Sfx;
    public int LevelMusicToPlay;

    // Start is called before the first frame update
    void Start()
    {
        PlayMusic(LevelMusicToPlay);
    }

    public void Awake()
    {
        instance = this;
    }

    public void PlayMusic(int musicToPlay)
    {
        Music[musicToPlay].Play();
    }

    public void PlaySFX(int sfxToPlay)
    {
        Sfx[sfxToPlay].Play();
    }
}
