using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    #region fields
    public static UIManager instance;
    public Image BlackScreen;
    public bool FadeToBlack, FadeFromBlack;
    public Text HealthText;
    public Image HealthImage;
    public Text CoinText;
    public GameObject PauseScreen;
    public GameObject OptionsScreen;
    public GameObject Buttons;
    public GameObject PauseText;
    public AudioMixer AudioMixer;
    private float _fadeSpeed = 1f;
    #endregion


    public void Awake()
    {
        instance = this;
    }

    public void Start()
    {
        BlackScreen.enabled = true;
        PauseScreen.SetActive(false);
        GameManager.instance.UI.SetActive(true);
        FadeFromBlack = true;
    }

    void Update()
    {
        Fade();
    }

    private void Fade()
    {
        if (FadeToBlack)
        {
            BlackScreen.color = new Color(BlackScreen.color.r, BlackScreen.color.g, BlackScreen.color.b, Mathf.MoveTowards(BlackScreen.color.a, 1f, _fadeSpeed * Time.deltaTime));
            if (BlackScreen.color.a == 1f)
                FadeToBlack = false;
        }
        if (FadeFromBlack)
        {
            BlackScreen.color = new Color(BlackScreen.color.r, BlackScreen.color.g, BlackScreen.color.b, Mathf.MoveTowards(BlackScreen.color.a, 0f, _fadeSpeed * Time.deltaTime));
            if (BlackScreen.color.a == 0f)
                FadeFromBlack = false;
        }
    }

    public void Resume()
    {
        GameManager.instance.PauseUnpause();
    }

    public void OpenOptions()
    {
        OptionsScreen.SetActive(true);
        SwitchUI(false);
    }

    public void CloseOptions()
    {
        OptionsScreen.SetActive(false);
        SwitchUI(true);
    }

    public void LevelSelect()
    {
        SceneManager.LoadScene("Scenes/SelectLevel");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Scenes/MainMenu");
    }

    public void SetMusicLevel(float value)
    {
        AudioMixer.SetFloat("MusicValue", value);
    }

    public void SetSFXLevel(float value)
    {
        AudioMixer.SetFloat("SfxValue", value);
    }


    public void SwitchUI(bool value)
    {
        Buttons.SetActive(value);
        PauseText.SetActive(value);
    }

}