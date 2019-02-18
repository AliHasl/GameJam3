using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour {

    public int xSize = 1;
    public int zSize = 1;

    public int xPos { get; private set; }
    public int zPos { get; private set; }

    private int exitCount;

    BoardManager myBoard;

    public void InitialiseRoom(int x, int z)
    {
        exitCount = transform.childCount;
        xPos = x;
        zPos = z;

        myBoard = GameObject.Find("BoardManager").GetComponent<BoardManager>();

        myBoard.numRooms--;

        if(myBoard.numRooms > 0)
        {
            int exit = Random.Range(0, exitCount);
            Corridor newCorridor = new Corridor();
            
            //Instantiate(newCorridor, transform.GetChild(exit).transform);
        }

    }

    // Use this for initialization
    void Start() {
        //InitialiseRoom((int) transform.position.x, (int) transform.position.z);

}
	
	// Update is called once per frame
	void Update () {
		
	}
}
