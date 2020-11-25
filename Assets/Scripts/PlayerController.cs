using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region fields
    public static PlayerController instance;
    public LayerMask GroundMask;
    public GameObject PlayerModel;
    public GameObject[] PlayerPieces;
    public Transform Camera;
    public bool InDarkness;
    private Vector3 _movementDirection;
    private Vector2 _knockbackPower = new Vector2(3f, 8f);
    private CharacterController _characterController;
    [SerializeField]
    private Transform _groundCheck;
    private Animator _animator;
    private float _moveSpeed = 6f;
    private float _jumpHeight = 1f;
    private float _gravityScale = 4f;
    private float _gravity = -9.81f;
    private float _rotateSpeed;
    private float _turnSmoothVelocity;
    private float _turnSmoothTime = 0.01f;
    private float _knockBackLength = .5f;
    private float _knockbackCounter;
    private float _groundDistance = 0.1f;
    private bool _isKnocking;
    private bool _isGrounded;
    #endregion

    void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
    }
    void Update()
    {
        if (InDarkness)
        {
            GameManager.instance.Respawn();
        }
        if (!_isKnocking)
            Movement();

        if (_isKnocking)
            KnockBack();
    }

    private void Movement()
    {
        Vector3 moveDir = Vector3.zero;
        Vector3 direction = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical")).normalized;
        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + Camera.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _turnSmoothVelocity, _turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            _characterController.Move(moveDir.normalized * (_moveSpeed * Time.deltaTime));
        }

        _isGrounded = Physics.CheckSphere(_groundCheck.position, _groundDistance, GroundMask);
        if (_isGrounded)
        {
            if (Input.GetButtonDown("Jump"))
                _movementDirection.y = Mathf.Sqrt(_jumpHeight * -2f * _gravity);
        }
        else
            _movementDirection.y += _gravity * Time.deltaTime;

        _characterController.Move(_movementDirection * Time.deltaTime);

        _animator.SetFloat("Speed", Mathf.Abs(moveDir.x) + Mathf.Abs(moveDir.z));
        _animator.SetBool("Grounded", _characterController.isGrounded);
    }
    
    Vector3 moveDir = Vector3.zero;
    private void KnockBack()
    {       
        moveDir = PlayerModel.transform.forward * -_knockbackPower.x;
        moveDir.y += _gravity * Time.deltaTime;
        _characterController.Move(moveDir * Time.deltaTime);
        if (_knockbackCounter <= 0)
            _isKnocking = false;
        _knockbackCounter -= Time.deltaTime;
    }

    public void StartKnockBack()
    {
        _isKnocking = true;
        _knockbackCounter = _knockBackLength;
        moveDir.y = _knockbackPower.y;
        _characterController.Move(moveDir * Time.deltaTime);
    }

    private void Awake()
    {
        instance = this;
    }
}


