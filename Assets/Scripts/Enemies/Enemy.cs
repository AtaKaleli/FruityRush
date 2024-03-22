using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Danger
{

    protected Animator anim;
    protected Rigidbody2D rb;
    protected Transform player;

    protected int facingDirection = -1; // facing direction of enemies, 1 means face right, -1 means face left

    [Header("CollisionChecks")]
    [SerializeField] protected LayerMask whatIsGround;
    [SerializeField] protected float groundCheckDistance;
    [SerializeField] protected float wallCheckDistance;
    [SerializeField] protected Transform groundCheck;
    [SerializeField] protected Transform wallCheck;

    [Header("CollisionDetections")]
    [SerializeField] private LayerMask whatIsPlayer;
    [SerializeField] private float playerDetectionDistance;
    protected bool wallDetected;
    protected bool groundDetected;
    protected RaycastHit2D playerDetected;


    //hideInInspector hides the variable in the inspector
    [HideInInspector]public bool invincible; // make enemy invincible so that player cant kill it when true

    [Header("Move Info")]
    [SerializeField] protected float speed;
    [SerializeField] protected float idleTime;
    protected float idleCounter;
    protected bool isAggressive;

    protected virtual void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        if(PlayerManager.instance != null)
            player = PlayerManager.instance.currentPlayer.transform;

        if (groundCheck == null)
            groundCheck = transform;
        if (wallCheck == null)
            wallCheck = transform;
    }

    protected virtual void WalkAround()
    {
        idleCounter -= Time.deltaTime;
        if (idleCounter <= 0)
            rb.velocity = new Vector2(speed * facingDirection, rb.velocity.y);
        else
            rb.velocity = new Vector2(0, 0);



        if (wallDetected || !groundDetected) // if wall detected, or ground is not detected, then go to idleTime
        {
            idleCounter = idleTime;// make the enemy  wait idleTime seconds then  flip it
            Flip();

        }
    }

    public virtual void Damage() // if enemy is not invincible, then this means player can damage the enemy
    {
        if(!invincible)
            anim.SetTrigger("gotHit");
    }

    public void DestroyMe() // If enemy is destroyed, set the velocity of it to 0 then destroy it. Set this function at the end of the hit animations of enemies.
    {
        rb.velocity = new Vector2(0, 0);
        Destroy(gameObject);
    }

    

    protected virtual void Flip() // basically flips the enemy. Each time it is called, it rotates the enemy using transform.Rotate function
    {
        facingDirection = facingDirection * -1;
        transform.Rotate(0, 180, 0);
    }

    protected virtual void CollisionChecks() // checks collision for enemies.
    {
        groundDetected = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);
        wallDetected = Physics2D.Raycast(wallCheck.position, Vector2.right * facingDirection, wallCheckDistance, whatIsGround);
        playerDetected = Physics2D.Raycast(wallCheck.position, Vector2.right * facingDirection, playerDetectionDistance, whatIsPlayer); // this way rhino can detect player
    }

    protected virtual void OnDrawGizmos() // draes gizmos lines for better vision
    {
        if(groundCheck != null)
            Gizmos.DrawLine(groundCheck.position, new Vector2(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
        if(wallCheck != null)
        {
            Gizmos.DrawLine(wallCheck.position, new Vector2(wallCheck.position.x + wallCheckDistance * facingDirection, wallCheck.position.y));
            Gizmos.DrawLine(wallCheck.position, new Vector2(wallCheck.position.x + playerDetected.distance * facingDirection, wallCheck.position.y));

        }

    }

}
