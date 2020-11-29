using UnityEngine;
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
    #endregion
    private void Awake()
    {
        instance = this;
    }
    void Update()
    {
        if(_required && GameManager.instance.CurrentCoins < RequiredCoins)
            Required();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && SceneManager.GetActiveScene().name != "LevelBoss")
        {
            if(GameManager.instance.CurrentCoins >= RequiredCoins)
                EndLevel();
            else
                Required();
        }
    }

    public void EndLevel()
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
            ScoreText.text = "Coins: " + GameManager.instance.CurrentCoins + "\n " + "Time: " + Mathf.RoundToInt(Time.timeSinceLevelLoad) + " seconds";
        else
        {
            ScoreText.text = "Congratulations! \nTime: " + Mathf.RoundToInt(Time.timeSinceLevelLoad) + " seconds";
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
