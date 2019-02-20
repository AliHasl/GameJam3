using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BaseAIScript : MonoBehaviour {

/**
 * Basic AI Needs:
 * Find player
 * Navmesh Angent
 * Moveto Location
 * Death
 * 
 */

    public enum States
    {
        FIND_PLAYER,
        MOVE_AI_NAVMESH,
        DEATH,
    };




    public Vector3 playerLocation;
    [SerializeField]
    private float speed, maxSpeed;
    private NavMeshAgent agent;

    private Animator anim;
    private GameObject player;

    public float Health;
    public Vector3 MoveToLocation;

    private void Awake()
    {
        gameObject.AddComponent<MeshCollider>();
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        states = States.MOVE_AI_NAVMESH;
        MoveToLocation = RandomNavmeshLocation(4f);
        agent.SetDestination(MoveToLocation);
    }

    public States states;

    public void Update()//BaseAi()
    {
        //Base States can be run constantly, no need to turn them off
        switch (states)
        {
            case BaseAIScript.States.FIND_PLAYER:
                playerLocation = player.transform.position;
                break;
            case BaseAIScript.States.MOVE_AI_NAVMESH:
                print("Outside");
                if (transform.position.x == agent.destination.x && agent.destination.z == MoveToLocation.z || agent.velocity.magnitude == 0)
                {
                    print("inside");
                    MoveToLocation = RandomNavmeshLocation(4f);
                    agent.SetDestination(MoveToLocation);
                }              
                break;

        }

        if(speed < maxSpeed)
        {
            speed += Time.deltaTime / 2;
        }
        if(speed < 0)
        {
            speed = 0;
        }

        if(Health < 0)
        {
            Destroy(gameObject);
        }
        
    }

    public void slowDown(float amount)
    {
        speed -= amount;
    }



    /**
     * Taken from "https://answers.unity.com/questions/475066/how-to-get-a-random-point-on-navmesh.html"
     * Author Selzier
     * */
    public Vector3 RandomNavmeshLocation(float radius)
    {
        Vector3 randomDirection = Random.insideUnitSphere * radius;
        randomDirection += transform.position;
        NavMeshHit hit;
        Vector3 finalPosition = new Vector3(0,1000,0);
        if (NavMesh.SamplePosition(randomDirection, out hit, radius, 1))
        {
            finalPosition = hit.position;
        }
       // finalPosition += transform.position;
        return finalPosition;
    }


   


}
