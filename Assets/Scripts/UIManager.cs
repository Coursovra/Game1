using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public Image blackScreen;
    private float _fadeSpeed = 1f;
    public bool FadeToBlack, FadeFromBlack;
    public Text HealthText;
    public Image HealthImage;
    public Text coinText;
    public GameObject PauseScreen;
    public GameObject OptionsScreen;

    public void Awake()
    {
        instance = this;
        blackScreen.enabled = true;
    }
    // Update is called once per frame
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
    }

    public void CloseOptions()
    {
        OptionsScreen.SetActive(false);
    }

    public void LevelSelect()
    {

    }

    public void MainMenu()
    {

    }

    public void SetMusicLevel()
    {

    }

    public void SetSFXLevel()
    {

    }

}