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

    private int previousDir = -1;

    private int spaceX = 0; int spaceZ = 0;

    private enum Direction {NORTH, EAST, SOUTH, WEST }

    private bool[][] spaceMap;

	// Use this for initialization
	void Start () {

        spaceMap = new bool[50][];
        for(int i = 0; i < 50; i++)
        {
            spaceMap[i] = new bool[50];
        }

        

        corridorLength = new IntRange(minCorridorLength, maxCorridorLength);
        IntRange roomRange = new IntRange(minRooms, maxRooms);
        numRooms = roomRange.Random;

        
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

    void CreateRoomsAndCorridors(Vector3 pos)
    {
        


        if (roomCount < numRooms)
        {
            int randomRoom = Random.Range(0, roomPrefabs.Length);
            GameObject newRoom = Instantiate(roomPrefabs[randomRoom],new Vector3(0,0,0), Quaternion.identity);
            
          
            tempRooms[roomCount] = newRoom;
            
            int dir = Random.Range(0, 4);
            if (previousDir == -1)
            {
                spaceX = 25;
                spaceZ = 25;
                spaceMap[spaceX][spaceZ] = true;
            }
            else
            {


                bool blocked = true;
                int attempts = 0;
                while (blocked) {
                    switch (dir)
                    {
                        case 0:
                            if (spaceMap[spaceX][spaceZ + 1] == true)
                            {
                                dir++;
                                attempts++;
                               
                            }
                            else
                            {
                                spaceMap[spaceX][spaceZ + 1] = true;
                                spaceZ++;
                                blocked = false;

                            }
                            break;
                        case 1:
                            if (spaceMap[spaceX + 1][spaceZ] == true)
                            {
                                dir++;
                                attempts++;
                            }
                            else
                            {
                                spaceMap[spaceX + 1][spaceZ] = true;
                                spaceX++;
                                blocked = false;
                            }
                            break;
                        case 2:
                            if (spaceMap[spaceX][spaceZ - 1] == true)
                            {
                                dir++;
                                attempts++;
                            }
                            else
                            {
                                spaceMap[spaceX][spaceZ - 1] = true;
                                spaceZ--;
                                blocked = false;
                            }
                            break;
                        case 3:
                            if (spaceMap[spaceX - 1][spaceZ] == true)
                            {
                                dir = 0;
                                attempts++;
                            }
                            else
                            {
                                spaceMap[spaceX - 1][spaceZ] = true;
                                spaceX--;
                                blocked = false;
                            }
                            break;
                    }
                    if(attempts > 4)
                    {
                        roomCount = numRooms;
                        break;
                    }
                }
                newRoom.transform.position = pos -= newRoom.transform.Find("Exits").GetChild(previousDir).transform.position;
            }

            //Sets the exit direction for the room gate controls.
            newRoom.GetComponent<Room>().exit = dir;
            Debug.Log("YO This is the Dir " + dir);
            CreateCorridor((Direction)dir, (Direction)previousDir);
            
        }

    }

  

    void CreateCorridor(Direction dir, Direction prev)
    {
        
        Vector3 newRoomLocation = new Vector3();


        if ((int)prev != -1)
        {
            while((int)dir == (int)prev)
            {
                dir = (Direction)Random.Range(0, 4);
            }
        }

        
            if (dir == Direction.NORTH)
            {
                for (int length = 0; length < corridorLength.Random; length++)
                {
                    Instantiate(floorTiles[0], tempRooms[roomCount].transform.Find("Exits").GetChild(0).transform.position + new Vector3(0, 0, length), Quaternion.identity);
                    newRoomLocation = tempRooms[roomCount].transform.Find("Exits").GetChild(0).transform.position + new Vector3(0, 0, length);
                }
            previousDir = (int)Direction.SOUTH;
            Debug.Log("Road " + roomCount + " Built NORTH");
            }

            else if (dir == Direction.EAST)
            {
                for (int length = 0; length < corridorLength.Random; length++)
                {
                    Instantiate(floorTiles[0], tempRooms[roomCount].transform.Find("Exits").GetChild(1).transform.position + new Vector3(length, 0, 0), Quaternion.identity);
                    newRoomLocation = tempRooms[roomCount].transform.Find("Exits").GetChild(1).transform.position + new Vector3(length, 0, 0);
                }
            previousDir = (int)Direction.WEST;
            Debug.Log("Road " + roomCount + " Built EAST");
        }

            else if (dir == Direction.SOUTH)
            {
                for (int length = 0; length < corridorLength.Random; length++)
                {
                    Instantiate(floorTiles[0], tempRooms[roomCount].transform.Find("Exits").GetChild(2).transform.position - new Vector3(0, 0, length), Quaternion.identity);
                    newRoomLocation = tempRooms[roomCount].transform.Find("Exits").GetChild(2).transform.position - new Vector3(0, 0, length);
                }
            previousDir = (int)Direction.NORTH;
            Debug.Log("Road " + roomCount + " Built SOUTH");
        }

            else if (dir == Direction.WEST)
            {
                for (int length = 0; length < corridorLength.Random; length++)
                {
                    Instantiate(floorTiles[0], tempRooms[roomCount].transform.Find("Exits").GetChild(3).transform.position - new Vector3(length, 0, 0), Quaternion.identity);
                    newRoomLocation = tempRooms[roomCount].transform.Find("Exits").GetChild(3).transform.position - new Vector3(length, 0, 0);
                }
            previousDir = (int)Direction.EAST;
            Debug.Log("Road " + roomCount + " Built WEST");
        }
        

        //previousDir =((int)dir + 2) % 4;
        roomCount++;
        Direction previous = (Direction)previousDir;
        Debug.Log("My Direction is " + dir + " My Previous direction is " + previous);
        CreateRoomsAndCorridors(newRoomLocation);

    }

}
