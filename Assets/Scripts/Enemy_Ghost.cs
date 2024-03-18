using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Ghost : Enemy
{

    [Header("Ghost Info")]
    [SerializeField] private float activeTime;
                     private float activeTimeCounter = 4f;

    private Transform player;

    protected override void Start()
    {
        base.Start();
        player = GameObject.Find("Player").transform;
        
    }



    private void Update()
    {
        activeTimeCounter -= Time.deltaTime;
        idleCounter -= Time.deltaTime;


        if(activeTimeCounter > 0)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        }

        if(activeTimeCounter<0 && idleCounter < 0 && isAggressive)
        {
            anim.SetTrigger("disappear");
            isAggressive = false;
            idleCounter = idleTime;
        }
        if (activeTimeCounter < 0 && idleCounter < 0 && !isAggressive)
        {
            anim.SetTrigger("appear");
            isAggressive = true;
            activeTimeCounter = activeTime;
            
        }




    }

    public override void Damage()
    {
        base.Damage();
    }


}
