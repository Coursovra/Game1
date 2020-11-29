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
        PlayMusic();
    }

    public void Awake()
    {
        instance = this;
    }

    public void PlayMusic()
    {
        Music[LevelMusicToPlay].Play();
    }

    public void PlaySFX(int sfxToPlay)
    {
        Sfx[sfxToPlay].Play();
    }
}
