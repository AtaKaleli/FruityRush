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

        bool playerDetection = playerDetected.collider.GetComponent<Player>() != null;
        if (idleCounter<0 && playerDetection)
        {
            idleCounter = idleTime;
            anim.SetTrigger("attack");
        }
    }


    private void AttackEvent()
    {
        GameObject newBullet = Instantiate(bulletPref, bulletOrigin.transform.position, Quaternion.identity);
        newBullet.GetComponent<Bullet>().SetupSpeed(bulletSpeed * facingDirection, 0);
    }

    
}
