﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateMechanism : MonoBehaviour {

    public bool gateShut = false;
    public bool lowerGate = false;

    Rigidbody m_rigidBody;

    Vector3 closePosition, openPosition;

	// Use this for initialization
	void Start () {
		if(gameObject.GetComponent<Rigidbody>() == null)
        {
            gameObject.AddComponent<Rigidbody>();
        }

        m_rigidBody = gameObject.GetComponent<Rigidbody>();

        closePosition = gameObject.transform.position + new Vector3(0, 1, 0);
        openPosition = gameObject.transform.position;
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        if (gateShut)
        {
            if (gameObject.transform.position.y < closePosition.y)
                m_rigidBody.AddForce(Vector3.up);
            else
            {
                m_rigidBody.velocity = Vector3.zero;
                gateShut = false;
            }
        }

        if (lowerGate)
        {
            if (gameObject.transform.position.y > openPosition.y)
                m_rigidBody.AddForce(Vector3.down);
            else
            {
                m_rigidBody.velocity = Vector3.zero;
                lowerGate = false;
            }
        }

	}

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Room")
        {
            Physics.IgnoreCollision(GetComponent<BoxCollider>(), collision.gameObject.GetComponent<BoxCollider>());
        }
       
    }


    public void LowerGate()
    {
        lowerGate = true;
    }

    public void ShutGates()
    {

        gateShut = true;
    }
}
