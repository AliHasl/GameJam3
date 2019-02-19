using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateMechanism : MonoBehaviour {

    public bool gateShut = false;
    

    Rigidbody m_rigidBody;

    Vector3 closePosition;

	// Use this for initialization
	void Start () {
		if(gameObject.GetComponent<Rigidbody>() == null)
        {
            gameObject.AddComponent<Rigidbody>();
        }

        m_rigidBody = gameObject.GetComponent<Rigidbody>();

        closePosition = gameObject.transform.position + new Vector3(0, 1, 0);
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
            }
        }
	}

    public void ShutGates()
    {

        gateShut = true;
    }
}
