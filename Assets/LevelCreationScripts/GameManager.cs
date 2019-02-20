using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager instance;
    public GameObject playerObject = null;
    public GameObject boardManager = null;

    public GameObject getPlayerObject() {
        return playerObject;
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        else
        {
            Destroy(gameObject);
            return;
        }

        

    }


    // Use this for initialization
    void Start () {
		if(playerObject != null)
        {
            Instantiate(playerObject, Vector3.up, Quaternion.identity);
        }

        if(GameObject.Find("BoardManager") == null)
        {
            GameObject bManager = Instantiate(boardManager);
            bManager.name = "BoardManager";

        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
