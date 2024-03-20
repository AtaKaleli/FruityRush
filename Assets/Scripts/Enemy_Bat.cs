using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Bat : Enemy
{

    [Header("Bat Info")]
    [SerializeField] private Transform[] idlePoints;

    private Vector2 destination;
    private bool canBeAggressive = true;
    private bool isPlayerDetected;
    private Transform player;

    [Header("Player Detection")]
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

        idleCounter -= Time.deltaTime;
        if (idleCounter > 0)
            return;


        isPlayerDetected = Physics2D.OverlapCircle(transform.position, checkRadius, whoIsPlayer);

        if(isPlayerDetected && !isAggressive && canBeAggressive)
        {
            isAggressive = true;
            canBeAggressive = false;
            destination = player.transform.position;
        }

        if (isAggressive)
        {
            transform.position = Vector2.MoveTowards(transform.position, destination, speed * Time.deltaTime);

            if(Vector2.Distance(transform.position, destination) < .1f)
            {
                isAggressive = false;
                int i = Random.Range(0, idlePoints.Length);

                destination = idlePoints[i].position;
                speed *= .5f;
            }
        }

        else
        {
            transform.position = Vector2.MoveTowards(transform.position, destination, speed * Time.deltaTime);

            if(Vector2.Distance(transform.position,destination) < .1f)
            {
                if (!canBeAggressive)
                    idleCounter = idleTime;
                canBeAggressive = true;
                speed = defaultSpeed;
            }
        }

        anim.SetBool("canBeAggressive", canBeAggressive);
        anim.SetFloat("speed", speed);
        FlipController();

    }

    private void FlipController()
    {
        if (facingDirection == -1 && transform.position.x < destination.x)
            Flip();
        else if (facingDirection == 1 && transform.position.x > destination.x)
            Flip();
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.DrawWireSphere(transform.position, checkRadius);
    }

    
}
