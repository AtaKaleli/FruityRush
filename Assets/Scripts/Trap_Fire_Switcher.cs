using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap_Fire_Switcher : MonoBehaviour
{


    public Trap_Fire trapFire;
    private Animator anim;
    [SerializeField] private float fireTimer;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            anim.SetTrigger("pressed");
            trapFire.FireSwitcherAfter(fireTimer);

        }
    }
}
