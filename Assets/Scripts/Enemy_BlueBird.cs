using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_BlueBird : Enemy
{

    [Header("Bluebird GroundCheck")]
    [SerializeField] private float groundCheckAboveDistance;
    [SerializeField] private float groundCheckBelowDistance;
    private bool groundAboveDetected;
    private bool groundBelowDetected;

   
    
    

    [Header("Speed Controller")]
    
    [SerializeField] private float flyingUpForce;
    [SerializeField] private float flyingDownForce;
                     private float flyingForce;

    protected override void Start()
    {
        base.Start();
        flyingForce = flyingUpForce;
    }


    private void Update()
    {
        CollisionChecks();

        if (groundAboveDetected)
            flyingForce = flyingDownForce;
        else if (groundBelowDetected)
            flyingForce = flyingUpForce;

        if (wallDetected)
            Flip();
    }

    public override void Damage()
    {
        base.Damage();
    }

    public void FlyUpEvent()
    {
        rb.velocity = new Vector2(speed* facingDirection, flyingForce);
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
