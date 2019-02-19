﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntranceTrigger : MonoBehaviour {

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

        gameObject.transform.parent.Find("Gates").GetComponentInChildren<GateMechanism>();
    }
}
