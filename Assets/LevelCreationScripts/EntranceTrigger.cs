using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntranceTrigger : MonoBehaviour {

    bool playerInRoom = false;
    GateMechanism[] roomGates;

	// Use this for initialization
	void Start () {
       roomGates = transform.parent.parent.Find("Gates").GetComponentsInChildren<GateMechanism>();

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.name != "Player")
        {
            return;
        }
        if (playerInRoom == false)
        {
            //transform.parent.parent.Find("Gates").GetComponentInChildren<GateMechanism>().ShutGates() ;
            foreach(GateMechanism g in roomGates)
            {
                g.ShutGates();
            }

            transform.parent.parent.GetComponent<SpawnMonsters>().CreateCreatures();

            playerInRoom = true;
        }
    }
}
