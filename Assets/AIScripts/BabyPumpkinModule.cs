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


    enum PumpkinStates
    {
        FIND_MINE_PLACE,
        PLANT,
        EXPLODE
    };

    private BaseAIScript baseAIScript;


    private void Awake()
    {
        baseAIScript = GetComponent<BaseAIScript>();

    }





    private void Update()
    {
        
    }
}
