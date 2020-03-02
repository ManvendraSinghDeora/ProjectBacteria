using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private float HorizontalInputDirection;
    private float VerticalInputDirection;
    private float turnTimer;

    private int amountOfJumpsLeft;
    private int facingDirection = 1;

    private bool isFacingRight = true;
    private bool isLeft = false;
    private bool isWalking;
    private bool isGrounded;
    private bool isTouchingWall;
    private bool isWallSliding;
    private bool canJump;
    private bool WallGrab;
    private bool CanClimbLedge;
    private bool canMove;
    private bool canFlip;

    private Rigidbody2D rb;
    private Animator anim;

    public int amountOfJumps = 1;
    [SerializeField]
    private int GravityScale;

    public float movementSpeed;
    public float jumpForce;
    public float groundCheckRadius;
    public float wallCheckDistance;
    public float wallSlideSpeed;
    public float wallClimbSpeed;
    public float airDragMultiplier = 0.95f;
    public float variableJumpHeightMultiplier = 0.5f;
    public float wallHopForce;
    public float wallJumpForce;
    public float turnTimerSet = 0.1f;

    public Vector2 wallHopDirection;
    public Vector2 wallJumpDirection;

    public Transform groundCheck;
    public Transform wallCheck;

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
        UpdateAnimations();
        CheckIfCanJump();
        CheckIfWallSliding();
        CheckIfGrabbingWall();
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

    private void CheckSurroundings()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);

        isTouchingWall = Physics2D.Raycast(wallCheck.position, transform.right, wallCheckDistance, whatIsGround);


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
            //Flip();
        }
        else if (!isFacingRight && HorizontalInputDirection > 0)
        {
            //Flip();
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
        //anim.SetBool("isGrounded", isGrounded);
        //anim.SetFloat("yVelocity", rb.velocity.y);
        //anim.SetBool("isWallSliding", isWallSliding);
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
            //    transform.Rotate(0.0f, 180.0f, 0.0f);
            if (!isLeft)
            {
                this.transform.GetComponent<SpriteRenderer>().flipX = true;
                isLeft = true;
            }
            else
            {
                this.transform.GetComponent<SpriteRenderer>().flipX = false;
                isLeft = false;
            }
        }
        else
        {

        }
         
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);

        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance, wallCheck.position.y, wallCheck.position.z));
    }
}