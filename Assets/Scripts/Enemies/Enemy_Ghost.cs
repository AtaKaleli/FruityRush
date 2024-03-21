using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Ghost : Enemy
{

    [Header("Ghost Info")]
    [SerializeField] private float activeTime;
                     private float activeTimeCounter = 4f;

    
    private SpriteRenderer sr;

    [SerializeField] private float[] xOffset;

    protected override void Start()
    {
        base.Start();
        
        sr = GetComponent<SpriteRenderer>();

        isAggressive = true;
        
        
    }



    private void Update()
    {

        if (player == null)
        {
            anim.SetTrigger("disappear");
            return;
        }

        activeTimeCounter -= Time.deltaTime;
        idleCounter -= Time.deltaTime;


        if (activeTimeCounter > 0)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        }

        if (activeTimeCounter < 0 && idleCounter < 0 && isAggressive)
        {
            anim.SetTrigger("disappear");
            isAggressive = false;
            idleCounter = idleTime;
        }
        if (activeTimeCounter < 0 && idleCounter < 0 && !isAggressive)
        {
            ChoosePosition();
            anim.SetTrigger("appear");
            isAggressive = true;
            activeTimeCounter = activeTime;

        }

        FlipController();

    }

    private void FlipController()
    {
        if (player == null)
            return;

        if (facingDirection == -1 && transform.position.x < player.transform.position.x)
            Flip();
        else if (facingDirection == 1 && transform.position.x > player.transform.position.x)
            Flip();
    }

    private void ChoosePosition()
    {
        float randomY = Random.Range(-7, 7);
        float randomX = xOffset[Random.Range(0, xOffset.Length)];

        transform.position = new Vector2(player.transform.position.x + randomX, player.transform.position.y + randomY);
    }


    public void Disappear()
    {
        sr.color = Color.clear;
        invincible = true;
    }
    public void Appear()
    {
        sr.color = Color.white;
        invincible = false;
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        
        if(isAggressive) 
            base.OnTriggerEnter2D(collision);
    }
}
