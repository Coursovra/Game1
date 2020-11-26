using UnityEngine;

public class HealthManager : MonoBehaviour
{
    #region fields
    public static HealthManager instance;
    public Sprite[] HealthBarImages = new Sprite[5];
    public int CurrentHealth, MaxHealth;
    public float InvincCounter;
    private float _invincibleLength = 2f;
    #endregion
    private void Awake()

    {
        instance = this;
    }
    void Start()
    {
        ResetHealth();
    }

    void Update()
    {
        DamageEffect();
    }

    public void DamageEffect()
    {
        if (InvincCounter > 0)
        {
            InvincCounter -= Time.deltaTime;

            for (int i = 0; i < PlayerController.instance.PlayerPieces.Length; i++)
            {
                if (Mathf.Floor(InvincCounter * 5f) % 2 == 0)
                {
                    PlayerController.instance.PlayerPieces[i].SetActive(true);
                }
                else
                {
                    PlayerController.instance.PlayerPieces[i].SetActive(false);
                }


                if (InvincCounter <= 0)
                {
                    PlayerController.instance.PlayerPieces[i].SetActive(true);
                }
            }
        }
    }
    public void Hurt()
    {
        if (InvincCounter <= 0)
        {
            CurrentHealth--;
            AudioManager.instance.PlaySFX(7);
            if (CurrentHealth <= 0)
            {
                CurrentHealth = 0;
                GameManager.instance.Respawn();       
            }
            else
            {
                PlayerController.instance.StartKnockBack();
                InvincCounter = _invincibleLength;
            }
            UpdateUI();
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
