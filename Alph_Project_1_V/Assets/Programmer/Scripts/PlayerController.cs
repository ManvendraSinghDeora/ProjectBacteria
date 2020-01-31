using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private float HorizontalInputDirection;
    private float VerticalInputDirection;
    private float turnTimer;

    private int amountOfJumpsLeft;
    private int facingDirection = 1;

    private bool isFacingRight = true;
    private bool isWalking;
    private bool isGrounded;
    private bool isTouchingWall;
    private bool isWallSliding;
    private bool canJump;
    private bool WallGrab;
    private bool IsTouchingLedge;
    private bool CanClimbLedge;
    private bool LedgeDetected;
    private bool canMove;
    private bool canFlip;

    private Rigidbody2D rb;
    private Animator anim;

    public int amountOfJumps = 1;
    private int GravityScale = 8;

    public float movementSpeed = 10.0f;
    public float jumpForce = 16.0f;
    public float groundCheckRadius;
    public float wallCheckDistance;
    public float wallSlideSpeed;
    public float wallClimbSpeed;
    public float movementForceInAir;
    public float airDragMultiplier = 0.95f;
    public float variableJumpHeightMultiplier = 0.5f;
    public float wallHopForce;
    public float wallJumpForce;
    public float ledgeClimbXOffset1 = 0f;
    public float ledgeClimbYOffset1 = 0f;
    public float ledgeClimbXOffset2 = 0f;
    public float ledgeClimbYOffset2 = 0f;
    public float turnTimerSet = 0.1f;

    public Vector2 wallHopDirection;
    public Vector2 wallJumpDirection;
    public Vector2 LedgePosBot;
    public Vector2 LedgePos1;
    public Vector2 LedgePos2;

    public Transform groundCheck;
    public Transform wallCheck;
    public Transform LedgeCheck;

    public LayerMask whatIsGround;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        amountOfJumpsLeft = amountOfJumps;
        wallHopDirection.Normalize();
        wallJumpDirection.Normalize();
    }

    // Update is called once per frame
    void Update()
    {
        CheckInput();
        CheckMovementDirection();
        //UpdateAnimations();
        CheckIfCanJump();
        CheckIfWallSliding();
        CheckIfGrabbingWall();
        CheckLedgeClimb();
    }

    private void FixedUpdate()
    {
        ApplyMovement();
        CheckSurroundings();
    }

    private void CheckIfWallSliding()
    {
        if (isTouchingWall && !isGrounded && rb.velocity.y < 0 && !CanClimbLedge)
        {
            isWallSliding = true;
        }
        else
        {
            isWallSliding = false;
        }
    }

    private void CheckLedgeClimb()
    {
        if(LedgeDetected && !CanClimbLedge)
        {
            CanClimbLedge = true;

            if (isFacingRight)
            {
                LedgePos1 = new Vector2(Mathf.Floor(LedgePosBot.x + wallCheckDistance) - ledgeClimbXOffset1, Mathf.Floor(LedgePosBot.y) + ledgeClimbYOffset1);
                LedgePos2 = new Vector2(Mathf.Floor(LedgePosBot.x + wallCheckDistance) + ledgeClimbXOffset2, Mathf.Floor(LedgePosBot.y) + ledgeClimbYOffset2);
            }
            else
            {
                LedgePos1 = new Vector2(Mathf.Ceil(LedgePosBot.x - wallCheckDistance) + ledgeClimbXOffset1, Mathf.Floor(LedgePosBot.y) + ledgeClimbYOffset1);
                LedgePos2 = new Vector2(Mathf.Ceil(LedgePosBot.x - wallCheckDistance) - ledgeClimbXOffset2, Mathf.Floor(LedgePosBot.y) + ledgeClimbYOffset2);
            }

            canMove = false;
            canFlip = false;

            anim.SetBool("canClimbLedge", CanClimbLedge);
        }

        if (CanClimbLedge)
        {
            transform.position = LedgePos1;
        }

    }

    public void FinishLedgeClimb()
    {
        CanClimbLedge = false;
        transform.position = LedgePos2;
        canMove = true;
        canFlip = true;
        LedgeDetected = false;
        anim.SetBool("canClimbLedge", CanClimbLedge);
    }

    private void CheckSurroundings()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);

        isTouchingWall = Physics2D.Raycast(wallCheck.position, transform.right, wallCheckDistance, whatIsGround);

        IsTouchingLedge = Physics2D.Raycast(LedgeCheck.position, transform.right, wallCheckDistance, whatIsGround);

        if(isTouchingWall && !IsTouchingLedge && !LedgeDetected)
        {
            LedgeDetected = true;
            LedgePosBot = wallCheck.position;
        }

    }

    private void CheckIfCanJump()
    {
        if ((isGrounded && rb.velocity.y <= 0) || isWallSliding)
        {
            amountOfJumpsLeft = amountOfJumps;
        }

        if (amountOfJumpsLeft <= 0)
        {
            canJump = false;
        }
        else
        {
            canJump = true;
        }

    }
    private void CheckIfGrabbingWall()
    {
        if (WallGrab)
        {
            rb.gravityScale = 0;
            rb.velocity = new Vector2(rb.velocity.x, VerticalInputDirection * wallClimbSpeed);
        }
        else if (WallGrab == false)
        {
            rb.gravityScale = GravityScale;
        }

    }

    private void CheckMovementDirection()
    {
        if (isFacingRight && HorizontalInputDirection < 0)
        {
            Flip();
        }
        else if (!isFacingRight && HorizontalInputDirection > 0)
        {
            Flip();
        }

        if (rb.velocity.x != 0)
        {
            isWalking = true;
        }
        else
        {
            isWalking = false;
        }
    }

    private void UpdateAnimations()
    {
        anim.SetBool("isWalking", isWalking);
        anim.SetBool("isGrounded", isGrounded);
        anim.SetFloat("yVelocity", rb.velocity.y);
        anim.SetBool("isWallSliding", isWallSliding);
    }

    private void CheckInput()
    {
        HorizontalInputDirection = Input.GetAxisRaw("Horizontal");
        VerticalInputDirection = Input.GetAxisRaw("Vertical");


        if(Input.GetButtonDown("Horizontal") && isTouchingWall)
        {
            if(!isGrounded && HorizontalInputDirection != facingDirection)
            {
                canMove = false;
                canFlip = false;
            }
        }

        if (!canMove)
        {
            turnTimer -= Time.deltaTime;

            if(turnTimer <= 0)
            {
                canMove = true;
                canFlip = true;
            }
        }

        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }

        if (Input.GetButtonUp("Jump"))
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * variableJumpHeightMultiplier);
        }

        WallGrab = isTouchingWall && Input.GetKey(KeyCode.LeftShift);

    }

    private void Jump()
    {
        if (canJump && !isWallSliding)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            amountOfJumpsLeft--;
        }
        else if (isWallSliding && HorizontalInputDirection == 0 && canJump) //Wall hop
        {
            isWallSliding = false;
            amountOfJumpsLeft--;
            Vector2 forceToAdd = new Vector2(wallHopForce * wallHopDirection.x * -facingDirection, wallHopForce * wallHopDirection.y);
            rb.AddForce(forceToAdd, ForceMode2D.Impulse);
        }
        else if ((isWallSliding || isTouchingWall) && HorizontalInputDirection != 0 && canJump) // Wall Jump
        {
            isWallSliding = false;
            amountOfJumpsLeft--;
            Vector2 forceToAdd = new Vector2(wallJumpForce * wallJumpDirection.x * HorizontalInputDirection, wallJumpForce * wallJumpDirection.y);
            rb.AddForce(forceToAdd, ForceMode2D.Impulse);
        }
    }

    private void ApplyMovement()
    {

        if (!isGrounded && !isWallSliding && HorizontalInputDirection == 0)
        {
            rb.velocity = new Vector2(rb.velocity.x * airDragMultiplier, rb.velocity.y);
        }
        else if(canMove)
        {
            rb.velocity = new Vector2(movementSpeed * HorizontalInputDirection, rb.velocity.y);
        }

        if (isWallSliding)
        {
            if (rb.velocity.y < -wallSlideSpeed)
            {
                rb.velocity = new Vector2(rb.velocity.x, -wallSlideSpeed);
            }
        }
    }

    private void Flip()
    {
        if (!isWallSliding && canFlip)
        {
            facingDirection *= -1;
            isFacingRight = !isFacingRight;
            transform.Rotate(0.0f, 180.0f, 0.0f);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);

        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance, wallCheck.position.y, wallCheck.position.z));
        Gizmos.DrawLine(LedgeCheck.position, new Vector3(LedgeCheck.position.x + wallCheckDistance, LedgeCheck.position.y, LedgeCheck.position.z));
    }
}