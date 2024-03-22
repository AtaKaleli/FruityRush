using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampoline : MonoBehaviour
{
    [SerializeField] private float jumpForce;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<Player>() != null)
        {
            collision.GetComponent<Rigidbody2D>().velocity = new Vector2(0, jumpForce);
            GetComponent<Animator>().SetTrigger("active");
            
        }
    }
}
