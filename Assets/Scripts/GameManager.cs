using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region fields
    public static GameManager instance;
    public GameObject PlayerDeathEffect;
    public GameObject UI;
    public Transform PlayerTransform;
    public int CurrentCoins;
    private Vector3 _respawnPosition;
    #endregion
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        UIManager.instance.FadeFromBlack = true;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        _respawnPosition = PlayerController.instance.transform.position;
        AddCoins(0);
        if (Time.timeScale == 0f)
        {
            UIManager.instance.PauseScreen.SetActive(false);
            Time.timeScale = 1f;

            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            UI.SetActive(true);
        }
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            PauseUnpause();
        }
    }

    public void Respawn()
    {
        StartCoroutine(RespawnCo());
    }

    private IEnumerator RespawnCo()
    {
        HealthManager.instance.PlayerKilled();
        PlayerController.instance.gameObject.SetActive(false);
        UIManager.instance.FadeToBlack = true;
        Instantiate(PlayerDeathEffect, PlayerTransform.position, Quaternion.identity);
        AudioManager.instance.PlaySFX(7);
        yield return new WaitForSeconds(2f);
        UIManager.instance.FadeFromBlack = true;
        PlayerController.instance.transform.position = _respawnPosition;
        PlayerController.instance.gameObject.SetActive(true);
        HealthManager.instance.ResetHealth();

    }

    public void SetSpawnPoint(Vector3 newSpawnPoint)
    {
        _respawnPosition = newSpawnPoint;
    }

    public void AddCoins(int coinsToAdd)
    {
        CurrentCoins += coinsToAdd;
        UIManager.instance.CoinText.text = CurrentCoins.ToString();
    }

    public void PauseUnpause()
    {
        if (UIManager.instance.PauseScreen.activeInHierarchy ) //&& !LevelEnd.instance.IsGameEnd
        {
            UIManager.instance.PauseScreen.SetActive(false);
            Time.timeScale = 1f;

            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            UI.SetActive(true);
        }
        else //paused //if(!LevelEnd.instance.IsGameEnd)
        {
            UIManager.instance.PauseScreen.SetActive(true);
            UIManager.instance.CloseOptions();
            Time.timeScale = 0f;

            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            UI.SetActive(false);
        }

    }
}
