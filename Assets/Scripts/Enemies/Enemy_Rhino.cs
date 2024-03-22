using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Rhino : Enemy
{

    

    [Header("Aggro Info")]
    [SerializeField] private float aggroSpeed;
    
    
    

    [Header("ShockTimer")]
    [SerializeField] private float shockTime;
                     private float shockTimeCounter;

    

    protected override void Start()
    {
        base.Start();
        invincible = true; // set rhino invincle at the beginning
    }

    


    // Update is called once per frame
    void Update()
    {


        
        
        if (playerDetected) // if rhino detects player, then make him aggressive
            isAggressive = true;

        if (!isAggressive) // if he is not aggressive, then walk him around
        {
            WalkAround();
        }
        else // if aggressive, then change the velocity of him , make him faster
        {
            rb.velocity = new Vector2(aggroSpeed*facingDirection,rb.velocity.y);

            if (wallDetected && invincible) // if walldetected (rhino hits wall) and invincible, then make him shock, and remove invincible
            {
                invincible = false;
                shockTimeCounter = shockTime;
            }

            if(shockTimeCounter <= 0 && !invincible) // if shock timer runs out to negative and is not invincible
            { 
                // set the invincible back again and Flip him , then set its aggro to false
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
