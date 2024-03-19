using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    protected Animator anim;
    protected Rigidbody2D rb;

    protected int facingDirection = -1;

    [Header("CollisionChecks")]
    [SerializeField] protected LayerMask whatIsGround;
    [SerializeField] protected float groundCheckDistance;
    [SerializeField] protected float wallCheckDistance;
    [SerializeField] protected Transform groundCheck;
    [SerializeField] protected Transform wallCheck;

    protected bool wallDetected;
    protected bool groundDetected;
    protected bool playerDetected;
    [SerializeField] protected LayerMask whatIsPlayer;

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
    }

    protected virtual void WalkAround()
    {
        idleCounter -= Time.deltaTime;
        if (idleCounter <= 0)
            rb.velocity = new Vector2(speed * facingDirection, rb.velocity.y);
        else
            rb.velocity = new Vector2(0, 0);



        if (wallDetected || !groundDetected)
        {
            idleCounter = idleTime;// make the mushroom wait one second after it flipped
            Flip();

        }
    }

    public virtual void Damage()
    {
        if(!invincible)
            anim.SetTrigger("gotHit");
    }

    public void DestroyMe()
    {
        rb.velocity = new Vector2(0, 0);
        Destroy(gameObject);
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() != null)
        {
            Player player = collision.GetComponent<Player>();
            player.KnockedBack(transform);
        }
    }

    protected virtual void Flip()
    {
        facingDirection = facingDirection * -1;
        transform.Rotate(0, 180, 0);
    }

    protected virtual void CollisionChecks()
    {
        groundDetected = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);
        wallDetected = Physics2D.Raycast(wallCheck.position, Vector2.right * facingDirection, wallCheckDistance, whatIsGround);
        playerDetected = Physics2D.Raycast(transform.position, Vector2.right * facingDirection, 100, whatIsPlayer); // this way rhino can detect player
    }

    protected virtual void OnDrawGizmos()
    {
        if(groundCheck != null)
            Gizmos.DrawLine(groundCheck.position, new Vector2(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
        if(wallCheck != null)
            Gizmos.DrawLine(wallCheck.position, new Vector2(wallCheck.position.x + wallCheckDistance * facingDirection, wallCheck.position.y));
    }

}
