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
    [SerializeField] private Transform enemyCheck;
    [SerializeField] private float enemyCheckRadius;


    private bool isGrounded;
    private bool canDoubleJump = true;

    private bool isFacingRight = true;
    private int facingDirection = 1;

    [Header("WallCollisionChecks")]

    [SerializeField] private float wallCheckDistance;
    private bool isTouchingWall;
    private bool canWallSlide;
    private bool isWallSliding;


    [Header("KnockedBack Info")]
    [SerializeField] private Vector2 knockbackDirection;
    private bool isKnocked;
    private bool canBeKnocked = true;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        defaultJumpForce = jumpForce;
    }


    void Update()
    {
        AnimationControllers();
        if (isKnocked)
            return;

        CollisionChecks();

        bufferJumpCounter -= Time.deltaTime;

        InputChecks();
        FlipController();
        CheckForEnemy();

        if (isGrounded)
        {
            canMove = true;
            canDoubleJump = true;

            if (bufferJumpCounter > 0)
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

    private void CheckForEnemy()
    {
        Collider2D[] hitedColliders = Physics2D.OverlapCircleAll(enemyCheck.position, enemyCheckRadius);

        foreach (var enemy in hitedColliders)
        {
            if (enemy.GetComponent<Enemy>() != null)
            {
                if(rb.velocity.y < 0) //kill enemy only if we are falling
                {
                    enemy.GetComponent<Enemy>().Damage();
                    Jump();
                }
                
            }
        }
    }

    private void AnimationControllers()
    {
        bool isMoving = rb.velocity.x != 0;
        anim.SetBool("isMoving", isMoving);

        anim.SetBool("isGrounded", isGrounded);
        anim.SetFloat("yVelocity", rb.velocity.y);

        anim.SetBool("isWallSliding", isWallSliding);
        anim.SetBool("isWallDetected", isTouchingWall);

        anim.SetBool("isKnocked", isKnocked);
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


    public void KnockedBack(Transform damageTransform)
    {
        if (!canBeKnocked)
            return;

        isKnocked = true;
        canBeKnocked = false;

        #region Define horizontal direction for knockback
        int hDirection = 0;
        if (transform.position.x > damageTransform.position.x) //if player's x is bigger than the trap's x (player is at the right of the trap)
            hDirection = 1;
        else if (transform.position.x < damageTransform.position.x)
            hDirection = -1;
        #endregion

        rb.velocity = new Vector2(knockbackDirection.x * hDirection, knockbackDirection.y);
        Invoke("CancelKnock", 0.7f);
    }

    private void CancelKnock()
    {
        isKnocked = false;
        canBeKnocked = true;
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
        Gizmos.DrawWireSphere(enemyCheck.position, enemyCheckRadius);
    }



}
