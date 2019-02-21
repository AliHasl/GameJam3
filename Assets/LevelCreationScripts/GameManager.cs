using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager instance;
    public GameObject[] players = null;
    public GameObject boardManager = null;
    private Camera m_camera;
    private PlayerCreation playerCreation;
    public Canvas playerHUD;

    private GameObject playerObject;

    Vector3 offset;

    public GameObject getPlayerObject() {
        return players[0];
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

        //Canvas newCanvas = Instantiate(playerCreationScreen);

        //newCanvas.transform.SetParent(Camera.main.transform);
        
   

        offset = new Vector3(0, 10, 0);

        m_camera = Camera.main;
        

    }

    public void displayHUD()
    {
        Camera.main.transform.GetChild(1).GetComponent<Canvas>().enabled = true;
    }


    public void SetPlayerObject()
    {
        if(playerObject == null)
        {
            playerObject = GameObject.FindGameObjectWithTag("Player");
        }


        //m_camera.transform.SetParent(playerObject.transform);
        
    }

    // Use this for initialization
    void Start () {
		/*if(playerObject == null)
        {
            Debug.Log("Firing");
            playerObject = Instantiate(players[0], Vector3.up, Quaternion.identity);
        }*/

        if(GameObject.Find("BoardManager") == null)
        {
            GameObject bManager = Instantiate(boardManager);
            bManager.name = "BoardManager";

        }




	}
	
	// Update is called once per frame
	void Update () {
		if(playerObject != null)
        {
            m_camera.transform.position = playerObject.transform.position + offset;
        }
	}
}
