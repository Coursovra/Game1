using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    #region fields
    public GameObject MainMenuScreen;
    public GameObject OptionsScreen;
    public AudioMixer AudioMixer;
    #endregion

    private void Start()
    {
        AudioMixer.SetFloat("MusicValue", 1);
    }
    
    private void Awake()
    {
        Time.timeScale = 1.0f;
    }


    public void NewGame()
    {
        SceneManager.LoadScene("Scenes/SelectLevel");
    }

    public void OpenOptions()
    {
        OptionsScreen.SetActive(true);
        MainMenuScreen.SetActive(false);
    }

    public void CloseOptions()
    {
        OptionsScreen.SetActive(false);
        MainMenuScreen.SetActive(true);
    }

    public void Exit()
    {
        Application.Quit();
    }

    
    public void SetMusicLevel(float value)
    {
        AudioMixer.SetFloat("MusicValue", value);
    }

    public void SetSFXLevel(float value)
    {
        AudioMixer.SetFloat("SfxValue", value);
    }
}