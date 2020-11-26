﻿using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelEnd : MonoBehaviour
{
    #region fields
    public static LevelEnd instance;
    public GameObject ScoreScreen;
    public GameObject RequiredScreen;
    public bool IsGameEnd;
    public Text RequiredText;
    public Text ScoreText;
    public int RequiredCoins = 5;
    private bool _required;
    private float _timer;
    #endregion
    private void Awake()
    {
        instance = this;
    }
    void Update()
    {
        if(!IsGameEnd)
            _timer += Time.deltaTime;
        if(_required)
            Required();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && SceneManager.GetActiveScene().name != "LevelBoss")
        {
            if(GameManager.instance.CurrentCoins == RequiredCoins)
                EndLevel(_timer);
            else
                Required();
        }
    }

    public void EndLevel(float timer)
    {
        Time.timeScale = 0f;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        if(SceneManager.GetActiveScene().name != "LevelBoss")
            GameManager.instance.UI.SetActive(false);
        IsGameEnd = true;
        if(SceneManager.GetActiveScene().name != "LevelBoss")
            RequiredScreen.SetActive(false);
        ScoreScreen.SetActive(true);
        if(SceneManager.GetActiveScene().name != "LevelBoss")
            ScoreText.text = "Coins: " + GameManager.instance.CurrentCoins + "\n " + "Time: " + timer + " seconds";
        else
        {
            ScoreText.text = "Congratulations! \nTime: " + (int)(_timer % 60) + " seconds";
        }
    }

    private void Required()
    {
        _required = true;
        RequiredScreen.SetActive(true);
        RequiredText.text = "You need " + (RequiredCoins - GameManager.instance.CurrentCoins) + " more coins";
    }

    public void EndLevelButton()
    {
        SceneManager.LoadScene("Scenes/MainMenu");
    }

    public void PlayAgainButton()
    {
        SceneManager.LoadScene("Scenes/" + SceneManager.GetActiveScene().name);

    }
}
