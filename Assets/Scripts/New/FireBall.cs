using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
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
       if(collider.gameObject.tag == "PorcuxPlanta")
       {
            Debug.Log("Tocar enemigo");
            Destroy(gameObject);
            GameManager.Instance.DeadPlanta(collider.gameObject);  
       }
       else
       {
           Destroy(gameObject);
       }

   }
}
