using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelEnd : MonoBehaviour
{
    public static LevelEnd instance;
    public GameObject ScoreScreen;
    public GameObject RequiredScreen;
    public bool IsGameEnd;
    public Text RequiredText;
    public Text ScoreText;
    private int _requiredCoins = 5;
    private bool _required = false;

    private float _timer;

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
        if (other.CompareTag("Player") )
        {
            if(GameManager.instance.CurrentCoins == _requiredCoins)
                EndLevel();
            else
                Required();
        }
    }

    private void EndLevel()
    {
        UIManager.instance.CloseOptions();
        Time.timeScale = 0f;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        GameManager.instance.UI.SetActive(false);
        IsGameEnd = true;
        RequiredScreen.SetActive(false);
        ScoreScreen.SetActive(true);
        ScoreText.text = "Coins: " + GameManager.instance.CurrentCoins + "\n " + "Time: " + _timer + " seconds";
        
    }

    private void Required()
    {
        _required = true;
        RequiredScreen.SetActive(true);
        RequiredText.text = "You need " + (_requiredCoins - GameManager.instance.CurrentCoins) + " more coins";
    }

    public void EndLevelButton()
    {
        SceneManager.LoadScene("Scenes/MainMenu");
    }

    public void PlayAgainButton()
    {
        SceneManager.LoadScene("Scenes/Level02");
    }
}
