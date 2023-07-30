using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthBar : HealthBar
{
    private static int enemyHealth;
    // Start is called before the first frame update
    new void Start()
    {
        enemyHealth = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Enemy>().health;
        Debug.Log("enemtHealth: " + enemyHealth);
        HealthMax = enemyHealth;
        base.Start();

    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();
    }
}
