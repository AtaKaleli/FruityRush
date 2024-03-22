using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
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

        if (isWorking)
            transform.position = Vector3.MoveTowards(transform.position, movePoints[moveSawPositionIdx].position, sawMoveSpeed * Time.deltaTime);

        if (Vector2.Distance(transform.position, movePoints[moveSawPositionIdx].position) < 0.1f)
        {
            
            cooldownTimer = cooldown;
            ChangeSawPositionIdx();

        }
    }

    private void ChangeSawPositionIdx()
    {

        moveSawPositionIdx = (1 + moveSawPositionIdx) % 2;

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<Player>() != null)
        {
            collision.transform.SetParent(transform);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() != null)
        {
            collision.transform.SetParent(null);
        }
    }

}
