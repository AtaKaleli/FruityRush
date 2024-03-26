using UnityEngine;

public class Player : MonoBehaviour
{

    

    private Rigidbody2D rb;
    private Animator anim;

    [Header("ParticleFX")]
    [SerializeField] private ParticleSystem dustFX;



    [Header("PlayerMove")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float doubleJumpForce;
    [SerializeField] private float bufferJumpTime;// make player jump even s/he did not touched grounded yet for a specific time before
    private float bufferJumpCounter;
    [SerializeField] private float cayoteJumpTime;//allow jump for a few sec even if player on air
    private float cayoteJumpCounter;
    private bool canHaveCayoteJump;

    private float defaultGravityScale;
    private float defaultJumpForce;
    private bool canMove;


    private float horizontalInput;
    private float verticalInput;

    [Header("GroundCollisionChecks")]
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private LayerMask whatIsWall;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private Transform enemyCheck;
    [SerializeField] private float enemyCheckRadius;

    private bool flipping;
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
    [SerializeField] private float knockBackProtectionTime;

    private bool isKnocked;
    private bool canBeKnocked = true;
    private bool canBeControlled;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        ChangePlayerSkin();

        defaultJumpForce = jumpForce; // default jump force equals to jump force.
        defaultGravityScale = rb.gravityScale;
        rb.gravityScale = 0;
    }


    void Update()
    {
        AnimationControllers();
        if (isKnocked) // if player is knocked, which means takes hit from enemy, then return, so nothing will happen
            return;

        CollisionChecks();

        bufferJumpCounter -= Time.deltaTime;
        cayoteJumpCounter -= Time.deltaTime;

        InputChecks();
        FlipController();
        CheckForEnemy();

        if (isGrounded) // if player is on the ground, then he can move and has doubleJump to use.
        {
            canMove = true;
            canDoubleJump = true;

            if (bufferJumpCounter > 0)
            {
                bufferJumpCounter = -1;
                Jump();
            }
            canHaveCayoteJump = true;
        }
        else //if not, then he can do cayote jump
        {
            if (canHaveCayoteJump)
            {

                canHaveCayoteJump = false;
                cayoteJumpCounter = cayoteJumpTime;
            }
        }

        if (canWallSlide) // player can wall slide on the wall with a smaller velocity
        {
            isWallSliding = true;
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.1f);
        }






