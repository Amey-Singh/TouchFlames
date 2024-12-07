using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour, PlayerInputAction.IGameplayActions
{
    #region Variables

    [Header("Movement Settings")]
    public float movementSpeed = 10.0f;
    public float crouchSpeed = 5.0f;
    public float movementForceInAir = 0.5f;

    [Header("Roll Settings")]
    public float rollSpeed = 15.0f;
    public float rollForce = 10.0f;
    public float rollCooldown = 1.0f;
    public float rollTime = 0.5f;

    [Header("Jump Settings")]
    public int amountOfJumps = 1;
    public float jumpForce = 16.0f;
    public float jumpTimerSet = 0.15f;
    public float wallJumpForce = 20.0f;
    public float wallJumpCooldown = 0.2f;  
    
    public Vector2 wallJumpDirection = new Vector2(1, 2);
    public float wallJumpTimerSet = 0.5f;

    [Header("Dash Settings")]
    public float dashTime = 0.5f;
    public float dashSpeed = 20.0f;
    public float distanceBetweenImages = 0.5f;
    public float dashCoolDown = 1.0f;

    [Header("Wall Slide Settings")]
    public float wallSlideSpeed = 2.0f;
    public float wallCheckDistance = 1.0f;

    [Header("Ground Check Settings")]
    public float groundCheckRadius = 0.2f;
    public LayerMask whatIsGround;

    [Header("Ceiling Check Settings")]
    public Transform ceilingCheck;
    public float ceilingCheckRadius = 0.2f;
    public LayerMask whatIsCeiling;

    [Header("Ledge Climb Settings")]
    public float ledgeClimbXOffset1 = 0.5f;
    public float ledgeClimbYOffset1 = 1.0f;
    public float ledgeClimbXOffset2 = 0.5f;
    public float ledgeClimbYOffset2 = 1.0f;

    [Header("Audio Clips")]
    public AudioClip jumpSound;
    public AudioClip walkSound;
    public AudioClip dashSound;
    public AudioClip ledgeGrabSound;

    [Header("Transforms")]
    public Transform groundCheck;
    public Transform wallCheck;
    public Transform ledgeCheck;

    [Header("Other")]
    public ParticleMoveController particleController;

    private PlayerInputAction inputActions;
    private float movementInputDirection;
    private float jumpTimer;
    private float turnTimer;
    private float wallJumpTimer;
    private float dashTimeLeft;
    private float lastImageXpos;
    private float lastDash = -100f;
    private float rollCooldownTimer;
    private float wallJumpCooldownTimer;

    private int amountOfJumpsLeft;
    private int facingDirection = 1;
    private int lastWallJumpDirection;

    private bool isFacingRight = true;
    private bool isWalking;
    private bool isGrounded;
    private bool isTouchingWall;
    private bool isWallSliding;
    private bool canNormalJump;
    private bool canWallJump;
    private bool isAttemptingToJump;
    private bool checkJumpMultiplier;
    private bool canMove = true;
    private bool canFlip = true;
    private bool hasWallJumped;
    private bool isTouchingLedge;
    private bool canClimbLedge = false;
    private bool ledgeDetected;
    private bool isDashing;
    private bool isLedgeClimbing = false;
    private bool isCrouching;
    private bool isRolling;
    private bool isTouchingCeiling;
    private bool jumpPressed;
    private bool crouchPressed;


    private Vector2 moveInput;
    private Vector2 ledgePosBot;
    private Vector2 ledgePos1;
    private Vector2 ledgePos2;

    private Rigidbody2D rb;
    private Animator anim;
    private SoundManager soundManager;

    private const string crouchAnimationTrigger = "IsCrouching";
    private const string rollAnimationTrigger = "IsRolling";

    #endregion

    #region Unity Methods

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        inputActions = new PlayerInputAction();
        inputActions.Gameplay.SetCallbacks(this);
    }

    private void OnEnable()
    {
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        amountOfJumpsLeft = amountOfJumps;
        soundManager = SoundManager.instance;
        wallJumpDirection.Normalize();

        if (wallJumpDirection == Vector2.zero)
        {
            wallJumpDirection = new Vector2(1, 2).normalized;
        }
    }

    private void Update()
    {
        if (IsDialoguePlaying()) return;
        UpdateAnimations();
        CheckIfCanJump();
        CheckIfWallSliding();
        CheckJump();
        CheckLedgeClimb();
        CheckDash();
        HandleJumpAndCrouch();

        if (isWalking && isGrounded)
            soundManager.PlaySound(walkSound);

        if (rollCooldownTimer > 0)
            rollCooldownTimer -= Time.deltaTime;

        if (wallJumpCooldownTimer > 0)
            wallJumpCooldownTimer -= Time.deltaTime;
        
    }

    private void FixedUpdate()
    {
        if (IsDialoguePlaying()) return;

        ApplyMovement();
        CheckSurroundings();
        HandleCrouch();
        Move();
    }

    private void OnDrawGizmos()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }

        if (ceilingCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(ceilingCheck.position, ceilingCheckRadius);
        }

        if (wallCheck != null)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(wallCheck.position, wallCheck.position + transform.right * wallCheckDistance);
        }
    }

    #endregion

    private void Move()
    {
        if (isRolling || isDashing) return; // Prevent movement during rolling or dashing

        Vector2 targetVelocity = new Vector2(moveInput.x * movementSpeed, rb.velocity.y);
        rb.velocity = targetVelocity;
    }
    #region Custom Methods

    public void OnMovement(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        UpdateJumpAndCrouchInput();
    }

    public void OnRoll(InputAction.CallbackContext context)
    {
        if (IsDialoguePlaying()) return;
        if (context.performed && isGrounded && !isRolling && rollCooldownTimer <= 0 && !isCrouching)
        {
            StartRoll();
            rollCooldownTimer = rollCooldown;
        }
    }

    public void OnDash(InputAction.CallbackContext context)
    {
        if (IsDialoguePlaying()) return;
        if (context.performed && !isDashing && !isCrouching)
        {
            particleController.PlayTouchParticle();
            if (Time.time >= (lastDash + dashCoolDown))
                AttemptToDash();
        }
    }
    public void OnAttack1(InputAction.CallbackContext context)
    {
        if (IsDialoguePlaying()) return;
        // Placeholder for OnAttack1 logic
    }

    public void OnAttack2(InputAction.CallbackContext context)
    {
        if (IsDialoguePlaying()) return;
        // Placeholder for OnAttack2 logic
    }

    private void UpdateJumpAndCrouchInput()
    {
        jumpPressed = moveInput.y > 0.1f;
        crouchPressed = moveInput.y < -0.1f;
    }

    private void HandleJumpAndCrouch()
    {
        if (jumpPressed && !isAttemptingToJump)
        {
            isAttemptingToJump = true;
            HandleJumpInput();
        }
        if (!jumpPressed)
        {
            isAttemptingToJump = false;
        }

        if (crouchPressed)
        {
            StartCrouch();
        }
        else
        {
            StopCrouch();
        }
    }

    private void HandleJumpInput()
    {
        if (isGrounded || (amountOfJumpsLeft > 0 && !isTouchingWall))
        {
            NormalJump();
        }
        else if (isTouchingWall && wallJumpCooldownTimer <= 0)
        {
            WallJump();
        }
    }

    private void UpdateAnimations()
    {
        isWalking = Mathf.Abs(moveInput.x) > 0 && !isCrouching && !isRolling;
        anim.SetBool("isWalking", isWalking);
        anim.SetBool("grounded", isGrounded);
        anim.SetFloat("yVelocity", rb.velocity.y);
        anim.SetBool("isWallSliding", isWallSliding);
        anim.SetBool("isCrouching", isCrouching);
    }

    private void CheckIfCanJump()
    {
        if (isGrounded && rb.velocity.y <= 0.01f)
            amountOfJumpsLeft = amountOfJumps;

        canWallJump = isTouchingWall;
        canNormalJump = amountOfJumpsLeft > 0;
    }

    private void CheckIfWallSliding()
    {
        isWallSliding = isTouchingWall && !isGrounded && rb.velocity.y < 0 && !canClimbLedge;
    }


    private void CheckLedgeClimb()
    {
        if (ledgeDetected && !canClimbLedge)
            InitiateLedgeClimb();

        if (canClimbLedge)
            transform.position = ledgePos1;
    }

    private void InitiateLedgeClimb()
    {
        canClimbLedge = true;
        isLedgeClimbing = true;

        if (isFacingRight)
        {
            ledgePos1 = new Vector2(Mathf.Floor(ledgePosBot.x + wallCheckDistance) - ledgeClimbXOffset1, Mathf.Floor(ledgePosBot.y) + ledgeClimbYOffset1);
            ledgePos2 = new Vector2(Mathf.Floor(ledgePosBot.x + wallCheckDistance) + ledgeClimbXOffset2, Mathf.Floor(ledgePosBot.y) + ledgeClimbYOffset2);
        }
        else
        {
            ledgePos1 = new Vector2(Mathf.Ceil(ledgePosBot.x - wallCheckDistance) + ledgeClimbXOffset1, Mathf.Floor(ledgePosBot.y) + ledgeClimbYOffset1);
            ledgePos2 = new Vector2(Mathf.Ceil(ledgePosBot.x - wallCheckDistance) - ledgeClimbXOffset2, Mathf.Floor(ledgePosBot.y) + ledgeClimbYOffset2);
        }

        canMove = false;
        canFlip = false;
        anim.SetBool("canClimbLedge", canClimbLedge);
    }

    public void FinishLedgeClimb()
    {
        canClimbLedge = false;
        isLedgeClimbing = false;
        transform.position = ledgePos2;
        canMove = true;
        canFlip = true;
        ledgeDetected = false;
        anim.SetBool("canClimbLedge", canClimbLedge);
        if (isTouchingCeiling)
        {
            StartCrouch();
        }
    }

    private void CheckSurroundings()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
        isTouchingWall = Physics2D.Raycast(wallCheck.position, transform.right, wallCheckDistance, whatIsGround);
        isTouchingLedge = Physics2D.Raycast(ledgeCheck.position, transform.right, wallCheckDistance, whatIsGround);
        isTouchingCeiling = Physics2D.OverlapCircle(ceilingCheck.position, ceilingCheckRadius, whatIsCeiling);

        if (isTouchingWall && !isTouchingLedge && !ledgeDetected)
        {
            ledgeDetected = true;
            ledgePosBot = wallCheck.position;
        }

        // Reset jump when grounded
        if (isGrounded && rb.velocity.y <= 0.01f)
        {
            amountOfJumpsLeft = amountOfJumps;
        }
    }

    private void ApplyMovement()
    {
        if (canMove)
        {
            if (isGrounded && !isCrouching && !isRolling)
            {
                rb.velocity = new Vector2(movementSpeed * moveInput.x, rb.velocity.y);
            }
            else if (isCrouching)
            {
                rb.velocity = new Vector2(crouchSpeed * moveInput.x, rb.velocity.y);
            }
            else if (!isGrounded && !isWallSliding && moveInput.x != 0)
            {
                Vector2 force = new Vector2(movementForceInAir * moveInput.x, 0);
                rb.AddForce(force);

                if (Mathf.Abs(rb.velocity.x) > movementSpeed)
                {
                    rb.velocity = new Vector2(movementSpeed * moveInput.x, rb.velocity.y);
                }
            }

            if (!isRolling)
            {
                Flip();
            }

            if (isWallSliding && rb.velocity.y < -wallSlideSpeed)
            {
                rb.velocity = new Vector2(rb.velocity.x, -wallSlideSpeed);
            }
        }
    }

    private void NormalJump()
    {
        if (canNormalJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            amountOfJumpsLeft--;
            jumpTimer = 0;
            isAttemptingToJump = false;
            checkJumpMultiplier = true;

            anim.SetTrigger("jump");
            soundManager.PlaySound(jumpSound);
        }
    }

    private void CheckJump()
    {
        if (jumpTimer > 0)
        {
            if (!isGrounded && isTouchingWall && moveInput.x != 0 && moveInput.x != facingDirection)
                WallJump();
            else if (isGrounded)
                NormalJump();
        }

        if (checkJumpMultiplier && !jumpPressed)
        {
            checkJumpMultiplier = false;
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }
    }

    private void WallJump()
    {
        if (canWallJump )
        {
            int wallJumpDirectionX = isFacingRight ? -1 : 1;
            wallJumpCooldownTimer = wallJumpCooldown;
            isWallSliding = false;
            amountOfJumpsLeft = amountOfJumps;
            amountOfJumpsLeft--;

            // Determine wall jump direction based on facing direction
            Vector2 forceToAdd = new Vector2(wallJumpForce * wallJumpDirection.x * -facingDirection, wallJumpForce * wallJumpDirection.y);
            rb.velocity = forceToAdd;

            jumpTimer = 0;
            turnTimer = 0;
            canMove = true;
            canFlip = true;
            hasWallJumped = true;
            wallJumpTimer = wallJumpTimerSet;
            lastWallJumpDirection = -facingDirection;

            if ((isFacingRight && wallJumpDirectionX < 0) || (!isFacingRight && wallJumpDirectionX > 0))
            {
                facingDirection *= -1;
                isFacingRight = !isFacingRight;
                transform.Rotate(0, 180, 0);
            }

            soundManager.PlaySound(jumpSound);
        }
    }


    private void AttemptToDash()
    {
        if (!isDashing)
        {
            isDashing = true;
            dashTimeLeft = dashTime;
            lastDash = Time.time;

            PlayerAfterImagePool.Instance.GetFromPool();
            lastImageXpos = transform.position.x;

            anim.SetBool("isDashing", isDashing);
            soundManager.PlaySound(dashSound);
        }
    }

    private void CheckDash()
    {
        if (isDashing)
        {
            if (dashTimeLeft > 0)
            {
                canMove = false;
                canFlip = false;
                rb.velocity = new Vector2(dashSpeed * facingDirection, 0);
                dashTimeLeft -= Time.deltaTime;

                if (Mathf.Abs(transform.position.x - lastImageXpos) > distanceBetweenImages)
                {
                    PlayerAfterImagePool.Instance.GetFromPool();
                    lastImageXpos = transform.position.x;
                }
            }

            if (dashTimeLeft <= 0 || isTouchingWall)
            {
                isDashing = false;
                canMove = true;
                canFlip = true;
                anim.SetBool("isDashing", isDashing);
            }
        }
    }

    private void StartCrouch()
    {
        isCrouching = true;
        anim.SetTrigger(crouchAnimationTrigger);
    }

    private void StopCrouch()
    {
        if (!isTouchingCeiling)
        {
            isCrouching = false;
            anim.ResetTrigger(crouchAnimationTrigger);
        }
    }

    private bool CheckForCeiling()
    {
        return isTouchingCeiling;
    }

    private void HandleCrouch()
    {
        if (isCrouching && !CheckForCeiling() && !crouchPressed)
        {
            StopCrouch();
        }
    }

    private void StartRoll()
    {
        if (!isRolling && !isCrouching)
        {
            isRolling = true;
            anim.SetTrigger(rollAnimationTrigger);
            StartCoroutine(PerformRoll());
        }
    }

    private void StopRoll()
    {
        isRolling = false;
        anim.ResetTrigger(rollAnimationTrigger);

        if (isTouchingCeiling)
        {
            StartCrouch();
        }
    }

    private IEnumerator PerformRoll()
    {
        float rollDirection = isFacingRight ? 1 : -1;
        rb.velocity = new Vector2(rollDirection * rollSpeed, rb.velocity.y);
        rb.AddForce(new Vector2(rollDirection * rollForce, 0), ForceMode2D.Impulse);
        yield return new WaitForSeconds(rollTime);
        StopRoll();
    }

    private void Flip()
    {
        if (!isWallSliding && canFlip)
        {
            if ((isFacingRight && moveInput.x < 0) || (!isFacingRight && moveInput.x > 0))
            {
                facingDirection *= -1;
                isFacingRight = !isFacingRight;
                transform.Rotate(0, 180, 0);
            }
        }
    }

    private bool IsDialoguePlaying()
    {
        return DialogueManager.GetInstance().dialogueIsPlaying;
    }

    public int GetFacingDirection()
    {
        return facingDirection;
    }

    #endregion

    public int FacingDirection => facingDirection;
    public bool IsDashing { get { return isDashing; } }
    public bool IsRolling { get { return isRolling; } }
    public bool IsCrouching { get { return isCrouching; } }
    public bool IsWallSliding { get { return isWallSliding; } }
    public bool walking { get { return isWalking; } }
    public bool Grounded { get { return isGrounded; } }

}