using UnityEngine;

public class Enemy_Bee : Enemy
{
    [Header("Bee Info")]
    [SerializeField] private Transform[] idlePoints;
    [SerializeField] private float checkRadius;
    [SerializeField] private LayerMask whoIsPlayer;
    [SerializeField] private Transform playerCheck;

    private bool isPlayerDetected;
    private Transform player;
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
        player = GameObject.Find("Player").transform;
        defaultSpeed = speed;
    }


    // Update is called once per frame
    void Update()
    {
        idleCounter -= Time.deltaTime;
        if (idleCounter > 0)
            return;


        isPlayerDetected = Physics2D.OverlapCircle(playerCheck.position, checkRadius, whoIsPlayer);

        if (isPlayerDetected && !isAggressive)
        {
            isAggressive = true;
            speed *= 1.5f;

        }

        if (!isAggressive)
        {
            transform.position = Vector2.MoveTowards(transform.position, idlePoints[idlePointIdx].position, speed * Time.deltaTime);

            if (Vector2.Distance(transform.position, idlePoints[idlePointIdx].position) < .15f)
            {
                idlePointIdx++;
                if (idlePointIdx >= idlePoints.Length)
                    idlePointIdx = 0;
            }
        }
        else
        {


            Vector2 newPosition = new Vector2(player.transform.position.x, player.transform.position.y + yOffset);

            float xDiff = transform.position.x - player.position.x;

            transform.position = Vector2.MoveTowards(transform.position, newPosition, speed * Time.deltaTime);

            if (Mathf.Abs(xDiff) < .15f)
            {
                anim.SetTrigger("attack");

            }


        }
    }

    private void AttackEvent()
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
