using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour {



    /**
     * need to add in:
     * PS
     * Sounds
     * Animations
     * Shooting
     * Movement
     * Dodge
     * Specials
     */


    public Transform HandLocation, lookAtObject;

    [SerializeField]
    private List<WandStats> wands = new List<WandStats>();
    [SerializeField]
    private WandStats equippedWand = null;
    [SerializeField]
    private int currentIndex = 0;

    [SerializeField]
    private GameObject Wand;


    //private variables
    [SerializeField]
    private string CharacterName;
    //Change to a "health script"
    [SerializeField]
    private int CurrentHealth, MaxHealth;
    [SerializeField]
    private float Speed;
    private Rigidbody rigidbody;
    private Animator anim;
   

    private float reloadCounter = 0;
    [SerializeField]
    private float invinceFrames = 0;
    private Vector3 holdDirection;




    public void BuildCharacter(int health, float speed, string characterName)
    {
        anim = GetComponent<Animator>();
        transform.position = new Vector3(0, 0, 0);
        MaxHealth = health;
        CurrentHealth = health;
        Speed = speed;
        CharacterName = characterName;
        rigidbody = gameObject.AddComponent<Rigidbody>();
        rigidbody.useGravity = false;
        rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezePositionY;
        lookAtObject = new GameObject().transform;
        HandLocation = GameObject.FindGameObjectWithTag("HandLocation").transform;

    }




    private void Update()
    {
        Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        if(direction.x == 0 && direction.z == 0)
        {
            anim.SetBool("IsRunning", false);
        } else
        {
            anim.SetBool("IsRunning", true);
        }
        if(Input.GetAxis("Horizontal") < .3f && Input.GetAxis("Horizontal") > -.3f)
        {
            
            direction.x = 0;
        }

        if (Input.GetAxis("Vertical") < .3f && Input.GetAxis("Vertical") > -.3f)
        {
            direction.y = 0;
        }

        
        
        

        Vector3 lookDirection = new Vector3(-Input.GetAxis("RightStickHorizontal"), 0, -Input.GetAxis("RightStickVertical"));
        lookAtObject.position = lookDirection + transform.position;

        //Dodge in a single direction
        if (Input.GetKeyDown(KeyCode.Joystick1Button4) && invinceFrames == 0)
        {
            anim.SetBool("IsDodging", true);
            invinceFrames = 1.25f;
            holdDirection = lookDirection;

        }
        else
        {
            anim.SetBool("IsDodging", false);
        }

        if (invinceFrames - .25f > 0)
        {
            direction = holdDirection;
        }
        rigidbody.MovePosition(transform.position + (direction * Speed));

        Vector3 targetDirection = lookAtObject.position - transform.position;
       // print(targetDirection);
        Vector3 localTarget = transform.InverseTransformPoint(lookAtObject.position);
        print(localTarget);
        float angleOfRotation = Mathf.Atan2(localTarget.x, localTarget.z) * Mathf.Rad2Deg;

        Vector3 eulerAngleVelo = new Vector3(0, angleOfRotation, 0);
        Quaternion qua = Quaternion.Euler(eulerAngleVelo * Time.deltaTime * 10);
       // Quaternion mult = Quaternion.Euler(0, 100, 0);
        rigidbody.MoveRotation((rigidbody.rotation * qua));


        //Interact
        if (Input.GetKey(KeyCode.Joystick1Button0))
        {
            print("A");
        }

        //Shoot
        if (Input.GetAxis("Shoot") > 0 && reloadCounter == 0)
        {
            reloadCounter += equippedWand.shoot(targetDirection);
            print("Shoot");
        }

        reloadCounter -= Time.deltaTime;
        invinceFrames -= Time.deltaTime;
        if (reloadCounter < 0)
        {
            reloadCounter = 0;
        }
        if(invinceFrames < 0)
        {
            invinceFrames = 0;
        }




        if (Input.GetKeyDown(KeyCode.Joystick1Button5))
        {
            reloadCounter +=equippedWand.reload();
        }
       // print(wands.Count);
        if(Input.GetKeyDown(KeyCode.Joystick1Button2))
        {
            if(wands.Count > currentIndex +1)
            {
                wands[currentIndex] = equippedWand;
                currentIndex++;
                equippedWand = wands[currentIndex];
                
            } else
            {
                wands[currentIndex] = equippedWand;
                currentIndex = 0;
                equippedWand = wands[currentIndex];
            }
            equipNewWand();

        }
        Wand.transform.position = HandLocation.position;
    }

    public void AddNewWand(WandStats wand)
    {
        wands.Add(wand);
        if(equippedWand == null)
        {
            equippedWand = wand;
        }
    }

    private void equipNewWand()
    {
        Wand = equippedWand.WandModel;
       
    }






    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<WandStats>() != null && Input.GetKeyDown(KeyCode.Joystick1Button0))
        {

            WandStats newWand = other.gameObject.GetComponent<WandStats>();
            AddNewWand(other.gameObject.GetComponent<WandStats>());
            Destroy(other.gameObject);
        }


        if(other.gameObject.tag == "Bullet" && invinceFrames == 0)
        {
            CurrentHealth--;
        }

    }
}


