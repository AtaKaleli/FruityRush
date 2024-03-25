using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit_DropByPlayer : Fruit_Item
{
    [SerializeField] private Vector2 speed;
    [SerializeField] private Color transparentColor;
    private bool canPickUp;
    private int speedMultiplier;


    private void Start()
    {
        if (GameManager.instance.levelDifficulty == 2)
            speedMultiplier = 1;
        else if (GameManager.instance.levelDifficulty == 3)
            speedMultiplier = 2;
        StartCoroutine(BlinkImage());
    }

    private void Update()
    {
        transform.position += new Vector3(speed.x, speed.y) * Time.deltaTime;
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if(canPickUp)
            base.OnTriggerEnter2D(collision);
    }

    private IEnumerator BlinkImage()
    {

        anim.speed = 0;
        sr.color = transparentColor;
        speed.y = speedMultiplier;

        speed.x = -speedMultiplier;
        yield return new WaitForSeconds(.5f);
        sr.color = Color.white;
        speed.x = speedMultiplier;
        yield return new WaitForSeconds(.5f);
        sr.color = transparentColor;
        speed.x = -speedMultiplier;
        yield return new WaitForSeconds(.5f);
        sr.color = Color.white;
        speed.x = speedMultiplier;
        yield return new WaitForSeconds(.5f);
        sr.color = transparentColor;
        speed.x = -speedMultiplier;
        yield return new WaitForSeconds(.4f);
        sr.color = Color.white;
        speed.x = speedMultiplier;
        yield return new WaitForSeconds(.4f);
        sr.color = transparentColor;
        speed.x = -speedMultiplier;
        yield return new WaitForSeconds(.2f);
        sr.color = Color.white;
        speed.x = speedMultiplier;
        yield return new WaitForSeconds(.2f);
        sr.color = transparentColor;
        speed.x = -speedMultiplier;
        yield return new WaitForSeconds(.2f);
        sr.color = Color.white;
        speed.x = speedMultiplier;
        yield return new WaitForSeconds(.2f);
        sr.color = transparentColor;
        speed.x = -speedMultiplier;
        yield return new WaitForSeconds(.3f);
        sr.color = Color.white;


        speed.x = 0;
        speed.y = 1;
        anim.speed = 1;
        canPickUp = true;
    }
}
