using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Bat : Enemy
{

    [Header("Bat Info")]
    [SerializeField] private Transform[] idlePoints; // points that bat will go in its idle time

    private Vector2 destination;
    private bool canBeAggressive = true;
    private bool isPlayerDetected;
    private Transform player;

    [Header("Player Detection")] // player detection 
    [SerializeField] private float checkRadius;
    [SerializeField] private LayerMask whoIsPlayer;

    private float defaultSpeed;

    

    protected override void Start()
    {
        base.Start();
        player = GameObject.Find("Player").transform;
        defaultSpeed = speed;
        destination = idlePoints[0].position;
    }

    void Update()
    {

        idleCounter -= Time.deltaTime; // if bat is on its idle time, then return, do nothing
        if (idleCounter > 0)
            return;


        isPlayerDetected = Physics2D.OverlapCircle(transform.position, checkRadius, whoIsPlayer); // detect player using phsicss2d.overlapCircle

        if(isPlayerDetected && !isAggressive && canBeAggressive) // if playerdetected, and bat is not aggressive and canBeAggressive
        {
            isAggressive = true; // set isAggressive to true
            canBeAggressive = false; // set canBeAggressive to false
            destination = player.transform.position; // and set the destination to the players last position
        }

        if (isAggressive) // if bat is aggressive
        {
            transform.position = Vector2.MoveTowards(transform.position, destination, speed * Time.deltaTime); // change its position from initial pos. to destination.

            if(Vector2.Distance(transform.position, destination) < .1f) // if the distance between its position and destination is very small
            {
                isAggressive = false; // set isaggressive to false
                int i = Random.Range(0, idlePoints.Length);

                destination = idlePoints[i].position; // then set its destination to a random idle point
                speed *= .5f; // make it slow when go to idlePoint
            }
        }

        else // if not aggressive
        {
            transform.position = Vector2.MoveTowards(transform.position, destination, speed * Time.deltaTime);

            if(Vector2.Distance(transform.position,destination) < .1f)
            {
                if (!canBeAggressive) // if can not be aggressive, then set the idle counter to idleTime
                    idleCounter = idleTime;
                canBeAggressive = true;
                speed = defaultSpeed;
            }
        }

        anim.SetBool("canBeAggressive", canBeAggressive);
        anim.SetFloat("speed", speed);
        FlipController();

    }

    private void FlipController() //flip controller
    {
        if (facingDirection == -1 && transform.position.x < destination.x)
            Flip();
        else if (facingDirection == 1 && transform.position.x > destination.x)
            Flip();
    }

    protected override void OnDrawGizmos() // gizmoss
    {
        base.OnDrawGizmos();
        Gizmos.DrawWireSphere(transform.position, checkRadius);
    }

    
}
