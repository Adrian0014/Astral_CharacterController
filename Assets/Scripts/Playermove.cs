using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playermove : MonoBehaviour
{
    private CharacterController controller;
    [SerializeField]private float speed = 5f;
    [SerializeField]private float jumpHeight = 1f;
    [SerializeField]private float gravity = -9.81f;
    private Transform cam;
    private float currentVelocity;
    [SerializeField]private float shoothTime = 0.5f;
    [SerializeField]private Transform groundSensor;
    [SerializeField]private float sensorRadius = 0.2f;
    [SerializeField]private LayerMask gorundLayer;
    private Vector3 playerVelocity;
    [SerializeField]private bool isGrounded;
    [SerializeField]private LayerMask detectorLayer;

    
    public GameObject bolitaAgua;

    public GameObject bolitaFuego;
  
    public Transform bulletSpwan;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
        cam = Camera.main.transform;
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        //Esta mierda es para crear los detectores del ray   PARA QUE FUNCIONE LOS RAYCAST ES NECESARIO COLIDERS
        RaycastHit hit;
        if(Physics.Raycast(transform.position, transform.forward, out hit, 10f, detectorLayer))
        {
            Debug.DrawRay(transform.position, transform.forward * hit.distance, Color.green);

            Vector3 hitPosition = hit.point;
            string hitName = hit.transform.name;
            string hitTag = hit.transform.tag;
            float hitDistance = hit.distance;
            Animator hitAnim = hit.transform.gameObject.GetComponent<Animator>();

            if(hitTag == "obstaculo")
            {
                Debug.Log("Impacto en obstaculo");
            }

            if(hitAnim != null)
            {
                hitAnim.SetBool("Jump", true);
            }
        }
        else
        {
            //cuando no detecta nada se pondra de color rojo y cuando detecta algo se pondra en verde
            Debug.DrawRay(transform.position, transform.forward * 10f, Color.red);
        }

        /*if(Input.GetButtonDown("Fire1"))
        {

            //Con esto cuando le demos clic a un objeto nos dira el nombre
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit rayHit;

            if(Physics.Raycast(ray, out rayHit))
            {
                Debug.Log(rayHit.transform.name);


                //si cuando hago clic hay algo con el nombre de XXX hara xxxx
                if(rayHit.transform.name == "Capsule")
                {
                    Debug.Log("Personaje");
                }
                if (rayHit.transform.name == "Cube")
                {
                    Destroy(rayHit.transform.gameObject);
                }

                if (rayHit.transform.name == "Next")
                {
                    //GameManager.Instance.LoadLevel(1);
                }
            }
        }*/
        if(Input.GetButtonDown("Fire1") && GameManager.Instance.powerFire == true)
        {
            Instantiate(bolitaFuego, bulletSpwan.position, bulletSpwan.rotation);
        
        }
        
        if(Input.GetButtonDown("Fire1") && GameManager.Instance.powerWater == true)
        {
            Instantiate(bolitaAgua, bulletSpwan.position, bulletSpwan.rotation);
        
        }
        /*if(Input.GetButtonDown("Fire1") && gameManager.shootPowerUp == true)
        {
            Instantiate(bulletPref, bulletSpwan.position, bulletSpwan.rotation);
        
        }*/


        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));

        if(movement != Vector3.zero)
        {
            float targetAngle = Mathf.Atan2(movement.x, movement.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float smoothAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, cam.eulerAngles.y, ref currentVelocity, shoothTime);

            transform.rotation = Quaternion.Euler(0, smoothAngle, 0);

            Vector3 moveDirection = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;
            controller.Move(moveDirection * speed * Time.deltaTime);        
        }

        //isGrounded = Physics.CheckSphere(groundSensor.position,sensorRadius, gorundLayer);


        //Utilizamos un RayCast para el sensordelsuelo
        if(Physics.Raycast(groundSensor.position, Vector3.down, sensorRadius, gorundLayer))
        {
            isGrounded = true;
            Debug.DrawRay(groundSensor.position, Vector3.down * sensorRadius, Color.green);
        }
        else
        {
            isGrounded = false;
            Debug.DrawRay(groundSensor.position, Vector3.down * sensorRadius, Color.red);

        }


        if(playerVelocity.y < 0 && isGrounded)
        {
            playerVelocity.y = 0;
        }

        if(isGrounded && Input.GetButtonDown("Jump"))
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        playerVelocity.y += gravity * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);


    }


    //El color que va tener el sensor del suelo cuando lo selecionesmos en el edit
    void OnDrawGizmosSelected() 
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(groundSensor.position, Vector3.down * sensorRadius);
    }
}
