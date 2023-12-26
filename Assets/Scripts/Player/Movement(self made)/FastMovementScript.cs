using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastMovementScript : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;

    public float groundDrag;
    public float OriginalDrag;

    [Header("Jumping")]
    public float jumpForce;
    public float DoublejumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    public float remainingJump;
    public float Maxjumps = 2;
    public float DownwardForce = 17;
    private float airTime;
    bool readyToJump;

    [HideInInspector] public float walkSpeed;
    [HideInInspector] public float sprintSpeed;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    bool grounded;

    [Header("Wall Running")]
    public LayerMask whatIsWall;
    public float WallrunSpeedBoost;
    public float WallrunJumpBoost;
    public float WallCheckDistance;
    private RaycastHit leftWallHit;
    private RaycastHit rightWallHit;
    private bool wallLeft;
    private bool wallRight;


    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        readyToJump = true;
        remainingJump = Maxjumps;

        OriginalDrag = groundDrag;
        StartCoroutine(DragHandler());
    }

    private void Update()
    {
        // ground check
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.3f, whatIsGround);

        
        SpeedControl();

    }

    private void FixedUpdate()
    {
        MovePlayer();
        MyInput();

        if (!grounded)
        {
            StartCoroutine(DragingPlayerDown());
        }
        else
        {
            StopCoroutine(DragingPlayerDown());
            airTime = 0;
        }

    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        // when to jump
        if (Input.GetKey(jumpKey) && readyToJump && grounded)
        {
            readyToJump = false;

            Jump();
            if (grounded)
            {
                Invoke(nameof(ResetJump), jumpCooldown);
            }
       
        }
        if(Input.GetKey(jumpKey) && !grounded && remainingJump > 0) //double jump
        {
            DoubleJump();
            remainingJump -= 1;
        }
    }

    private void MovePlayer()
    {
        // calculate movement direction
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        // on ground
        if (grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);

        // in air
        else if (!grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        // limit velocity if needed
        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    private void Jump()
    {
        // reset y velocity
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void DoubleJump()
    {
        // reset y velocity
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * DoublejumpForce, ForceMode.VelocityChange);
    }

    private void ResetJump()
    {
        readyToJump = true;
        remainingJump = Maxjumps;
        
    }

    IEnumerator DragHandler()
    {
        // handle drag
        if (grounded)
            rb.drag = groundDrag;
        else
            rb.drag = 0;


        //if the player is grounded, moving but not holding any movement key, increase the drag to act as counter weight
        if (verticalInput == 0 && horizontalInput == 0 && grounded)
        {
            groundDrag = 15;
        }
        else
        {
            groundDrag = OriginalDrag;
        }
        yield return null;
        StartCoroutine(DragHandler());
    }
    IEnumerator DragingPlayerDown()
    {

        // drag the player down when they jump
        if (!grounded)
        {
            
            airTime += Time.deltaTime;
            rb.AddForce(0, -(DownwardForce + airTime), 0, ForceMode.Acceleration);
        }   
        yield return null;
    }

    private void CheckForWall()
    {
        wallRight = Physics.Raycast(transform.position, orientation.right, out rightWallHit, WallCheckDistance, whatIsWall);
        wallLeft = Physics.Raycast(transform.position, -orientation.right, out leftWallHit, WallCheckDistance, whatIsWall);
    }

   private void WallRunningMovement()
    {

    }
}
