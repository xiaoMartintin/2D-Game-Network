using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public abstract class Enemy : NetworkBehaviour
{
    //Local
    public int health;
    public int damage;

    public float flashTime;

    private SpriteRenderer sr;
    private Color originalColor;
    //private PlayerHealth playerHealth;
    public GameObject floatPoint;
    public GameObject bloodEffect;

    //Sync
    public int healthSync;
    public int damageSync;

    public float flashTimeSync;

    private SpriteRenderer srSync;
    private Color originalColorSync;
    //private PlayerHealth playerHealthSync;
    public GameObject floatPointSync;
    public GameObject bloodEffectSync;


    public void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        originalColor = sr.color;
        //transformSync = transform;
        //playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
    }

    public void Update()
    {
        //if (health <= 0)
        //{
        //    Destroy(gameObject);
        //}
        //if (isLocalPlayer)
        //{
        //    CmdUpdate();
        //}
        //else if (isServer)
        //{
        //    RpcUpdate();
        //}
    }

    [Command]
    void CmdUpdate()
    {
        //healthSync = health;
        damageSync = damage;
        flashTimeSync = flashTime;
        originalColorSync = originalColor;
        //playerHealthSync = playerHealth;
        floatPointSync = floatPoint;
        bloodEffectSync = bloodEffect;
    }

    [ClientRpc]
    void RpcUpdate()
    {
        //health = healthSync;
        damage = damageSync;
        flashTime = flashTimeSync;
        originalColor = originalColorSync;
        //playerHealth = playerHealthSync;
        floatPoint = floatPointSync;
        bloodEffectSync = bloodEffectSync;

    }

    [ClientRpc]
    public void TakeDamage(int dam)
    {
        GameObject gb = Instantiate(floatPoint, transform.position, Quaternion.identity) as GameObject;
        gb.transform.GetChild(0).GetComponent<TextMesh>().text = damage.ToString();
        //health -= dam;
        FlashColor(flashTime);
        Instantiate(bloodEffect, transform.position, Quaternion.identity);
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && other.GetType().ToString() == "UnityEngine.PolygonCollider2D")
        {
            //if (playerHealth != null)
            //{
            //    playerHealth.DamagePlayer(damage);
            //}

        }
        //if (other.gameObject.CompareTag("Player") && other.GetType().ToString() == "UnityEngine.PolygonCollider2D")
        //{
        //    other.gameObject.GetComponent<PlayerScript>.OnHit(damage);
        ////    other.gameObject.GetComponent<PkayerNetwork>.OnHit(damage);
        //}



    }

    public void FlashColor(float time)
    {
        sr.color = Color.red;
        Invoke("ResetColor", time);
    }

    public void ResetColor()
    {
        sr.color = originalColor;
    }

}



//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public abstract class Enemy : MonoBehaviour
//{
//    public int health;
//    public int damage;
//    public float flashTime;
//    public GameObject bloodEffect;
//    public GameObject dropCoin;
//    public GameObject floatPoint;

//    private SpriteRenderer sr;
//    private Color originalColor;
//    private PlayerHealth playerHealth;

//    // Start is called before the first frame update
//    public void Start()
//    {
//        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
//        sr = GetComponent<SpriteRenderer>();
//        originalColor = sr.color;
//    }

//    // Update is called once per frame
//    public void Update()
//    {
//        if (health <= 0)
//        {
//            Instantiate(dropCoin, transform.position, Quaternion.identity);
//            Destroy(gameObject);
//        }
//    }

//    public void TakeDamage(int damage)
//    {
//        GameObject gb = Instantiate(floatPoint, transform.position, Quaternion.identity) as GameObject;
//        gb.transform.GetChild(0).GetComponent<TextMesh>().text = damage.ToString();
//        health -= damage;
//        FlashColor(flashTime);
//        Instantiate(bloodEffect, transform.position, Quaternion.identity);
//        GameController.camShake.Shake();
//    }

//    void FlashColor(float time)
//    {
//        sr.color = Color.red;
//        Invoke("ResetColor", time);
//    }

//    void ResetColor()
//    {
//        sr.color = originalColor;
//    }

//    void OnTriggerEnter2D(Collider2D other)
//    {
//        if (other.gameObject.CompareTag("Player") && other.GetType().ToString() == "UnityEngine.CapsuleCollider2D")
//        {
//            if (playerHealth != null)
//            {
//                playerHealth.DamagePlayer(damage);
//            }
//        }
//    }
//}
