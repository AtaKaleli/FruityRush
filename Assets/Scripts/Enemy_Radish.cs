using UnityEngine;

public class Enemy_Radish : Enemy
{

    [Header("Radish GroundCheck")]
    [SerializeField] private float groundCheckAboveDistance;
    [SerializeField] private float groundCheckBelowDistance;
    private bool groundAboveDetected;
    private bool groundBelowDetected;
    private bool isAggressive;

    [SerializeField] private float aggroTime;
                     private float aggroTimeCounter;
    [SerializeField] private float flyForce;

    protected override void Start()
    {
        base.Start();
    }


    // Update is called once per frame
    void Update()
    {
        aggroTimeCounter -= Time.deltaTime;

        if (aggroTimeCounter < 0 && !groundAboveDetected)
        {
            rb.gravityScale = 1;
            isAggressive = false;
        }

        if (!isAggressive)
        {
            if (groundBelowDetected && !groundAboveDetected)
            {
                rb.velocity = new Vector2(0, flyForce);
            }
        }
        else
        {
            //if(groundBelowDetected.distance <= 1.25f)
            WalkAround();

        }

        

        CollisionChecks();

        anim.SetFloat("xVelocity", rb.velocity.x);
        anim.SetBool("isAggressive", isAggressive);
    }

    public override void Damage()
    {
        if (!isAggressive)
        {
            aggroTimeCounter = aggroTime;
            isAggressive = true;
            rb.gravityScale = 20;
        }
        else
            base.Damage();
    }

  

    protected override void CollisionChecks()
    {
        base.CollisionChecks();
        groundAboveDetected = Physics2D.Raycast(transform.position, Vector2.up, groundCheckAboveDistance, whatIsGround);
        groundBelowDetected = Physics2D.Raycast(transform.position, Vector2.down, groundCheckBelowDistance, whatIsGround);


    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.DrawLine(transform.position, new Vector2(transform.position.x, transform.position.y + groundCheckAboveDistance));
        Gizmos.DrawLine(transform.position, new Vector2(transform.position.x, transform.position.y - groundCheckBelowDistance));


    }

}
