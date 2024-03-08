using UnityEngine;

public class Player : MonoBehaviour
{


    private Rigidbody2D rb;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;

    private float horizontalInput;

    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private float groundCheckDistance;
    private bool isGrounded;
    private bool canDoubleJump=true;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        CollisionChecks();


        Move();
        InputChecks();

        if (isGrounded)
            canDoubleJump = true;


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
