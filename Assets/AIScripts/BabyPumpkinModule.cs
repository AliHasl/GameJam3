using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BabyPumpkinModule : MonoBehaviour {

    /**
     * Baby pumpkin works like a mine
     * Search for a location in the room
     * Plant itself
     * detonate after 3 seconds
     **/

    public GameObject bullet;




    private BaseAIScript baseAIScript;
    private float timer = 5;

    private void Awake()
    {
        baseAIScript = GetComponent<BaseAIScript>();
        baseAIScript.states = BaseAIScript.States.MOVE_AI_NAVMESH;

    }





    private void Update()
    {
        if(timer < 0)
        {
            baseAIScript.states = BaseAIScript.States.FIND_PLAYER;
            
        }
        if(timer < -3)
        {
            for (float i = 0; i < 1; i += .1f)
            {
                Quaternion qua = new Quaternion(0, i * 360, 0, 0);
                GameObject bull = Instantiate(bullet, transform.position, Quaternion.Euler(0, i * 360, 0));
                bull.GetComponent<Rigidbody>().rotation = Quaternion.Euler(0, i * 360, 0);
                print(bull.GetComponent<Rigidbody>().rotation);
                bull.GetComponent<Rigidbody>().AddRelativeForce(transform.forward * 300);

            }
            print("Shooting");
        }
        timer -= Time.deltaTime;
    }
}
