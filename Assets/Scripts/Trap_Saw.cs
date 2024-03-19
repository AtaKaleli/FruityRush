using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Trap_Saw : Danger
{

    private Animator anim;
    private bool isWorking;
    [SerializeField] private Transform[] movePoints;
    [SerializeField] private float sawMoveSpeed;
    private int moveSawPositionIdx = 0;

    private float cooldownTimer;
    [SerializeField] private float cooldown;




    private void Start()
    {
        anim = GetComponent<Animator>();
        transform.position = movePoints[0].position;
    }




    private void Update()
    {
        
        cooldownTimer -= Time.deltaTime;
        isWorking = cooldownTimer < 0;
        anim.SetBool("isWorking", isWorking);

        if(isWorking)
            transform.position = Vector3.MoveTowards(transform.position, movePoints[moveSawPositionIdx].position, sawMoveSpeed * Time.deltaTime);

        if (Vector2.Distance(transform.position, movePoints[moveSawPositionIdx].position) < 0.1f)
        {
            Flip();
            cooldownTimer = cooldown;
            ChangeSawPositionIdx();
           
        }
    }

    private void ChangeSawPositionIdx()
    {
        
        moveSawPositionIdx = (1 + moveSawPositionIdx) % 2;
       
    }

    private void Flip()
    {
        transform.localScale = new Vector3(1, transform.localScale.y * -1);
    }

}
