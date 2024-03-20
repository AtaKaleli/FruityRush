using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : Danger // bullet of enemies that can shoot to player
{

    private Rigidbody2D rb;
    private float xSpeed;
    private float ySpeed;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(xSpeed, ySpeed); // based on the speed to the x and y, set the velocity to bullet

    }

    

    public void SetupSpeed(float x, float y) // setup the speed of the bullet from different scripts
    {
        xSpeed = x;
        ySpeed = y;
    }

    protected override void OnTriggerEnter2D(Collider2D collision) // if bullet hit anything, then destroy it so it wont go to infinity.
    {
        base.OnTriggerEnter2D(collision);
        Destroy(gameObject);
    }
}
