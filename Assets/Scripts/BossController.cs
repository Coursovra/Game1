using System.Collections;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.Audio;

public class BossController : MonoBehaviour
{
    #region fields
    public static BossController instance;
    public GameObject BossUI;
    public GameObject ScoreScreen;
    public GameObject PlayerUI;
    public GameObject[] HealthPointImages = new GameObject[9];
    public GameObject Head;
    public Animator BossAnimation;
    public GameObject LeftHand;
    public GameObject RightHand;
    [SerializeField]
    private GameObject[] _bossPieces;
    public bool IsPlayerInArena;
    public bool LeftSide;
    public bool RightSide;
    private bool _fightStarted;
    private bool _isDead;
    private bool _isScaryMusicPlaying;
    private bool _isActionMusicPlaying;
    private BoxCollider _arenaBoxCollider;
    private _bossState _currentState = _bossState.isIdle;
    private int _maxHealth = 9;
    private int _currentHealth;
    private float _invulnerabilityTime;
    private float _attackCoolDown;
    private float _attackRange = 6.2f;
    private float _timeToIdle = 5;
    private float _timer;
    [SerializeField]
    private AudioClips _audioClips;
    [SerializeField]
    private AudioSource _audioSourceMusic;
    [SerializeField]
    private AudioSource _audioSourceSFX;
    private LayerMask _playerLayerMask;
    private enum _bossState
    {
        isIdle,
        isAttacking,
        isDeath
    }
    #endregion

    private void Awake()
    {
        _arenaBoxCollider = GetComponent<BoxCollider>();
        instance = this;
        BossAnimation.SetBool("IsIdle", false);
        _playerLayerMask = LayerMask.GetMask("PlayerLayer");
        _audioSourceMusic.volume = .3f;
        _audioSourceSFX.volume = .3f;
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
        if (_attackCoolDown <= 0)
        {
            BossAnimation.SetBool(hand + "HandAttack", true);
            yield return new WaitForSeconds(1.5f);
            _attackCoolDown = 3.5f;
            if (_currentHealth < 5)
                _attackCoolDown = 2.5f;
            BossAnimation.SetBool(hand + "HandAttack", false);
        }
    }

    private IEnumerator StatFight()
    {
        _fightStarted = true;
        foreach (var piece in _bossPieces)
        {
            piece.SetActive(true);
        }
        BossAnimation.SetBool("Intro", true);


        yield return new WaitForSeconds(1.65f);
        BossAnimation.SetBool("Intro", false);

        _currentHealth = _maxHealth;
        SwitchUI(true);
    }

    private void Idle()
    {
        if (!_isScaryMusicPlaying)
        {
            _audioSourceMusic.clip = _audioClips.ScaryMusic;
            _audioSourceMusic.Play();
            _isScaryMusicPlaying = true;
            _isActionMusicPlaying = false;
        }

        if (IsPlayerInArena)
            _currentState = _bossState.isAttacking;
        _timeToIdle = 5;
        BossAnimation.SetBool("IsIdle", true);
    }
    
    private IEnumerator Death()
    {
        _isDead = true;
        _audioSourceMusic.clip = _audioClips.VictoryMusic;
        _audioSourceMusic.Play();
        BossAnimation.SetBool("Death", true);
        yield return new WaitForSeconds(7f);
        EndLevel();
    }

    private void Attacking()
    {
        _isScaryMusicPlaying = false;
        if (!_isActionMusicPlaying)
        {
            _isActionMusicPlaying = true;
            _audioSourceMusic.clip = _audioClips.ActionMusic;
            _audioSourceMusic.Play();
        }

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
            //else
                //StartCoroutine(Attack("Left"));
        }

        Head.transform.LookAt(new Vector3(PlayerController.instance.transform.position.x, 85, PlayerController.instance.transform.position.z));
        _invulnerabilityTime -= Time.deltaTime;
        if(_invulnerabilityTime <= 0)
            _attackCoolDown -= Time.deltaTime;
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

    public void RightHandShakeCamera()
    {
        CinemachineShake.Instance.ShakeCamera(8f, .5f, 1f);
    }
    public void LeftHandShakeCamera()
    {
        CinemachineShake.Instance.ShakeCamera(2f, .9f, .4f);
    }

}
