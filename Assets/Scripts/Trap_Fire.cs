using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap_Fire : Trap
{
    
    public bool isWorking;
    private Animator anim;
    public float repeatRate;

    private void Start()
    {
        anim = GetComponent<Animator>();

        if(transform.parent == null)
            InvokeRepeating("FireSwitcher", 0, repeatRate);
    }

    private void Update()
    {
        anim.SetBool("isWorking", isWorking);
    }

    public void FireSwitcher()
    {
        isWorking = !isWorking;
    }

    public void FireSwitcherAfter(float seconds)
    {
        CancelInvoke(); // cancels all invokes
        isWorking = false;
        Invoke("FireSwitcher", seconds);
    }


    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if(isWorking)
            base.OnTriggerEnter2D(collision);
    }
}
