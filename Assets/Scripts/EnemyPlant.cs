using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPlant : Enemy
{


    protected override void Start()
    {
        base.Start();
    }


    
    void Update()
    {
        CollisionChecks();
        idleCounter -= Time.deltaTime;

        if (!playerDetected)
            return;

        bool playerDetection = playerDetected.collider.GetComponent<Player>() != null;
        if (idleCounter<0 && playerDetection)
        {
            idleCounter = idleTime;
            anim.SetTrigger("attack");
        }
    }


    private void AttackEvent()
    {
        print("attack" + playerDetected.collider.name);
    }
}
