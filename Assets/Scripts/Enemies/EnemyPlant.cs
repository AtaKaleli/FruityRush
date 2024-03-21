using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPlant : Enemy
{

    [Header("Plant Bullet Info")]
    [SerializeField] private GameObject bulletPref; // this is what plant will instantiate
    [SerializeField] private Transform bulletOrigin; // this is where the the origin of the bullet
    [SerializeField] private float bulletSpeed;
    [SerializeField] private bool facingRight;

    protected override void Start()
    {
        base.Start();

        if (facingRight)
            Flip();
        
    }


    
    void Update()
    {
        CollisionChecks();
        idleCounter -= Time.deltaTime;

        if (!playerDetected)
            return;

        bool playerDetection = playerDetected.collider.GetComponent<Player>() != null; // set true or false based on the player detection
       

        if (idleCounter<0 && playerDetection) // if idlecounter is less than 0 and playerdetected, then attack to player and set the idleCounter to idleTime
        {
            idleCounter = idleTime;
            anim.SetTrigger("attack");
        }
    }


    private void AttackEvent() // this event is called in the attack animation of plant
    {
        GameObject newBullet = Instantiate(bulletPref, bulletOrigin.transform.position, Quaternion.identity);
        newBullet.GetComponent<Bullet>().SetupSpeed(bulletSpeed * facingDirection, 0);
    }

    
}
