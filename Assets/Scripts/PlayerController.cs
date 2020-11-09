using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerController : MonoBehaviour
{
    #region fields
    public static PlayerController instance;
    public float moveSpeed = 6f; 
    public float jumpForce = 5f;
    public Vector3 movementDirection;
    public float gravityScale = 5f;
    public float gravity = -9.81f;
    public CharacterController characterController;
    private Camera theCam;
    public GameObject playerModel;
    public float rotateSpeed;
    public float turnSmoothVelocity;
    public float turnSmoothTime = 0.1f;
    public Transform cam;
    public Animator anim;

    public bool isKnocking;
    public float knockBackLength = .5f;
    private float knockbackCounter;
    public Vector2 knockbackPower;

    public GameObject[] playerPieces;

    #endregion

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }
    // Update is called once per frame
    void Update()
    {

        if (!isKnocking)
        {
            Movement();
            Jump();
        }
        if (isKnocking)
        {
            KnockBack();
        }
    }

    private void Movement()
    {
        Vector3 direction = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical")).normalized;
        Debug.Log(direction.magnitude);
        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            movementDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            characterController.Move(movementDirection.normalized * (moveSpeed * Time.deltaTime));

        }
        movementDirection.y += gravity * Time.deltaTime;
        characterController.Move(movementDirection * Time.deltaTime);
    }

    private void Jump()
    {
        if (characterController.isGrounded && Input.GetButtonDown("Jump"))
        {
            movementDirection.y = Mathf.Sqrt(jumpForce * -2f * gravityScale);
        }
    }

    private void KnockBack()
    {
        knockbackCounter -= Time.deltaTime;

        float yStore = movementDirection.y;
        movementDirection = playerModel.transform.forward * -knockbackPower.x;
        movementDirection.y = yStore;

        if (characterController.isGrounded)
        {
            movementDirection.y = 0f;
        }

        movementDirection.y += Physics.gravity.y * Time.deltaTime * gravityScale;

        characterController.Move(movementDirection * Time.deltaTime);

        if (knockbackCounter <= 0)
        {
            isKnocking = false;
        }
    }



    public void Knockback()
    {
        isKnocking = true;
        knockbackCounter = knockBackLength;
        movementDirection.y = knockbackPower.y;
        characterController.Move(movementDirection * Time.deltaTime);
    }

    private void Awake()
    {
        instance = this;
    }


}


