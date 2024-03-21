using UnityEngine;

public class Enemy_Bee : Enemy
{
    [Header("Bee Info")]
    [SerializeField] private Transform[] idlePoints;
    [SerializeField] private float checkRadius;
    [SerializeField] private LayerMask whoIsPlayer;
    [SerializeField] private Transform playerCheck;

    private bool isPlayerDetected;
    
    private float defaultSpeed;

    [SerializeField] private float yOffset;

    [Header("Bullet Info")]
    [SerializeField] private GameObject bulletPref;
    [SerializeField] private Transform bulletOrigin;
    [SerializeField] private float bulletSpeed;
    private int idlePointIdx = 0;

    protected override void Start()
    {
        base.Start();
        
        defaultSpeed = speed;
    }


    // Update is called once per frame
    void Update()
    {


        idleCounter -= Time.deltaTime; //if the bee is on the idle counter, it will not do anything, just stays in the idle.
        if (idleCounter > 0)
            return;

        if (player == null)
            return;

        isPlayerDetected = Physics2D.OverlapCircle(playerCheck.position, checkRadius, whoIsPlayer); // the area where player is detected

        if (isPlayerDetected && !isAggressive) // if player is detected and the bee is not aggressive, set isaggressive to true, and make it faster
        {
            isAggressive = true;
            speed *= 1.5f;

        }

        if (!isAggressive) // if the bee is not aggressive and player is not detected, then move between idle points
        {
            transform.position = Vector2.MoveTowards(transform.position, idlePoints[idlePointIdx].position, speed * Time.deltaTime);//each time, move to new idle point

            if (Vector2.Distance(transform.position, idlePoints[idlePointIdx].position) < .15f) // make sure that bee moves idle points that is closer to him
            {
                idlePointIdx++;
                if (idlePointIdx >= idlePoints.Length)
                    idlePointIdx = 0;
            }
        }
        else
        {


            Vector2 newPosition = new Vector2(player.transform.position.x, player.transform.position.y + yOffset);


            transform.position = Vector2.MoveTowards(transform.position, newPosition, speed * Time.deltaTime);

            float xDiff = transform.position.x - player.position.x; // when the bee and player x is less than .15f in abs, then bee shoots
            if (Mathf.Abs(xDiff) < .15f)
            {
                anim.SetTrigger("attack");

            }


        }
    }

    private void AttackEvent() // attack event of bee, we created new bullet with origin, then shoot, after shoot, make idlecounter set to idle time and aggro to false
    {
        GameObject newBullet = Instantiate(bulletPref, bulletOrigin.transform.position, Quaternion.identity);
        newBullet.GetComponent<Bullet>().SetupSpeed(0, -bulletSpeed);

        idleCounter = idleTime;
        isAggressive = false;
        speed = defaultSpeed;

    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.DrawWireSphere(playerCheck.position, checkRadius);
    }

}
