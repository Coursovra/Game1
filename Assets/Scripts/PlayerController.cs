using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region fields
    public static PlayerController instance;
    public LayerMask _groundMask;
    public GameObject playerModel;
    public GameObject[] playerPieces;
    public Transform _camera;
    private float _moveSpeed = 6f; 
    private float _jumpHeight = 0.8f;
    private float _gravityScale = 4f;
    private float _gravity = -9.81f;
    private float _rotateSpeed;
    private float _turnSmoothVelocity;
    private float _turnSmoothTime = 0.01f;
    private float _knockBackLength = 1f;
    private float _knockbackCounter;
    private float _groundDistance = 0.4f;
    private bool _isKnocking;
    private bool _isGrounded;
    private Vector3 _movementDirection;
    private Vector2 _knockbackPower = new Vector2(1f, 1f);
    private CharacterController _characterController;
    [SerializeField]
    private Transform _groundCheck;
    private Animator _animator;
    #endregion

    void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        if (!_isKnocking)
        {
            Movement();
        }
        if (_isKnocking)
        {
            KnockBack();
        }
    }

    private void Movement()
    {
        Vector3 moveDir = Vector3.zero;
        Vector3 direction = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical")).normalized;
        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + _camera.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _turnSmoothVelocity, _turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            _characterController.Move(moveDir.normalized * (_moveSpeed * Time.deltaTime));
        }

        _isGrounded = Physics.CheckSphere(_groundCheck.position, _groundDistance, _groundMask);
        if (_isGrounded)
        {
            if (Input.GetButtonDown("Jump"))
            {
                _movementDirection.y = Mathf.Sqrt(_jumpHeight * -2f * _gravity);
            }

        }
        _movementDirection.y += _gravity * Time.deltaTime;
        _characterController.Move(_movementDirection * Time.deltaTime);

        _animator.SetFloat("Speed", Mathf.Abs(moveDir.x) + Mathf.Abs(moveDir.z));
        _animator.SetBool("Grounded", _characterController.isGrounded);
    }

    private void KnockBack()
    {
        _knockbackCounter -= Time.deltaTime;

        float yStore = _movementDirection.y;
        _movementDirection = playerModel.transform.forward * -_knockbackPower.x;
        _movementDirection.y = yStore;

        if (_isGrounded)
        {
            _movementDirection.y = 0f;
        }

        _movementDirection.y += Physics.gravity.y * Time.deltaTime * _gravityScale;
        _characterController.Move(_movementDirection * Time.deltaTime);

        if (_knockbackCounter <= 0)
        {
            _isKnocking = false;
        }
    }

    public void StartKnockBack()
    {
        _isKnocking = true;
        _knockbackCounter = _knockBackLength;
        _movementDirection.y = _knockbackPower.y;
        _characterController.Move(_movementDirection * Time.deltaTime);
    }

    private void Awake()
    {
        instance = this;
    }
}


