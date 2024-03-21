using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Trunk : Enemy
{


    [Header("Trunk Bullet Info")]
    [SerializeField] private GameObject bulletPref; // this is what plant will instantiate
    [SerializeField] private Transform bulletOrigin; // this is where the the origin of the bullet
    [SerializeField] private float bulletSpeed; // speed of the bullet

    private bool isShooting;


    protected override void Start()
    {
        base.Start();
        
    }

    // Update is called once per frame
    void Update()
    {

        

        if(playerDetected)
        {
            isShooting = true;
            
        }
        else
        {
            
            isShooting = false;
            WalkAround();

            
        }


        CollisionChecks();
        AnimationControllers();

    }

    private void ShootEvent()
    {
        GameObject newBullet = Instantiate(bulletPref, bulletOrigin.position, Quaternion.identity);
        newBullet.GetComponent<Bullet>().SetupSpeed(bulletSpeed * facingDirection, 0);
    }




    private void AnimationControllers()
    {
        anim.SetFloat("xVelocity", rb.velocity.x);
        anim.SetBool("isShooting", isShooting);
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
    }
}
