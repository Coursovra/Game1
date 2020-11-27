using System.Collections;
using DefaultNamespace;
using UnityEngine;

public class BossController : MonoBehaviour
{
    #region fields
    public static BossController instance;
    public GameObject BossUI;
    public GameObject ScoreScreen;
    public GameObject PlayerUI;
    public GameObject[] HealthPointImages = new GameObject[9];
    public GameObject Head;
    public GameObject LeftHand;
    public GameObject RightHand;
    public Animator BossAnimation;
    // public AudioSource ScaryMusic;
    // public AudioSource ActionMusic;
    // public AudioSource VictoryMusic;
    // public AudioSource LeftHandSfx;
    // public AudioSource RightHandSfx;
    public bool IsPlayerInArena;
    public bool LeftSide;
    public bool RightSide;
    private bool _fightStarted;
    private BoxCollider _arenaBoxCollider;
    private _bossState _currentState = _bossState.isIdle;
    private int _maxHealth = 9;
    [SerializeField]
    private int _currentHealth;
    private float _invulnerabilityTime;
    private float _attackSpeed;
    private float _attackRange = 6.2f;
    private float _timeToIdle = 5;
    private float _timer;
    [SerializeField]
    private AudioClips _audioClips;
    [SerializeField]
    private AudioSource _audioSourceMusic;
    [SerializeField]
    private AudioSource _audioSourceSFX;

    private bool _isActionMusicPlaying;
    private bool _isVictoryMusicPlaying;
    private LayerMask _playerLayerMask;
    private bool _isDead;
    private enum _bossState
    {
        isIdle,
        isAttacking,
        isDeath
    }
    #endregion

    void Start()
    {
        _arenaBoxCollider = GetComponent<BoxCollider>();
    }
    private void Awake()
    {
        instance = this;
        BossAnimation.SetBool("IsIdle", false);
        _audioSourceMusic.clip = _audioClips.ScaryMusic;
        _audioSourceMusic.Play();
        _playerLayerMask = LayerMask.GetMask("PlayerLayer");
    }
    void Update()
    {
        _timer += Time.deltaTime;
        IsPlayerInArena = Physics.CheckBox(transform.TransformPoint(_arenaBoxCollider.center), _arenaBoxCollider.size/2,
            Quaternion.identity, _playerLayerMask);

        if (IsPlayerInArena && !_fightStarted)
        {
            StartCoroutine(StatFight());
        }
        BossState();
    }

    private void BossState()
    {
        switch (_currentState)
        {
            case _bossState.isIdle:
            {
                Idle();
                break;
            }
            case _bossState.isAttacking:
            {
                Attacking();
                break;
            }
            case _bossState.isDeath:
                if(!_isDead)
                    StartCoroutine(Death());
                break;
        }
    }

    private IEnumerator Attack(string hand)
    {
        if (_attackSpeed <= 0)
        {
            BossAnimation.SetBool(hand + "HandAttack", true);
            yield return new WaitForSeconds(1.5f);
            _attackSpeed = 3.5f;
            BossAnimation.SetBool(hand + "HandAttack", false);
        }
    }

    private IEnumerator StatFight()
    {
        if (!_isActionMusicPlaying)
        {
            _audioSourceMusic.clip = _audioClips.ActionMusic;
            _audioSourceMusic.Play();
            _isActionMusicPlaying = true;
        }
        Head.SetActive(true);
        RightHand.SetActive(true);
        LeftHand.SetActive(true);
        BossAnimation.SetBool("Intro", true);
        yield return new WaitForSeconds(1.7f);
        BossAnimation.SetBool("Intro", false);
        _fightStarted = true;
        _currentHealth = _maxHealth;
        SwitchUI(true);
    }

    private void Idle()
    {
        if (IsPlayerInArena)
            _currentState = _bossState.isAttacking;
        _timeToIdle = 5;
        BossAnimation.SetBool("IsIdle", true);
        if (!_audioSourceMusic.isPlaying)
            _audioSourceMusic.Play();
        
    }
    
    private IEnumerator Death()
    {
        if (!_isVictoryMusicPlaying)
        {
            _audioSourceMusic.clip = _audioClips.ActionMusic;
            _audioSourceMusic.Play();
            _isVictoryMusicPlaying = true;
        }
        _audioSourceMusic.clip = _audioClips.VictoryMusic;
        _audioSourceMusic.Play();
        BossAnimation.SetBool("Death", true);
        yield return new WaitForSeconds(7f);
        _isDead = true;
        EndLevel();
    }

    private void Attacking()
    {
        if (!IsPlayerInArena)
        {
            _timeToIdle -= Time.deltaTime;
            if(_timeToIdle <= 0)
                _currentState = _bossState.isIdle;
        }
        if (Vector3.Distance((LeftHand.transform.position + RightHand.transform.position)/2, PlayerController.instance.transform.position) <= _attackRange)
        {
            if (LeftSide)
                StartCoroutine(Attack("Left"));
            else if (RightSide)
                StartCoroutine(Attack("Right"));
            else
                StartCoroutine(Attack("Left"));
        }
        _invulnerabilityTime -= Time.deltaTime;
        if(_invulnerabilityTime <= 0)
            _attackSpeed -= Time.deltaTime;
    }

    public IEnumerator HurtBoss()
    {
        if(_invulnerabilityTime <= 0)
        {
            AudioManager.instance.PlaySFX(1);
            _currentHealth--;
            HealthPointImages[_currentHealth].gameObject.SetActive(false);
            if (_currentHealth <= 0)
            {
                _currentState = _bossState.isDeath;
            }
            yield return new WaitForSeconds(.2f);
            BossAnimation.SetBool("Hurt", true);
            _invulnerabilityTime = 1.5f;
            yield return new WaitForSeconds(1.5f);
            BossAnimation.SetBool("Hurt", false);
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

    public void PlayLAttackSfx()
    {
        _audioSourceSFX.clip = _audioClips.LeftHandAttack;
        _audioSourceSFX.Play();
    }
    public void PlayRAttackSfx()
    {
        _audioSourceSFX.clip = _audioClips.RightHandAttack;
        _audioSourceSFX.Play();
    }
}
