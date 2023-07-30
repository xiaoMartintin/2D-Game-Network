using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class EnemySnake : Enemy
{
    public float speed;
    public float waitTime;
    public Transform[] moveSpots;

    private float bloodInterval;
    private float bloodIntervalConst = 1.0f;

    private int i = 0;
    private bool movingRight = true;
    private float wait;


    // Use this for initialization
    public new void Start()
    {
        base.Start();
        wait = waitTime;
    }

    // Update is called once per frame
    public new void Update()
    {
        base.Update();
        if (isServer)
        {
            Patrol();
        }


        bloodInterval -= Time.deltaTime;

        if (bloodInterval <= 0)
        {
            bloodInterval = bloodIntervalConst;
            Instantiate(bloodEffect, transform.position, Quaternion.identity);
        }


    }

    [ClientRpc]
    void Patrol()
    {
        transform.position =
            Vector2.MoveTowards(transform.position, moveSpots[i].position, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, moveSpots[i].position) < 0.1f)
        {

            if (waitTime <= 0)
            {
                if (movingRight == true)
                {
                    transform.eulerAngles = new Vector3(0, -180, 0);
                    movingRight = false;
                }
                else
                {
                    transform.eulerAngles = new Vector3(0, 0, 0);
                    movingRight = true;
                }

                if (i == 0)
                {

                    i = 1;
                }
                else
                {

                    i = 0;
                }

                waitTime = wait;
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }
    }

}
