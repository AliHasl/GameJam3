using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour {


    public int columns = 10;
    public int rows = 10;

    public int maxRooms = 1;
    public int minRooms = 1;
    private int numCorridors = 0;
    public int minCorridorLength = 0;
    public int maxCorridorLength = 0;

    public GameObject[] floorTiles;
    

    //tempRooms used for now, to be replaced with the Rooms class laters.
    public GameObject[] tempRooms;
    public GameObject[] roomPrefabs;
    


    private GameObject boardHolder;


    private Corridor[] corridors;

    public int numRooms;
    public IntRange corridorLength;

    private int roomCount = 0;

    private enum Direction {NORTH, SOUTH, EAST, WEST }


	// Use this for initialization
	void Start () {

        

        corridorLength = new IntRange(minCorridorLength, maxCorridorLength);
        IntRange roomRange = new IntRange(minRooms, maxRooms);
        numRooms = roomRange.Random;


        //SetUpTilesArray();
        tempRooms = new GameObject[numRooms];
        CreateRoomsAndCorridors(new Vector3(0,0,0));
        
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void SetUpTilesArray()
    {
        for(int z = 0; z < 10; z++)
        {
            for (int x = 0; x < 10; x++)
            {
               Instantiate(floorTiles[0], new Vector3( x, 0.0f , z), Quaternion.identity);
                
            }
        }
    }

    void CreateRoomsAndCorridors(Vector3 position)
    {
        
        
        
        //tempRooms.Add(Instantiate(roomPrefabs[0]));

        //numCorridors = roomPrefabs.Length - 1;

        //numRooms--;

        if (roomCount < numRooms)
        {
            GameObject newRoom = Instantiate(roomPrefabs[0], position, Quaternion.identity);
            tempRooms[roomCount] = newRoom;
            int dir = Random.Range(0, 4);
            CreateCorridor((Direction)dir);
            Debug.Log(dir);
        }


        
        //corridors[0] = new Corridor();
    }

    void CreateCorridor(Direction dir)
    {

        Vector3 newRoomLocation = new Vector3();

        if (dir == Direction.NORTH)
        {
            for (int length = 0; length < corridorLength.Random; length++)
            {
                Instantiate(floorTiles[0], tempRooms[roomCount].transform.GetChild(0).transform.position + new Vector3(0, 0, length), Quaternion.identity);
                newRoomLocation = tempRooms[roomCount].transform.GetChild(0).transform.position + new Vector3(0, 0, length);
            }
            
        }

        else if (dir == Direction.EAST)
        {
            for (int length = 0; length < corridorLength.Random; length++)
            {
                Instantiate(floorTiles[0], tempRooms[roomCount].transform.GetChild(1).transform.position - new Vector3(length, 0, 0), Quaternion.identity);
                newRoomLocation = tempRooms[roomCount].transform.GetChild(1).transform.position - new Vector3(length, 0, 0);
            }

        }

        else if (dir == Direction.SOUTH)
        {
            for (int length = 0; length < corridorLength.Random; length++)
            {
                Instantiate(floorTiles[0], tempRooms[roomCount].transform.GetChild(2).transform.position - new Vector3(0, 0, length), Quaternion.identity);
                newRoomLocation = tempRooms[roomCount].transform.GetChild(2).transform.position - new Vector3(0, 0, length);
            }
        }

        else if (dir == Direction.WEST)
        {
            for (int length = 0; length < corridorLength.Random; length++)
            {
                Instantiate(floorTiles[0], tempRooms[roomCount].transform.GetChild(3).transform.position + new Vector3(length, 0, 0), Quaternion.identity);
                newRoomLocation = tempRooms[roomCount].transform.GetChild(3).transform.position + new Vector3(length, 0, 0);
            }
        }
        roomCount++;
        CreateRoomsAndCorridors(newRoomLocation);

    }

}
