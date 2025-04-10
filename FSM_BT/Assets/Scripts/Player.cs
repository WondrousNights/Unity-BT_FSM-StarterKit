using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
[RequireComponent(typeof(CharacterController), typeof(PlayerInput))]
public class Player : MonoBehaviour
{
    //Singleton
   public static Player Instance {get; private set;}

    CharacterController cc;
    Vector3 moveInput;

    [SerializeField] float speed;
    [SerializeField] float powerUpDuration;
    [SerializeField] Material powerUpMaterial;
    [SerializeField] Material playerMaterial;
    [SerializeField] TMP_Text winText;
    [SerializeField] GameObject bullet;
    [SerializeField] Transform bulletShootPos;
    [SerializeField] float bulletForce;
    public bool isPoweredUp = false;

    float shootTimer;
    int ammo = 0;
    
    void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
        
    }

    void Start()
    {
        winText.text = "";
        cc = GetComponent<CharacterController>();
    }

    //Recieves Messages from PlayerInput component
    public void OnMove(InputValue value)
    {
        Vector2 moveValue = value.Get<Vector2>();
        moveInput = new Vector3(moveValue.x,0,moveValue.y);
    }

    //Recieves Messages from PlayerInput component
    public void OnClick(InputValue value)
    {
       if(shootTimer >= 0.1f && ammo > 0)
       {
            //Instantiates bullet and applies force to it
            GameObject bulletGO = Instantiate(bullet, bulletShootPos.position, Quaternion.identity);
            bulletGO.GetComponent<Rigidbody>().AddForce(bulletShootPos.forward * bulletForce, ForceMode.Impulse);
            Destroy(bulletGO, 1f);
            shootTimer = 0f;
       }
        
    }

    void Update()
    {
        cc.Move(moveInput * speed * Time.deltaTime);

        //Slerps rotation based on mouse position
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hitInfo, 100f))
        {
            Vector3 lookAtPoint = hitInfo.point;
            Vector3 direction = lookAtPoint - transform.position;
            direction.y = 0f; 

            if (direction != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f);
            }
        }

        if(shootTimer <= 0.2f)
        {
            shootTimer += Time.deltaTime;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Powerup")
        {
           StartCoroutine(PowerUpRoutine());
           ammo += 5;
           Destroy(other.gameObject);
        }

        if(other.gameObject.tag == "Win")
        {
            winText.text = "YouWin!";
            this.enabled = false;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {

            if(isPoweredUp)
            {
                Destroy(collision.gameObject);
            }
            else
            {
                this.enabled = false;
                GetComponentInChildren<MeshRenderer>().enabled = false;
                winText.text = "You Lose!";
            }
        }

        
    }

    
    private IEnumerator PowerUpRoutine()
    {
        GetComponentInChildren<MeshRenderer>().material = powerUpMaterial;
        isPoweredUp = true;
        yield return new WaitForSeconds(powerUpDuration);
        isPoweredUp = false;
        GetComponentInChildren<MeshRenderer>().material = playerMaterial;
    }

}
