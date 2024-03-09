using UnityEngine;

public class Player : MonoBehaviour
{


    private Rigidbody2D rb;
    private Animator anim;

    [Header("PlayerMove")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;

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
    }


    void Update()
    {
        AnimationControllers();

        CollisionChecks();



        InputChecks();
        FlipController();

        if (isGrounded)
            canDoubleJump = true;

        if (canWallSlide)
        {
            isWallSliding = true;
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.1f);
        }
        
        if (facingDirection * (int)horizontalInput == -1)
            isTouchingWall = false;
        

        if (!isTouchingWall)
        {
            isWallSliding = false;
            Move();

        }

        


    }

    private void AnimationControllers()
    {
        bool isMoving = rb.velocity.x != 0;
        anim.SetBool("isMoving", isMoving);

        anim.SetBool("isGrounded", isGrounded);
        anim.SetFloat("yVelocity", rb.velocity.y);

        anim.SetBool("isWallSliding", isWallSliding);
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
        if (isGrounded)
            Jump();
        else if (canDoubleJump)
        {
            canDoubleJump = false;
            Jump();
        }
    }

    private void Move()
    {


        




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
            canWallSlide = false;
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
