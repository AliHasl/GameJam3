using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntranceTrigger : MonoBehaviour {

    bool playerInRoom = false;


	// Use this for initialization
	void Start () {
		
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
            gameObject.transform.parent.Find("Gates").GetComponentInChildren<GateMechanism>().ShutGates();
            playerInRoom = true;
        }
    }
}
