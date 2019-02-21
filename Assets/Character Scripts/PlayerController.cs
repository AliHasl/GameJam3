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
    private List<GameObject> wands = new List<GameObject>();
    [SerializeField]
    private GameObject equippedWand = null;
    private WandStats equippedWandStats = null;
    [SerializeField]
    private int currentIndex = 0;

    [SerializeField]
    private GameObject Wand = null;


    //private variables
    [SerializeField]
    private string CharacterName;
    //Change to a "health script"
    [SerializeField]
    private int CurrentHealth, MaxHealth;
    [SerializeField]
    private float Speed;
    private Rigidbody rigidBody;
    private Animator anim;
   

    private float reloadCounter = 0;
    [SerializeField]
    private float invinceFrames = 0;
    private Vector3 holdDirection;
    private Vector3 holdLookDirection;

    private bool characterBuilt = false;



    public void BuildCharacter(int health, float speed, string characterName, GameObject startWand)
    {
        gameObject.tag = "Player";
        anim = GetComponent<Animator>();
        transform.position = new Vector3(0, 0, 0);
        MaxHealth = health;
        CurrentHealth = health;
        Speed = speed;
        CharacterName = characterName;
        rigidBody = GetComponent<Rigidbody>();
        rigidBody.useGravity = false;
        rigidBody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezePositionY;
        characterBuilt = true;
        lookAtObject = GameObject.FindGameObjectWithTag("lookAtObject").transform;
        HandLocation = GameObject.FindGameObjectWithTag("HandLocation").transform;
        //Wand.name = ("lalalalala");
        //Wand.transform.parent = HandLocation;
        //Wand.transform.SetParent(HandLocation.gameObject.transform);
        GameObject newWand = Instantiate(startWand, transform.position, Quaternion.identity);
        AddNewWand(newWand);
        
        //equipNewWand();
        

    }




    private void Update()
    {
        if (!characterBuilt)
        {
            return;
        }
        //print(Wand.transform.parent);
        Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        if (anim != null)
        {
            if (direction.x == 0 && direction.z == 0)
            {
                anim.SetBool("IsRunning", false);
            }
            else
            {
                anim.SetBool("IsRunning", true);
            }
            if (Input.GetAxis("Horizontal") < .3f && Input.GetAxis("Horizontal") > -.3f)
            {

                direction.x = 0;
            }

            if (Input.GetAxis("Vertical") < .3f && Input.GetAxis("Vertical") > -.3f)
            {
                direction.y = 0;
            }

        }



        Vector3 lookDirection = new Vector3(-Input.GetAxis("RightStickHorizontal"), 0, -Input.GetAxis("RightStickVertical"));
        lookAtObject.position = lookDirection + transform.position;

        //Dodge in a single direction
        if (Input.GetKeyDown(KeyCode.Joystick1Button4) && invinceFrames == 0)
        {
           // anim.SetBool("IsRunning", false);

            anim.SetBool("IsDodging", true);
            invinceFrames = 1.25f;
            holdDirection = lookDirection;
            holdLookDirection = lookAtObject.position;

        }
        else if (invinceFrames < .4f)
        {
            anim.SetBool("IsDodging", false);
        }

        if (invinceFrames - .3f > 0)
        {
            direction = holdDirection;
            //lookAtObject.position = holdLookDirection;
        }
        else
        {
            

            Vector3 targetDirection = lookAtObject.position - transform.position;
            // print(targetDirection);
            Vector3 localTarget = transform.InverseTransformPoint(lookAtObject.position);
            //print(localTarget);
            float angleOfRotation = Mathf.Atan2(localTarget.x, localTarget.z) * Mathf.Rad2Deg;

            Vector3 eulerAngleVelo = new Vector3(0, angleOfRotation, 0);
            Quaternion qua = Quaternion.Euler(eulerAngleVelo * Time.deltaTime * 10);
            // Quaternion mult = Quaternion.Euler(0, 100, 0);
            rigidBody.MoveRotation((rigidBody.rotation * qua));


            //Interact
            

            //Shoot
            if (Input.GetAxis("Shoot") > 0 && reloadCounter == 0)
            {
                reloadCounter += equippedWandStats.shoot(-targetDirection);
                print("Shoot");
            }
        }
        rigidBody.MovePosition(transform.position + (direction * Speed));

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
            reloadCounter +=equippedWandStats.reload();
        }
       // print(wands.Count);
        if(Input.GetKeyDown(KeyCode.Joystick1Button2))
        {
            if(wands.Count > currentIndex +1)
            {
                equippedWand.SetActive(false);
                wands[currentIndex] = equippedWand;
                currentIndex++;
                equippedWand = wands[currentIndex];
                equippedWand.SetActive(true);
                
            } else
            {
                equippedWand.SetActive(false);
                wands[currentIndex] = equippedWand;
                currentIndex = 0;
                equippedWand = wands[currentIndex];
                equippedWand.SetActive(true);
            }
            equipNewWand();

        }
        Wand.transform.localPosition = new Vector3(0,0,0);
        //Wand.GetComponent<WandStats>().particleGameObject.transform.localPosition = new Vector3(0, 0, 0);
    }

    public void AddNewWand(GameObject wand)
    {
        GameObject newWand = wand;
        wands.Add(newWand);
        if (wands.Count > 1)
        {
            equippedWand.SetActive(false);
            wands[currentIndex] = equippedWand;
            currentIndex++;
            equippedWand = wands[currentIndex];
            equippedWand.SetActive(true);
        }
        else
        {


            equippedWand = wand;
        }
            equipNewWand();       
    }

    private void equipNewWand()
    {
        equippedWandStats = equippedWand.GetComponent<WandStats>();
        Wand = equippedWand;
        Wand.transform.SetParent(HandLocation);
        
    }






    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Wand" && Input.GetKeyDown(KeyCode.Joystick1Button0))
        {
            print("Step 1 works");
            if (other.gameObject.GetComponent<WandStats>().pickUp)
            {
                print("Step 2 works");
                WandStats newWand = other.gameObject.GetComponent<WandStats>();
                AddNewWand(other.gameObject);
                other.gameObject.SetActive(false);
            }
        }


        if(other.gameObject.tag == "Bullet" && invinceFrames == 0)
        {
            CurrentHealth--;
        }

        if (other.gameObject.tag == "Wand")
        {
            print("WandHere");
        }
    }


    public void takeDamage()
    {
        CurrentHealth--;
    }
}


