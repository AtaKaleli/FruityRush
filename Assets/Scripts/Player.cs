using UnityEngine;

public class Player : MonoBehaviour
{


    private Rigidbody2D rb;
    private Animator anim;

    [Header("PlayerMove")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float doubleJumpForce;
    [SerializeField] private float bufferJumpTime;
    private float bufferJumpCounter;
    
    private float defaultJumpForce;
    private bool canMove;


    private float horizontalInput;
    private float verticalInput;

    [Header("GroundCollisionChecks")]
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private float groundCheckDistance;
    private bool isGrounded;
    private bool canDoubleJump = true;

    private bool isFacingRight = true;
    private int facingDirection = 1;

    [Header("WallCollisionChecks")]

    [SerializeField] private float wallCheckDistance;
    private bool isTouchingWall;
    private bool canWallSlide;
    private bool isWallSliding;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        defaultJumpForce = jumpForce;
    }


    void Update()
    {
        AnimationControllers();

        CollisionChecks();

        bufferJumpCounter -= Time.deltaTime;

        InputChecks();
        FlipController();

        if (isGrounded)
        {
            canMove = true;
            canDoubleJump = true;

            if(bufferJumpCounter > 0)
            {
                bufferJumpCounter = -1;
                Jump();
            }

        }

        if (canWallSlide)
        {
            isWallSliding = true;
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.1f);
        }






        Move();



    }

    private void AnimationControllers()
    {
        bool isMoving = rb.velocity.x != 0;
        anim.SetBool("isMoving", isMoving);

        anim.SetBool("isGrounded", isGrounded);
        anim.SetFloat("yVelocity", rb.velocity.y);

        anim.SetBool("isWallSliding", isWallSliding);
        anim.SetBool("isWallDetected", isTouchingWall);
    }

    private void InputChecks()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        if (verticalInput < 0)
            canWallSlide = false;



        if (Input.GetKeyDown(KeyCode.Space))
        {
            JumpButton();
        }
    }

    private void JumpButton()
    {

        if (!isGrounded)
        {
            bufferJumpCounter = bufferJumpTime;
        }

        if (isWallSliding)
        {
            WallJump();
            canDoubleJump = true;
        }

        else if (isGrounded)
            Jump();

        else if (canDoubleJump)
        {
            canMove = true;
            canDoubleJump = false;
            jumpForce = doubleJumpForce;
            Jump();
            jumpForce = defaultJumpForce;
        }

        canWallSlide = false;
    }

    private void WallJump()
    {
        canMove = false;
        rb.velocity = new Vector2(5 * -facingDirection, jumpForce);
    }

    private void Move()
    {

        if (canMove)
            rb.velocity = new Vector2(moveSpeed * horizontalInput, rb.velocity.y);

    }

    private void Flip()
    {
        facingDirection = facingDirection * -1;
        isFacingRight = !isFacingRight;
        transform.Rotate(0, 180, 0);
    }

    private void FlipController()
    {
        if (isFacingRight && rb.velocity.x < 0)
            Flip();
        else if (!isFacingRight && rb.velocity.x > 0)
            Flip();
    }


    private void CollisionChecks()
    {
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, whatIsGround);
        isTouchingWall = Physics2D.Raycast(transform.position, Vector2.right * facingDirection, wallCheckDistance, whatIsGround);

        if (isTouchingWall && rb.velocity.y < 0)
        {
            canWallSlide = true;
        }

        if (!isTouchingWall)
        {
            isWallSliding = false;
            canWallSlide = false;
        }
    }

    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x, transform.position.y - groundCheckDistance));
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x + wallCheckDistance * facingDirection, transform.position.y));
    }



}
