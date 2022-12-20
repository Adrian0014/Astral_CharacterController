using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{


    public static GameManager Instance;

    public bool powerWater = false;

    public bool powerFire = false;

    void Awake() 
    {
        if( Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    void Update() 
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            powerFire = true;
            powerWater = false;
            Debug.Log("Fuego");
        }
        else if(powerFire == true)
        {

        }

        if(Input.GetKeyDown(KeyCode.E))
        {
            powerFire = false;
            powerWater = true;
            Debug.Log("Agua");
        }       
    }

    public void DeadPlanta(GameObject plantita)
    {

        Destroy(plantita, 0.5f);
        Debug.Log("Muerete");

    }

    public void DeadFuego(GameObject fueguito)
    {

        Destroy(fueguito, 0.5f);
        Debug.Log("MuereteFunny");

    }



}
