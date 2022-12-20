using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBall : MonoBehaviour
{
    //velocidad del proyectil
    public float bulletSpeed;

    private Rigidbody rBody;
    

    void Awake()
    {
        rBody = GetComponent<Rigidbody>();
    }

    void Start()
    {
        rBody.AddForce(transform.forward * bulletSpeed);
    }
   
   void OnTriggerEnter(Collider collider)
   {
       if(collider.gameObject.tag == "PorcuxFuego")
       {
            Debug.Log("Tocar enemigo");
            Destroy(gameObject);
            GameManager.Instance.DeadFuego(collider.gameObject);    
       }
       else
       {
           Destroy(gameObject);
       }

   }
}
