using System;
using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class BossController : MonoBehaviour
{
    #region fields

    public static BossController instance;
    public int MaxHealth = 9;
    public int CurrentHealth;
    public GameObject BossUI;
    public GameObject ScoreScreen;
    public GameObject PlayerUI;
    public GameObject[] HealthPointImages = new GameObject[9];
    public GameObject LeftHand;
    public GameObject RightHand;
    public GameObject Head;
    public Animator BossAnimation;
    public bool IsPlayerInArena;
    [SerializeField]
    public float WaitTime = 5;
    private float _timer;
    [SerializeField]
    private float _attackSpeed = 3;
    private int _attackRange = 3;
    [SerializeField]
    private _bossState _currentState = _bossState.isIdle;
    private bool _fightStarted;

    private BoxCollider collider;
    private enum _bossState
    {
        isIdle,
        isAttacking
    }
    #endregion

    void Start()
    {
        collider = GetComponent<BoxCollider>();
        AudioManager.instance.PlayMusic(1);
    }
    private void Awake()
    {
        instance = this;
        _timer += Time.deltaTime;
        BossAnimation.SetBool("IsIdle", false);
    }
    void Update()
    {
        IsPlayerInArena = Physics.CheckBox(transform.TransformPoint(collider.center), collider.size/2,
            Quaternion.identity,
            LayerMask.GetMask("PlayerLayer"));
        print(IsPlayerInArena);

        if (IsPlayerInArena && !_fightStarted)
        {
            StatFight();
        }
        BossState();
    }

    private void BossState()
    {
        switch (_currentState)
        {
            case _bossState.isIdle:
            {
                if (IsPlayerInArena)
                    _currentState = _bossState.isAttacking;
                WaitTime = 5;
                BossAnimation.SetBool("IsIdle", true);

                break;
            }
            case _bossState.isAttacking:
            {
                if (!IsPlayerInArena)
                {
                    WaitTime -= Time.deltaTime;
                    if(WaitTime <= 0)
                        _currentState = _bossState.isIdle;
                }

                BossAnimation.SetBool("IsIdle", false);
                _attackSpeed -= Time.deltaTime;
                if (_attackSpeed <= 0)
                {

                    StartCoroutine(PlayAnimation());

                    //BossAnimation.SetTrigger("LAttack");
                    BossAnimation.SetBool("IsIdle", true);
                    _attackSpeed = 3;
                }
                else
                {
                    //BossAnimation.ResetTrigger("LAttack");
                }

                //BossAnimation.SetBool("LeftHandAttack", false);
                break;
            }

        }
    }

    IEnumerator PlayAnimation()
    {
        BossAnimation.SetBool("IsIdle", false);
        BossAnimation.SetBool("LeftHandAttack", false);
        //BossAnimation.SetTrigger("LAttack");
        yield return new WaitForSeconds(3f);
    }

    private void StatFight()
    {
        _fightStarted = true;
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

    public void HurtBoss()
    {
        AudioManager.instance.PlaySFX(1);
        CurrentHealth--;
        print(CurrentHealth);
        HealthPointImages[CurrentHealth].gameObject.SetActive(false);
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

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.TransformPoint(collider.center), collider.size);
    }
}
