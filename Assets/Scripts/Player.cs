using UnityEngine;

public class Player : MonoBehaviour
{

    
    private Rigidbody2D rb;
    private Animator anim;

    [Header("PlayerMove")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;

    private float horizontalInput;

    [Header("CollisionChecks")]
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private float groundCheckDistance;
    private bool isGrounded;
    private bool canDoubleJump=true;
    private bool isFacingRight = true;
    private int facingDirection = 1;
    


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }


    void Update()
    {
        AnimationControllers();

        CollisionChecks();


        Move();
        InputChecks();
        FlipController();

        if (isGrounded)
            canDoubleJump = true;


    }

    private void AnimationControllers()
    {
        bool isMoving = rb.velocity.x != 0;
        anim.SetBool("isMoving", isMoving);

        anim.SetBool("isGrounded", isGrounded);
        anim.SetFloat("yVelocity", rb.velocity.y);
    }

    private void InputChecks()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        


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
    }

    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x, transform.position.y - groundCheckDistance));
    }

}
