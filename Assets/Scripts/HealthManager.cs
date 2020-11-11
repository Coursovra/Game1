using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static HealthManager instance;
    public int CurrentHealth, MaxHealth;
    private float _invincibleLength = 2f;
    private float _invincCounter;
    public Sprite[] HealthBarImages = new Sprite[5];

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        ResetHealth();
    }

    // Update is called once per frame
    void Update()
    {
        if (_invincCounter > 0)
        {
            _invincCounter -= Time.deltaTime;

            for (int i = 0; i < PlayerController.instance.playerPieces.Length; i++)
            {
                if (Mathf.Floor(_invincCounter * 5f) % 2 == 0)
                {
                    PlayerController.instance.playerPieces[i].SetActive(true);
                }
                else
                {
                    PlayerController.instance.playerPieces[i].SetActive(false);
                }


                if (_invincCounter <= 0)
                {
                    PlayerController.instance.playerPieces[i].SetActive(true);
                }
            }
        }
    }

    public void Hurt()
    {
        if (_invincCounter <= 0)
        {
            CurrentHealth--;

            if (CurrentHealth <= 0)
            {
                CurrentHealth = 0;
                GameManager.instance.Respawn();       
            }
            else
            {
                PlayerController.instance.StartKnockBack();
                _invincCounter = _invincibleLength;
            }
            UpdateUI();
            AudioManager.instance.PlaySFX(7);
        }
    }

    public void ResetHealth()
    {
        UIManager.instance.HealthImage.enabled = true;
        CurrentHealth = MaxHealth;
        UpdateUI();
    }

    public void AddHealth(int amountToHeal)
    {
        CurrentHealth += amountToHeal;
        if (CurrentHealth > MaxHealth)
        {
            CurrentHealth = MaxHealth;
        }
        UIManager.instance.HealthImage.enabled = true;
        UpdateUI();
    }

    public void UpdateUI()
    {
        UIManager.instance.HealthText.text = CurrentHealth.ToString();
        switch (CurrentHealth)
        {
            case 5:
                UIManager.instance.HealthImage.sprite = HealthBarImages[4];
                break;
            case 4:
                UIManager.instance.HealthImage.sprite = HealthBarImages[3];
                break;
            case 3:
                UIManager.instance.HealthImage.sprite = HealthBarImages[2];
                break;

            case 2:
                UIManager.instance.HealthImage.sprite = HealthBarImages[1];
                break;
            case 1:
                UIManager.instance.HealthImage.sprite = HealthBarImages[0];
                break;
            case 0:
                UIManager.instance.HealthImage.enabled = false;
                break;
        }
    }

    public void PlayerKilled()
    {
        CurrentHealth = 0;
        UpdateUI();
    }
}
