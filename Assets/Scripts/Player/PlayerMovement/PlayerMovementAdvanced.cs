using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementAdvanced : MonoBehaviour
{
    [Header("Movement")]
    public float CurrentSpeed;
    private float moveSpeed;
    public float walkSpeed;
    public float sprintSpeed;
    public float slideSpeed;
    public float wallrunSpeed;
    public float dashSpeed;
    public float dashSpeedChangeFactor;

    private float desiredMoveSpeed;
    private float lastDesiredMoveSpeed;
    private MovementState lastState;
    private bool keepMomentum;

    public float speedIncreaseMultiplier;
    public float slopeIncreaseMultiplier;

    public float groundDrag;
    public float OriginalDrag;

    [Header("Jumping")]
    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    public float jumpCounter;
    public float remainingJump;
    bool readyToJump;

    [Header("Falling Speed")]
    public float airTime;
    public float TerminalVelocity;
    public float PeakHeight;
    public float CurrentHeight;

    [Header("Crouching")]
    public float crouchSpeed;
    public float crouchYScale;
    private float startYScale;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode sprintKey = KeyCode.LeftShift;
    public KeyCode crouchKey = KeyCode.LeftControl;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    bool grounded;

    [Header("Slope Handling")]
    public float maxSlopeAngle;
    private RaycastHit slopeHit;
    private bool exitingSlope;

    [Header("Dashing")]
    public float dashForce;
    public float dashDuration;
    public float dashCdTimer;
    public float dashCd;
    public KeyCode dashKey = KeyCode.E;

    public Transform PlayerTransform;
    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;

    public MovementState state;
    public enum MovementState
    {
        walking,
        sprinting,
        dashing,
        wallrunning,
        crouching,
        sliding,
        air
    }

    public bool sliding;
    public bool wallrunning;
    public bool dashing;

    private void Start()
    {       

        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        readyToJump = true;

        startYScale = transform.localScale.y;

        OriginalDrag = groundDrag;

        remainingJump = jumpCounter;

        
    }

    private void Update()
    {
        // Check Current Speed
        CurrentSpeed = Vector3.Magnitude(rb.velocity);

        // ground check
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);

        MyInput();
        SpeedControl();
        StateHandler();

        if (!grounded)
        {
            StartCoroutine(DragingPlayerDown());
        }
        else
        {
            StopCoroutine(DragingPlayerDown());
            airTime = 0;
            CurrentHeight = 0;
            PeakHeight = 0;
        }

        if (Input.GetKeyDown(dashKey) && !wallrunning)
        {
            groundDrag = 5;
            Dash();
            Debug.Log("Dashed");
        }

        if(dashCdTimer > 0)
        {
            dashCdTimer -= Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        MovePlayer();

        // drag the player down when falling

        StartCoroutine(DragHandler());

    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if (Input.GetKey(jumpKey) && grounded)
        {
            groundDrag = 5;
        }

        // when to jump
        if (Input.GetKey(jumpKey) && readyToJump && grounded)
        {
            readyToJump = false;

            groundDrag = 5;

            Jump();

            Invoke(nameof(ResetJump), jumpCooldown);
        }

        // start crouch
        if (Input.GetKeyDown(crouchKey) && grounded)
        {
            transform.localScale = new Vector3(transform.localScale.x, crouchYScale, transform.localScale.z);
            rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);
        }

        // stop crouch
        if (Input.GetKeyUp(crouchKey))
        {
            transform.localScale = new Vector3(transform.localScale.x, startYScale, transform.localScale.z);
        }
    }

    private void StateHandler()
    {
        // Mode - Wallrunning
        if (wallrunning)
        {
            state = MovementState.wallrunning;
            desiredMoveSpeed = wallrunSpeed;
        }

        // Mode - Sliding
        else if (sliding)
        {
            state = MovementState.sliding;

            if (OnSlope() && rb.velocity.y < 0.1f)
                desiredMoveSpeed = slideSpeed;

            else
                desiredMoveSpeed = sprintSpeed;
        }

        //dashing
        else if (Input.GetKey(dashKey))
        {
            state = MovementState.dashing;
            desiredMoveSpeed = dashSpeed;
        }

        // Mode - Crouching
        else if (Input.GetKey(crouchKey))
        {
            state = MovementState.crouching;
            desiredMoveSpeed = crouchSpeed;
        }

        // Mode - Walking
        else if (grounded)
        {
            state = MovementState.walking;
            desiredMoveSpeed = walkSpeed;
        }

        // Mode - Air
        else
        {
            state = MovementState.air;
        }

        // check if desiredMoveSpeed has changed drastically
        if(Mathf.Abs(desiredMoveSpeed - lastDesiredMoveSpeed) > 4f && moveSpeed != 0)
        {
            StopAllCoroutines();
            StartCoroutine(SmoothlyLerpMoveSpeed());
        }
        else
        {
            moveSpeed = desiredMoveSpeed;
        }

        bool desiredMoveSpeedHasChanged = desiredMoveSpeed != lastDesiredMoveSpeed;
        if (lastState == MovementState.dashing) keepMomentum = true;

        if (desiredMoveSpeedHasChanged)
        {
            if (keepMomentum)
            {
                StopAllCoroutines();
                StartCoroutine(SmoothlyLerpMoveSpeed());
            }
            else
            {
                StopAllCoroutines();
                moveSpeed = desiredMoveSpeed;
            }
        }

        lastDesiredMoveSpeed = desiredMoveSpeed;
        lastState = state;
    }

    private IEnumerator SmoothlyLerpMoveSpeed()
    {
        // smoothly lerp movementSpeed to desired value
        float time = 0;
        float difference = Mathf.Abs(desiredMoveSpeed - moveSpeed);
        float startValue = moveSpeed;

        float boostFactor = dashSpeedChangeFactor;

        while (time < difference)
        {
            moveSpeed = Mathf.Lerp(startValue, desiredMoveSpeed, time / difference);

            if (OnSlope())
            {
                float slopeAngle = Vector3.Angle(Vector3.up, slopeHit.normal);
                float slopeAngleIncrease = 1 + (slopeAngle / 90f);

                time += Time.deltaTime * speedIncreaseMultiplier * slopeIncreaseMultiplier * slopeAngleIncrease;
            }
            else
                time += Time.deltaTime * boostFactor;

            yield return null;
        }

        moveSpeed = desiredMoveSpeed;
    }

    private void MovePlayer()
    {
        // calculate movement direction
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        // on slope
        if (OnSlope() && !exitingSlope)
        {
            rb.AddForce(GetSlopeMoveDirection(moveDirection) * moveSpeed * 20f, ForceMode.Force);

            if (rb.velocity.y > 0)
                rb.AddForce(Vector3.down * 80f, ForceMode.Force);
        }

        // on ground
        else if(grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);

        // in air
        else if(!grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);

        // turn gravity off while on slope
        rb.useGravity = !OnSlope();
    }

    private void SpeedControl()
    {
        // limiting speed on slope
        if (OnSlope() && !exitingSlope)
        {
            if (rb.velocity.magnitude > moveSpeed)
                rb.velocity = rb.velocity.normalized * moveSpeed;
        }

        // limiting speed on ground or in air
        else
        {
            Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

            // limit velocity if needed
            if (flatVel.magnitude > moveSpeed)
            {
                Vector3 limitedVel = flatVel.normalized * moveSpeed;
                rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
            }
        }
    }

    private void Jump()
    {
        exitingSlope = true;

        // reset y velocity
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);

        //remainingJump -= 1;
    }
    private void ResetJump()
    {
        readyToJump = true;

        exitingSlope = false;

        //remainingJump = jumpCounter;
    }

    public bool OnSlope()
    {
        if(Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight * 0.5f + 0.3f))
        {
            float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
            return angle < maxSlopeAngle && angle != 0;
        }

        return false;
    }

    public Vector3 GetSlopeMoveDirection(Vector3 direction)
    {
        return Vector3.ProjectOnPlane(direction, slopeHit.normal).normalized;
    }

    private void Dash()
    {
        if (dashCdTimer > 0) return;
        else dashCdTimer = dashCd;

        //forward and backward dash
        if (verticalInput < 0)
        {
            dashing = true;

            //backwards
            groundDrag = 0;
            Vector3 forceToApply = -(orientation.forward * dashForce);

            delayedForceToApplyVertical = forceToApply;
            Invoke(nameof(DelayedDashForceVertical), 0.025f);

            Invoke(nameof(ResetDash), dashDuration);
        }
        if (verticalInput > 0 || (verticalInput == 0 && horizontalInput == 0))
        {
            dashing = true;

            //forward and default dash
            groundDrag = 0;
            Vector3 forceToApply = orientation.forward * dashForce;

            delayedForceToApplyVertical = forceToApply;
            Invoke(nameof(DelayedDashForceVertical), 0.025f);

            Invoke(nameof(ResetDash), dashDuration);
        }

        //sideways dash
        if (horizontalInput < 0)
        {
            dashing = true;

            //right
            groundDrag = 0;
            Vector3 forceToApply = -(orientation.right * dashForce);

            delayedForceToApplyHorizontal = forceToApply;
            Invoke(nameof(DelayedDashForceHorizontal), 0.025f);

            Invoke(nameof(ResetDash), dashDuration);
        }
        if (horizontalInput > 0)
        {
            dashing = true;

            //left
            groundDrag = 0;
            Vector3 forceToApply = orientation.right * dashForce;

            delayedForceToApplyHorizontal = forceToApply;
            Invoke(nameof(DelayedDashForceHorizontal), 0.025f);

            Invoke(nameof(ResetDash), dashDuration);
        }

    }


    private Vector3 delayedForceToApplyVertical;
    private Vector3 delayedForceToApplyHorizontal;
    private void DelayedDashForceVertical()
    {
        rb.AddForce(delayedForceToApplyVertical, ForceMode.Impulse);

    }
    private void DelayedDashForceHorizontal()
    {
        rb.AddForce(delayedForceToApplyHorizontal, ForceMode.Impulse);

    }

    private void ResetDash()
    {
        dashing = false;
    }

    IEnumerator DragingPlayerDown()
    {
        if (!grounded)
        {
            airTime += Time.deltaTime;
            if (wallrunning)
            {
                airTime = 0;
                CurrentHeight = 0;
                PeakHeight = 0;
            }

            if (airTime > 0.1 && !wallrunning)
            {
                CurrentHeight += PlayerTransform.transform.position.y;
                PeakHeight = PlayerTransform.transform.position.y;
                rb.AddForce(0, -(1 + airTime), 0, ForceMode.Acceleration);
            }
        }
        
        yield return null;
    }

    IEnumerator DragHandler()
    {
        // handle drag
        if (grounded)
            rb.drag = groundDrag;
        else
            rb.drag = 0;

        if (verticalInput == 0 && horizontalInput == 0 && grounded && !dashing)
        {
            groundDrag = 15;
        }
        else
        {
            groundDrag = OriginalDrag;
        }
        yield return null;
    }
}