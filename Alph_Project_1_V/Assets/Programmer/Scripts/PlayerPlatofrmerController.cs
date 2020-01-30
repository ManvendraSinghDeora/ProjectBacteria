using UnityEngine;

public class PlayerPlatofrmerController : MonoBehaviour
{
    public static PlayerPlatofrmerController instance { get;private set; }

    public bool isGrounded;
    public bool IsOnWall;
    public bool OnWallRight;
    public bool OnWallLeft;
    public bool WallGrab;
    public bool wallJumped;

    [SerializeField] float speed;
    [SerializeField] float WallslideSpeed;
    [SerializeField] float wallJumpLerp = 10;
    [SerializeField] float jumpForce;
    [SerializeField] float WallHopForce;
    [SerializeField] float collisionRadius;

    [SerializeField]int extraJumpValues;
    [SerializeField]int GravityScaleValue;

    [SerializeField] Transform RightWallCheck, LeftWallCheck,temp_right,Temp_left;

    [SerializeField] Transform GroundCheck;

    [SerializeField] LayerMask WhatIsGround;

    [SerializeField] KeyCode jumpKey;

    float HorizontalmoveInput;
    float VerticalMoveInput;

    int extraJump;
    int GravityScale;

    bool isFacingRight = true;

    Vector2 direction;


    Rigidbody2D rb;

    Animator animator;

    private void Awake()
    {
        instance = this;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(RightWallCheck.position, collisionRadius);
        Gizmos.DrawWireSphere(LeftWallCheck.position, collisionRadius);
    }
    void Start()
    {
        animator = GetComponent<Animator>();

        rb = GetComponent<Rigidbody2D>();

        extraJump = extraJumpValues;

        GravityScale = GravityScaleValue;

    }

    void Update()
    {

        //Horizontal Movement input
        HorizontalmoveInput = Input.GetAxis("Horizontal");
        //Vertial movement input(For WallClimb)
        VerticalMoveInput = Input.GetAxis("Vertical");

        //Direction
        direction = new Vector2(HorizontalmoveInput, VerticalMoveInput);

        //If player is trying to move left side then flip left side else flip right side.
        if(isFacingRight == false && HorizontalmoveInput > 0)
        {
            Flip();
        }
        else if(isFacingRight == true && HorizontalmoveInput < 0)
        {
            Flip();
        }

        //For playing Animation
        if(Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            RightWallCheck.gameObject.SetActive(false);
            LeftWallCheck.gameObject.SetActive(true);
            // Left-side walk animation;
        }
        else if(Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            // Right-side walk animation
            RightWallCheck.gameObject.SetActive(true);
            LeftWallCheck.gameObject.SetActive(false);
        }
        else
        {
            //animator.Play("Player_Idle");
        }

        //Checking if player is tying to jump and whether he's has an extra jump and is he grounded.
        //If yes then make him jump else decrement extrajump.
        if (Input.GetKeyDown(jumpKey) && extraJump > 0)
        {
            //Play jump animation
            rb.velocity = Vector2.up * jumpForce;
            extraJump--;
        }
        else if (Input.GetKeyDown(jumpKey) && extraJump == 0 && isGrounded == true)
        {
            //Play jump animation
            rb.velocity = Vector2.up * jumpForce;
        }

        //Checking if player is grounded.
        if (isGrounded == true)
        {
            extraJump = extraJumpValues;
        }

        //Checking if player is on wall and if he is then he will slide on wall.
        if(IsOnWall == true && isGrounded == false)
        {
            wallSlide();
        }

        //Checking if player is trying to climb wall
        if (WallGrab)
        {
            rb.gravityScale = 0;
            rb.velocity = new Vector2(rb.velocity.x, VerticalMoveInput * speed);
        }
        else if(WallGrab == false)
        {
            rb.gravityScale = GravityScale;
        }

        //Checking if player released the jump key.
        if (Input.GetKeyUp(jumpKey))
        {
            ////Play Jump_Fall animation if any.
            isGrounded = false;
        }

        if (IsOnWall && Input.GetKeyDown(jumpKey) && WallGrab == false)
        {
            wallJumped = true;
        }

        if (wallJumped)
        {
            Debug.Log("Wall Jumped");
            rb.velocity = new Vector2(direction.x * WallHopForce * 2, rb.velocity.y);
            wallJumped = false;
        }
        
    }

    void FixedUpdate()
    {
        // Moving player as per  left or right input using Rigidbody2D.
        move();
        //Making a small radius circle near player's feet to check whether player is on ground or not.
        isGrounded = Physics2D.OverlapCircle(GroundCheck.position, collisionRadius, WhatIsGround);

        //Checking if player is on right wall

        OnWallRight = Physics2D.OverlapCircle(RightWallCheck.position, collisionRadius, WhatIsGround);

        OnWallLeft = Physics2D.OverlapCircle(LeftWallCheck.position, collisionRadius, WhatIsGround);

        //Checking if player is on left wall

        // Checking if player is on wall or not.
        IsOnWall = OnWallRight || OnWallLeft;

        //Checking if player is trying to climb wall
        WallGrab = IsOnWall && Input.GetKey(KeyCode.LeftShift);
    }

    void move()
    {
        //Moving player.
        rb.velocity = new Vector2(HorizontalmoveInput * speed, rb.velocity.y);
    }

    void Flip()
    {
        //Changing this bool's value as per player's input and multiplying it's X's local scale by -1 to flip it
        isFacingRight =  !isFacingRight;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }

    void wallSlide()
    {
        rb.velocity = new Vector2(rb.velocity.x, -WallslideSpeed);
        if (OnWallRight)
        {
           
        }
    }

}