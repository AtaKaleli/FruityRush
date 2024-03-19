using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Danger : MonoBehaviour
{

    // this one responsible for giving damage to player
    protected virtual void OnTriggerEnter2D(Collider2D collision) // need to use virtual if we want to use inheritence and override the functions.
    {
        if(collision.tag == "Player")
        {
            Player player = collision.GetComponent<Player>();
            player.KnockedBack(transform);
        }
    }

}
