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
            //ShootStuffHere
            print("Shooting");
        }
        timer -= Time.deltaTime;
    }
}
