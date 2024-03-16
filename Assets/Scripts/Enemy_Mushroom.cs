using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Mushroom : Enemy
{
    [Header("Move Info")]
    [SerializeField] private float speed;
    [SerializeField] private float idleTime;
                     private float idleCounter;

    private bool isMoving;


    protected override void Start()
    {
        base.Start();
    }

    private void Update()
    {
        idleCounter -= Time.deltaTime;

        if (idleCounter <= 0)
        {
            isMoving = true;
            rb.velocity = new Vector2(speed * facingDirection, rb.velocity.y);
        }

        CollisionChecks();

        if (wallDetected || !groundDetected)
        {
            idleCounter = idleTime;// make the mushroom wait one second after it flipped
            Flip();
            isMoving = false;
        }

        anim.SetBool("isMoving", isMoving);

    }
}
