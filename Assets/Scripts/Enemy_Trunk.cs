using UnityEngine;

public class Enemy_Trunk : Enemy
{


    [Header("Trunk Bullet Info")]
    [SerializeField] private GameObject bulletPref; // this is what plant will instantiate
    [SerializeField] private Transform bulletOrigin; // this is where the the origin of the bullet
    [SerializeField] private float bulletSpeed; // speed of the bullet

    [Header("Trunk Shoot Info")]
    [SerializeField] private float shootTime;

    private float shootingCounter;
    [SerializeField] private float moveTime;
    private float moveTimeCounter;

    [Header("Trunk Collision Info")]
    [SerializeField] private Transform groundBehindCheck;
    private bool wallBehind;
    private bool groundBehind;

    [Header("Player Detection")] // player detection 
    [SerializeField] private float checkRadius;
    [SerializeField] private LayerMask whoIsPlayer;
    private bool isPlayerDetected;

    [SerializeField] private float retreatTime;
    private float retreatTimeCounter;
    private Transform player;

    protected override void Start()
    {
        base.Start();
        player = GameObject.Find("Player").transform;

    }



    void Update()
    {

        shootingCounter -= Time.deltaTime;
        moveTimeCounter -= Time.deltaTime;
        retreatTimeCounter -= Time.deltaTime;


        CollisionChecks();
        AnimationControllers();

        if (isPlayerDetected)
            moveTimeCounter = moveTime;

        if (playerDetected)
        {
            

            if (shootingCounter < 0)
            {
                shootingCounter = shootTime;
                anim.SetTrigger("isShooting");
                moveTimeCounter = moveTime;

            }
            else
            {
                MoveBackwards(1.5f);
            }
            


        }
        else
        {
            
                if (moveTimeCounter > 0)
                {

                    MoveBackwards(4f);

                }


                else
                {
                    WalkAround();
                }
            
            

        }


        

    }

    private void MoveBackwards(float multiplier)
    {
        if (wallBehind)
            return;
        if (!groundBehind)
            return;

        
        rb.velocity = new Vector2(speed*multiplier*-facingDirection,rb.velocity.y);
        


    }


    private void ShootEvent()
    {
        GameObject newBullet = Instantiate(bulletPref, bulletOrigin.position, Quaternion.identity);
        newBullet.GetComponent<Bullet>().SetupSpeed(bulletSpeed * facingDirection, 0);
    }




    private void AnimationControllers()
    {
        anim.SetFloat("xVelocity", rb.velocity.x);

    }

    protected override void CollisionChecks()
    {
        base.CollisionChecks();
        
        groundBehind = Physics2D.Raycast(groundBehindCheck.position, Vector2.down * facingDirection, groundCheckDistance, whatIsGround);
        wallBehind = Physics2D.Raycast(wallCheck.position, Vector2.right * (-facingDirection + 1), wallCheckDistance, whatIsGround); // this way rhino can detect player
        isPlayerDetected = Physics2D.OverlapCircle(transform.position, checkRadius, whoIsPlayer); // detect player using phsicss2d.overlapCircle
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.DrawWireSphere(transform.position, checkRadius);

        Gizmos.DrawLine(groundBehindCheck.position, new Vector2(groundBehindCheck.position.x, groundBehindCheck.position.y - groundCheckDistance));

    }
}
