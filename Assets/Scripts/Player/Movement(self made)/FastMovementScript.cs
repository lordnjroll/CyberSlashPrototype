using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastMovementScript : MonoBehaviour
{
    [Header("Player Object")]
    public GameObject PlayerObject;
    public Camera PlayerCam;
    private Vector3 CamPosition;

    [Header("Movement settings")]
    public float moveSpeed;
    public float defaultSpeed;
    public float groundDrag;
    public float OriginalDrag;
    private float desiredSpeed;

    [Header("Jump settings")]
    public float jumpForce;
    public float DoublejumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    public float remainingJump;
    public float Maxjumps = 2;
    public float DownwardForce = 17;
    private float airTime;
    private float jumpBuffer;
    bool readyToJump;

    [Header("Slam settings")]
    public float slamSpeed;
    public KeyCode CroutchKey = KeyCode.LeftControl;
    private float VelocityStorage;
    private bool isSlaming;
    private GameObject SlamRayCastObject; // store the information of the object the player is about to slam into
    private bool SlamFloorCheck;
    public LayerMask WhatIsEnemyLayer;
    private bool SlamEnemyHit = false;

    [HideInInspector] public float walkSpeed;
    [HideInInspector] public float sprintSpeed;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    public bool grounded;

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

    [Header("Desire speed increase settings")]
    public float wallRunDesireSpeedIncrease;
    public float wallJumpDesireSpeedIncrease;
    public float DashDesireSpeedIncrease;
    private float groundedTime = 0;
    public float bhopTimeLimit; //how long is the player allowed on the ground before losinig desire speed

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

        defaultSpeed = moveSpeed;
        desiredSpeed = moveSpeed;

        OriginalDrag = groundDrag;

        CamPosition = PlayerCam.transform.position;

        StartCoroutine(DragHandler());
    }

    private void Update()
    {
        // ground check
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.3f, whatIsGround);

        MyInput();
        SpeedControl();
        CheckForWall();
        BhopTimer();
    }

    private void FixedUpdate()
    {
        MovePlayer();
        //MyInput();
        WallRunCheck();

        if (!grounded)
        {
            StartCoroutine(DragingPlayerDown());
        }
        else
        {
            StopCoroutine(DragingPlayerDown());
            airTime = 0;
            rb.useGravity = true;
            CancelInvoke("SlamDown");
        }

        if (isWallRunning)
        {
            //increase desired speed
            desiredSpeed += wallRunDesireSpeedIncrease;

            WallRunningMovement();
          
        }
       
    }

    private void BhopTimer()
    {
        if (grounded)
        {
            groundedTime += Time.deltaTime;
        }
        if (!grounded)
        {
            groundedTime = 0;
        }

        if(groundedTime > bhopTimeLimit)
        {
            desiredSpeed = defaultSpeed;
        }
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        // when to jump
        if (Input.GetKeyDown(jumpKey) && readyToJump && grounded && !isWallRunning)
        {
            readyToJump = false;

            Jump();
            if (grounded)
            {
                Invoke(nameof(ResetJump), jumpCooldown);
            }
       
        }
        else if (Input.GetKeyDown(jumpKey) && remainingJump <= 0) //jump buffer
        {
            jumpBuffer += Time.deltaTime;

            if (grounded && jumpBuffer < 0.5f)
            {
                //allow the player to buffer their jump by half a second

                readyToJump = false;

                jumpBuffer = 0;

                Jump();
                if (grounded)
                {
                    Invoke(nameof(ResetJump), jumpCooldown);
                }
            }
        }
        //setting the max jump to one works, don't change it
        if (Input.GetKeyDown(jumpKey) && !grounded && remainingJump > 0 && !isWallRunning && !iswallDoubleJumpOnCD) 
        {
            DoubleJump();
            remainingJump -= 1;
        }
        


        //air slam
        if(!grounded && Input.GetKeyDown(CroutchKey))
        {
            //store the player's speed before slamming
            VelocityStorage = rb.velocity.magnitude;

            InvokeRepeating("SlamDown", 0, 0.1f);

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
        float velocity0 = 0.0f;

        // limit velocity if needed
        if(flatVel.magnitude > desiredSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * desiredSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }



        //increase the player's speed as the desiredSpeed build up

        if(desiredSpeed > defaultSpeed && moveSpeed < desiredSpeed)
        {
            //moveSpeed += 0.5f;

            moveSpeed = Mathf.SmoothDamp(moveSpeed, desiredSpeed, ref velocity0, 1f);
        }
        
        if(grounded && verticalInput == 0)
        {
            desiredSpeed = defaultSpeed;
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
        //increase desireSpeed
        desiredSpeed += wallJumpDesireSpeedIncrease;

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

    private void SlamDown()
    {
        isSlaming = true;
        VelocityStorage = rb.velocity.magnitude;

        rb.velocity = new Vector3(0, slamSpeed, 0);

        var ray = new Ray(this.transform.position, -this.transform.up); // shoots a ray below the player
        RaycastHit ObjectHit; // stores the information of the object the player slammed down to

        if(Physics.Raycast(ray, out ObjectHit, playerHeight  , whatIsGround + WhatIsEnemyLayer))
        {
            SlamRayCastObject = ObjectHit.transform.gameObject;
            if (SlamRayCastObject.layer == 3)
            {
                if(verticalInput == 0)
                {
                    //add a small acceleration boost if holding w 
                    rb.AddForce(rb.transform.forward * VelocityStorage , ForceMode.Impulse);
                    isSlaming = false;
                    CancelInvoke("SlamDown");
                }
                else
                {
                    isSlaming = false;
                    CancelInvoke("SlamDown");
                }
            } else if (SlamRayCastObject.layer == 8)
            {
                //if the player hits an enemy
                isSlaming = false;
                CancelInvoke("SlamDown");
            }
              
        }
     
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject stompedEnemy = collision.transform.gameObject;

        if (isSlaming && (stompedEnemy.layer == 8))
        {
            //freeze the enemy
            stompedEnemy.transform.position = new Vector3(stompedEnemy.transform.position.x, stompedEnemy.transform.position.y, stompedEnemy.transform.position.z);

            //freeze the player
            rb.constraints = RigidbodyConstraints.FreezePosition;
            StartCoroutine("SlammedEnemy");
            isSlaming = false;
            CancelInvoke("SlamDown");
        }
    }

    IEnumerator SlammedEnemy()
    {
        CancelInvoke("SlamDown");

        //push the player cam down
        PlayerCam.transform.position = new Vector3(PlayerCam.transform.position.x,PlayerCam.transform.position.y +0.1f, PlayerCam.transform.position.z);
        yield return new WaitForSeconds(.3f);  

        rb.velocity = new Vector3(0, 0, 0);
        rb.constraints = RigidbodyConstraints.None;
        desiredSpeed += 0.45f;

        //launch the player forward and upward
        rb.useGravity = false;
        rb.AddForce((rb.transform.forward * desiredSpeed * 5f ) + (rb.transform.up * jumpForce), ForceMode.Impulse);

        //return the cam to it's original position
        PlayerCam.transform.position = new Vector3(PlayerCam.transform.position.x, PlayerCam.transform.position.y - 0.1f, PlayerCam.transform.position.z);
        yield return new WaitForSeconds(.2f);
        ResetJump();
        rb.useGravity = true;
        CancelInvoke("SlamEnemy");
        
    }
}
