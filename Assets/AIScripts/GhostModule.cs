using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostModule : MonoBehaviour {


    /**
     * Things needed for Ghost:
     * Aim 
     * Shoot
     * */


    // Use this for initialization


    private BaseAIScript baseAIScript;

    public WandStats wand;

    private float waitTime = 0;

    private void Awake()
    {

        if (GetComponent<BaseAIScript>())
        {
            baseAIScript = GetComponent<BaseAIScript>();
        } else
        {
            print("YOU FORGOT TO ADD THE BASEAI SCRIPT!");
        }
    }
    enum GhostStates
    {
        AIM,
        SHOOT
    };

    private GhostStates ghostStates;


    private void Update()
    {

        transform.LookAt(baseAIScript.playerLocation);

        if (waitTime < 0)
        {
            baseAIScript.states = BaseAIScript.States.FIND_PLAYER;
            waitTime += wand.shoot(-(baseAIScript.playerLocation - transform.position).normalized);
            
        } else
        {
            baseAIScript.states = BaseAIScript.States.MOVE_AI_NAVMESH;
        }

        waitTime -= Time.deltaTime;

    
    }
}