        Move();



    }

    private void CheckForEnemy() // this function checks for enemies
    {
        // create a collider2d array, which takes enemyCheck as a transform, and enemyCheckRadius as a radius
        // then in the inspector, place this circle to feet of the player, so that he can kill enemies by jumping their head
        // circle is visualized by gizmos in the gizmoz func

        Collider2D[] hitedColliders = Physics2D.OverlapCircleAll(enemyCheck.position, enemyCheckRadius);

        //look for each collider, if the collider itself has an enemy script, then this means we collided with an enemy
        foreach (var enemy in hitedColliders)
        {
            if (enemy.GetComponent<Enemy>() != null)
            {

                Enemy newEnemy = enemy.GetComponent<Enemy>(); // temp enemy that can be used if enemy is found


                if (newEnemy.invincible)
                    return; //if enemy is invincible, then cant make damage to it

                if (rb.velocity.y < 0) //kill enemy only if we are falling and enemy is not invincible
                {
                    
                    anim.SetBool("flipping", true);
                    newEnemy.Damage(); // call the damage function of the enemy, which triggers the gotHit animation 
                    //after gotHit animations ends, the destroyMe func will be called by addition of event to the end of hit animation

                    //after killing an enemy, make player jump.
                    Jump();
                }

            }
        }
    }

    public void SetFlippingFalse()
    {
        anim.SetBool("flipping", false);
    }

    private void AnimationControllers() // control the animations of player
    {
        bool isMoving = rb.velocity.x != 0;
        anim.SetBool("isMoving", isMoving);

        anim.SetBool("isGrounded", isGrounded);
        anim.SetFloat("yVelocity", rb.velocity.y);

        anim.SetBool("isWallSliding", isWallSliding);
        anim.SetBool("isWallDetected", isTouchingWall);

        anim.SetBool("isKnocked", isKnocked);

        anim.SetBool("canBeControlled", canBeControlled);
    }
    

    private void ChangePlayerSkin()
    {
        for (int i = 0; i < anim.layerCount; i++)
        {
            anim.SetLayerWeight(i, 0); // set the weight of each layer to 0
        }

        anim.SetLayerWeight(PlayerManager.instance.chosenSkinID, 1); // set chosen fruit type layer weight to 1
    }

    public void MakeControlled()
    {
        rb.gravityScale = defaultGravityScale;
        canBeControlled = true;
    }

    private void InputChecks() // take the x and y dir. inputs
    {

        if (!canBeControlled)
            return;

        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        if (verticalInput < 0) // if player pressed S , which means go down, then the wall sliding is interrupted
            canWallSlide = false;



        if (Input.GetKeyDown(KeyCode.Space)) // if keycode is space, then jump
        {
            JumpButton();
        }
    }

    private void JumpButton()
    {

        if (!isGrounded) // if player is not grounded, then set a bufferJump counter so player has this amount of time to make a jump
        {
            bufferJumpCounter = bufferJumpTime;
        }

        if (isWallSliding) // if player makes a wall slide, then call WallJump and allow them to make a doubleJump
        {
            WallJump();
            canDoubleJump = true;
        }

        else if (isGrounded || cayoteJumpCounter > 0) // if player on the ground or cayoteJump is grater then 0, then he can jump
            Jump();

        else if (canDoubleJump) // allow player to make double jump
        {
            canMove = true;
            canDoubleJump = false; // set the doubleJump to false after enteting this functions
            jumpForce = doubleJumpForce; // set jump for to doubleJump force
            Jump(); // then jump
            jumpForce = defaultJumpForce; // and then set it to default jumpForce
        }

        canWallSlide = false;
    }


    public void KnockedBack(Transform damageTransform)
    {
        if (!canBeKnocked) // If player cant be knocked, then return
            return;

        if (PlayerManager.instance.collectedFruits > 0 || GameManager.instance.levelDifficulty == 1)
            PlayerManager.instance.ScreenShake(-facingDirection);

        if (GameManager.instance.levelDifficulty > 1)
        {
            PlayerManager.instance.OnTakingDamage();
        }

        
        isKnocked = true; //after entering this fucntion, make isKnocked true and canBeKnocked false
        canBeKnocked = false;

        //those are the knock back directions for player after they got damage 
        #region Define horizontal direction for knockback 
        int hDirection = 0;
        if (transform.position.x > damageTransform.position.x) //if player's x is bigger than the trap's x (player is at the right of the trap)
            hDirection = 1;
        else if (transform.position.x < damageTransform.position.x)
            hDirection = -1;
        #endregion

        rb.velocity = new Vector2(knockbackDirection.x * hDirection, knockbackDirection.y); // set the new position of the player based on knockback direction
        Invoke("CancelKnock", 0.7f); // after a certain amount of time, make isKnocked false
        Invoke("AllowKnockBack", knockBackProtectionTime); // after a certan amount of protection time, make player can be knockable.so player is not knocked again and again
    }

    private void CancelKnock()
    {
        isKnocked = false;

    }
    private void AllowKnockBack()
    {
        canBeKnocked = true;
    }


    private void WallJump()// when player making wall jump, do not allow he to move
    {
        dustFX.Play();
        canMove = false;
        rb.velocity = new Vector2(5 * -facingDirection, jumpForce);// set the velocity of wall jump
    }

    private void Move() // player move function
    {

        if (canMove)
            rb.velocity = new Vector2(moveSpeed * horizontalInput, rb.velocity.y);

    }

    private void Flip() // flip the player. change the facing direction every time we get in this func. also rotate the player by 180 deg.
    {
        dustFX.Play();
        facingDirection = facingDirection * -1;
        isFacingRight = !isFacingRight;
        transform.Rotate(0, 180, 0);
    }

    private void FlipController() //controls the flip of the player
    {
        
        if (isFacingRight && rb.velocity.x < 0)
            Flip();
        else if (!isFacingRight && rb.velocity.x > 0)
            Flip();
    }


    private void CollisionChecks()
    {
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, whatIsGround); // raycasts the ground so we can get player is on the ground
        isTouchingWall = Physics2D.Raycast(transform.position, Vector2.right * facingDirection, wallCheckDistance, whatIsWall);// raycasts the ground so we can get player is touching to wall

        if (isTouchingWall && rb.velocity.y < 0) // if player is touching wall and he is going down, then allow he to do wallSlide
        {
            canWallSlide = true;
        }

        if (!isTouchingWall) // is he is not touching wall, can set the wall slide parameters to false
        {
            isWallSliding = false;
            canWallSlide = false;
        }
    }

    private void Jump() // simple jump function
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        if (isGrounded)
            dustFX.Play();

        
    }

    private void OnDrawGizmos() // draws gizmos lines for better visualization
    {
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x, transform.position.y - groundCheckDistance));
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x + wallCheckDistance * facingDirection, transform.position.y));
        Gizmos.DrawWireSphere(enemyCheck.position, enemyCheckRadius);
    }



}
