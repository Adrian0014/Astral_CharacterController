using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    //velocidad del proyectil
    public float bulletSpeed;

    private Rigidbody2D rBody;
    private GameManager gameManager;

    void Awake()
    {
        rBody = GetComponent<Rigidbody2D>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void Start()
    {
        rBody.AddForce(transform.right * bulletSpeed, ForceMode2D.Impulse);
    }
   
   void OnTriggerEnter2D(Collider2D collider)
   {
       if(collider.gameObject.tag == "Goombas")
       {
            Destroy(gameObject);
            //gameManager.DeathGoomba(collider.gameObject);  
       }
       if(collider.gameObject.tag == "Coin")
       {

       }
       else
       {
           Destroy(gameObject);
       }

   }

}
