using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public Image blackScreen;
    private float _fadeSpeed = 1f;
    public bool FadeToBlack, FadeFromBlack;
    public Text HealthText;
    public Image HealthImage;
    public Text coinText;
    public GameObject PauseScreen;
    public GameObject OptionsScreen;
    public GameObject Buttons;
    public GameObject PauseText;
    public AudioMixer AudioMixer;



    public void Awake()
    {
        instance = this;
        blackScreen.enabled = true;
    }

    void Update()
    {
        if (FadeToBlack)
        {
            blackScreen.color = new Color(blackScreen.color.r, blackScreen.color.g, blackScreen.color.b, Mathf.MoveTowards(blackScreen.color.a, 1f, _fadeSpeed * Time.deltaTime));
            if (blackScreen.color.a == 1f)
            {
                FadeToBlack = false;
            }
        }
        if (FadeFromBlack)
        {            
            blackScreen.color = new Color(blackScreen.color.r, blackScreen.color.g, blackScreen.color.b, Mathf.MoveTowards(blackScreen.color.a, 0f, _fadeSpeed * Time.deltaTime));
            if (blackScreen.color.a == 0f)
            {
                FadeFromBlack = false;
            }
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

    }

    public void MainMenu()
    {

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