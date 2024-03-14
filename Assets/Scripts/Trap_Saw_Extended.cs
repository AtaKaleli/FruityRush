using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap_Saw_Extended : Trap
{
    private Animator anim;
    private bool isWorking;
    [SerializeField] private Transform[] movePoints;
    [SerializeField] private float sawMoveSpeed;
    private int moveSawPositionIdx = 0;
    private bool goingForward = true;
   




    private void Start()
    {
        anim = GetComponent<Animator>();
        transform.position = movePoints[0].position;
    }




    private void Update()
    {

        
        anim.SetBool("isWorking", true);

        
        transform.position = Vector3.MoveTowards(transform.position, movePoints[moveSawPositionIdx].position, sawMoveSpeed * Time.deltaTime);

        if (Vector2.Distance(transform.position, movePoints[moveSawPositionIdx].position) < 0.15f)
        {

            if (goingForward)
                moveSawPositionIdx++;
            else
                moveSawPositionIdx--;
            
            if(moveSawPositionIdx >= movePoints.Length)
            {
                moveSawPositionIdx = movePoints.Length - 1;
                goingForward = false;
                Flip();
            }
            
            if(moveSawPositionIdx<0)
            {
                moveSawPositionIdx = 0;
                goingForward = true;
            }
                

            ChangeSawPosition();


        }
    }

    private void ChangeSawPosition()
    {
        
        moveSawPositionIdx = (moveSawPositionIdx) % movePoints.Length;

    }

    


    private void Flip()
    {
        transform.localScale = new Vector3(1, transform.localScale.y * -1);
    }
}
