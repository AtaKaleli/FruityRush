using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{


    protected virtual void OnTriggerEnter2D(Collider2D collision) // need to use virtual if we want to use inheritence and override the functions.
    {
        if(collision.tag == "Player")
        {
            print("Knocked!");
        }
    }

}
