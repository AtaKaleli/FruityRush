using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Rhino : Enemy
{

    

    [Header("Aggro Info")]
    [SerializeField] private float aggroSpeed;
    [SerializeField] private LayerMask whatIsPlayer;
    private bool playerDetected;
    private bool isAggressive;

    [Header("ShockTimer")]
    [SerializeField] private float shockTime;
                     private float shockTimeCounter;

    

    protected override void Start()
    {
        base.Start();
        invincible = true;
    }

    


    // Update is called once per frame
    void Update()
    {

        playerDetected = Physics2D.Raycast(transform.position, Vector2.right * facingDirection, 100, whatIsPlayer); // this way rhino can detect player
        if (playerDetected)
            isAggressive = true;

        if (!isAggressive)
        {
            WalkAround();
        }
        else
        {
            rb.velocity = new Vector2(aggroSpeed*facingDirection,rb.velocity.y);

            if (wallDetected && invincible)
            {
                invincible = false;
                shockTimeCounter = shockTime;
            }

            if(shockTimeCounter <= 0 && !invincible)
            {
                invincible = true;
                Flip();
                isAggressive = false;
            }
        }


        shockTimeCounter -= Time.deltaTime;
        CollisionChecks();
        anim.SetFloat("xVelocity", rb.velocity.x);
        anim.SetBool("invincible", invincible);

       
    }


    protected override void OnDrawGizmos() // gizmos jsut draws a line, the aggro mode is based on the phy2D.raycast function
    {
        base.OnDrawGizmos();
        //Gizmos.DrawLine(wallCheck.position, new Vector2(wallCheck.position.x + 15 * facingDirection, wallCheck.position.y));
    }
}
