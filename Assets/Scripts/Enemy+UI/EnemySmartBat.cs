using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class EnemySmartBat : Enemy
{
    public float speed;
    public float radius;

    private Transform playerTransform;

    // Start is called before the first frame update
    public new void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    public new void Update()
    {
        //调用父类的Update()方法
        base.Update();
        if(isServer)
            Chase();
        
    }

    [ClientRpc]
    void Chase()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        if (playerTransform != null)
        {
            float distance = (transform.position - playerTransform.position).sqrMagnitude;

            if (distance < radius)
            {
                transform.position = Vector2.MoveTowards(transform.position,
                                                        playerTransform.position,
                                                        speed * Time.deltaTime);
            }
        }
    }
}

