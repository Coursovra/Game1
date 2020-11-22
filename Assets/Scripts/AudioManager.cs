using UnityEngine;

public class AudioManager : MonoBehaviour
{
    #region fields
    public static AudioManager instance;
    public AudioSource[] Music;
    public AudioSource[] Sfx;
    public int LevelMusicToPlay;
    #endregion

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
