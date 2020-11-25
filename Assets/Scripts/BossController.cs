using System;
using UnityEngine;
using UnityEngine.UI;

public class BossController : MonoBehaviour
{
    #region fields
    public int MaxHealth = 9;
    public int CurrentHealth;
    public GameObject BossUI;
    public GameObject ScoreScreen;
    public GameObject PlayerUI;
    public GameObject[] HealthPointImages = new GameObject[9];
    public GameObject LeftHand;
    public GameObject RightHand;
    public GameObject Head;
    public Animator LeftHandAnimator;
    public Animator RightHandAnimator;
    public Animator HeadAnimator;
    private float _timer;
    private int _attackSpeed = 5;
    private int _attackRange = 3;
    #endregion

    void Start()
    {
        AudioManager.instance.PlayMusic(1);
    }
    private void Awake()
    {
        _timer += Time.deltaTime;
        //HeadController.SetBool(0, true);
        HeadAnimator.SetBool(0, false);
        //RightHandAnimator.SetBool("RightHandAttack", true);
        LeftHandAnimator.SetBool(1, true);
        //LeftHandController.SetBool(1, true);
    }
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StatFight();
        }
    }

    private void StatFight()
    {
        CurrentHealth = MaxHealth;
        AudioManager.instance.PlayMusic(2);
        SwitchUI(true);
    }

    private void Attack()
    {
        //if collider trigger ruki
        //player.hurt
    }
    private void Idle()
    {
        //play animation idle
    }

    private void HurtBoss()
    {
        // if collider trigger (na rukah tochki)
        AudioManager.instance.PlaySFX(1);
        CurrentHealth--;
        HealthPointImages[CurrentHealth+1].gameObject.SetActive(false);
        if (CurrentHealth <= 0)
        {
            BossDeath();
            EndLevel();
        }
    }

    private void EndLevel()
    {
        LevelEnd.instance.EndLevel(_timer);
        SwitchUI(false);
    }

    private void SwitchUI(bool value)
    {
        BossUI.SetActive(value);
        ScoreScreen.SetActive(!value);
        PlayerUI.gameObject.SetActive(value);
    }

    private void BossDeath()
    {
        //play Animations
        //playSFX
    }

}
