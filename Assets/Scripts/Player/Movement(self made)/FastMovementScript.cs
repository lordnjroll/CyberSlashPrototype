using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastMovementScript : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    public float groundDrag;
    public float OriginalDrag;
    private float desiredSpeed;

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
    public float WallrunJumpSideBoost;
    public float WallrunJumpUpBoost;
    public float WallCheckDistance;
    private RaycastHit leftWallHit;
    private RaycastHit rightWallHit;
    private bool wallLeft;
    private bool wallRight;
    private bool isWallRunning = false;
    private bool iswallDoubleJumpOnCD;

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


        desiredSpeed = moveSpeed;
        OriginalDrag = groundDrag;
        StartCoroutine(DragHandler());
    }

    private void Update()
    {
        // ground check
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.3f, whatIsGround);

        
        SpeedControl();
        CheckForWall();
    }

    private void FixedUpdate()
    {
        MovePlayer();
        MyInput();
        WallRunCheck();

        if (!grounded)
        {
            StartCoroutine(DragingPlayerDown());
        }
        else
        {
            StopCoroutine(DragingPlayerDown());
            airTime = 0;
        }

        if (isWallRunning)
        {
            WallRunningMovement();
          //Debug.Log("wall running");
        }
        /*if (!isWallRunning)
        {
            Debug.Log("not wall running");
        }*/
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        // when to jump
        if (Input.GetKey(jumpKey) && readyToJump && grounded && !isWallRunning)
        {
            readyToJump = false;

            Jump();
            if (grounded)
            {
                Invoke(nameof(ResetJump), jumpCooldown);
            }
       
        }
        if(Input.GetKey(jumpKey) && !grounded && remainingJump > 0 && !isWallRunning && !iswallDoubleJumpOnCD) //double jump
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
        //if (flatVel.magnitude > moveSpeed)
        if(flatVel.magnitude > desiredSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * desiredSpeed;
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

    private void WallRunCheck()
    {
        if((wallLeft || wallRight) && verticalInput > 0 && !grounded)
        {
            isWallRunning = true;
            readyToJump = true;
            remainingJump = Maxjumps;

            if (Input.GetKey(jumpKey)) //wall jump
            {
                Debug.Log("wall jumped");
                WallJump();
            }
        }
        else
        {
            isWallRunning = false;
        }
    }

   private void WallRunningMovement()
    {
        //Debug.Log("wall running");

        rb.useGravity = false;
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        Vector3 wallNormal = wallRight ? rightWallHit.normal : leftWallHit.normal;

        Vector3 wallForward = Vector3.Cross(wallNormal, transform.up);

        if((orientation.forward - wallForward).magnitude > (orientation.forward - -wallForward).magnitude)
        {
            wallForward = -wallForward;
        }

        //forward force
        rb.AddForce(wallForward * WallrunSpeedBoost, ForceMode.Force);

        // push to wall force
        if (!(wallLeft && horizontalInput > 0) && !(wallRight && horizontalInput < 0))
        {
            rb.AddForce(-wallNormal * 100 , ForceMode.Force);
        }

    }

   private void WallJump()
   {
        //determine which wall the player is running from
        Vector3 wallNormal = wallRight ? rightWallHit.normal : leftWallHit.normal;

        //add the side and up jump force
        Vector3 forceToApply = transform.up * WallrunJumpUpBoost + wallNormal * WallrunJumpSideBoost;

        //reset y velocity and add force
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(forceToApply, ForceMode.Force);

        iswallDoubleJumpOnCD = true;

        //to prevent the player from using their double jump immediately afte wall jumping
        Invoke("WallDoubleJumpCoolDown", 0.2f);
    }

    private void WallDoubleJumpCoolDown()
    {
        iswallDoubleJumpOnCD = false;

        //resets double jump
        remainingJump = Maxjumps;
    }
}
