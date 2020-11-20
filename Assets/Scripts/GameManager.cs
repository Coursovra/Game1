using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private Vector3 respawnPosition;
    public GameObject DeathEffect;
    public GameObject UI;
    public Transform PlayerTransform;
    public int CurrentCoins;
    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        UIManager.instance.FadeFromBlack = true;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        respawnPosition = PlayerController.instance.transform.position;
        AddCoins(0);
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

    public IEnumerator RespawnCo()
    {
        HealthManager.instance.PlayerKilled();
        PlayerController.instance.gameObject.SetActive(false);
        UIManager.instance.FadeToBlack = true;
        Instantiate(DeathEffect, PlayerTransform.position, Quaternion.identity);
        yield return new WaitForSeconds(2f);
        UIManager.instance.FadeFromBlack = true;
        PlayerController.instance.transform.position = respawnPosition;
        PlayerController.instance.gameObject.SetActive(true);
        HealthManager.instance.ResetHealth();
    }

    public void SetSpawnPoint(Vector3 newSpawnPoint)
    {
        respawnPosition = newSpawnPoint;
    }

    public void AddCoins(int coinsToAdd)
    {
        CurrentCoins += coinsToAdd;
        UIManager.instance.coinText.text = CurrentCoins.ToString();
    }

    public void PauseUnpause()
    {
        if (UIManager.instance.PauseScreen.activeInHierarchy)
        {
            UIManager.instance.PauseScreen.SetActive(false);
            Time.timeScale = 1f;

            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            UI.SetActive(true);
        }
        else //paused
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
