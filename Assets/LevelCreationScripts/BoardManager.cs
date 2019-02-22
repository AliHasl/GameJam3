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

    private GameObject newRoom;
    private Vector3 newRoomLocation;

    //tempRooms used for now, to be replaced with the Rooms class laters.
    public GameObject[] tempRooms;
    public GameObject[] roomPrefabs;
    


    private GameObject boardHolder;
    public float corridorZSize;

    private Corridor[] corridors;

    public int numRooms;
    public IntRange corridorLength;

    private int roomCount = 0;

    private int previousDir = -1;

    private int spaceX = 0; int spaceZ = 0;

    private enum Direction {NORTH, EAST, SOUTH, WEST }

    private Direction previousPreviousDir;

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

        List<GameObject> roomList = new List<GameObject>();
        roomList.AddRange(tempRooms);

        //Instantiate the BOSS ROOM
        GameObject bossRoom = Instantiate(roomPrefabs[2]);
        Vector3 roomOffset = bossRoom.transform.position + bossRoom.transform.Find("Exits").GetChild((int)previousDir).transform.position;
        bossRoom.transform.position = newRoomLocation - roomOffset;
        bossRoom.name = "BossRoom";
        roomList.Add(bossRoom);
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
            int randomRoom = Random.Range(0, roomPrefabs.Length - 1);           //Leave the large room for the boss encounter
            newRoom = Instantiate(roomPrefabs[randomRoom],new Vector3(0,0,0), Quaternion.identity);
            
          
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
                                spaceMap[spaceX][spaceZ + 2] = true;        //Additional block to try prevent this
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
                                spaceMap[spaceX + 2][spaceZ] = true;        //Additional block to try prevent this
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
                                spaceMap[spaceX][spaceZ - 2] = true;        //Additional block to try prevent this
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
                                spaceMap[spaceX - 2][spaceZ] = true;        //Additional block to try prevent this
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
                newRoom.name = "room no." + roomCount;
            }

            //Sets the exit direction for the room gate controls.
            newRoom.GetComponent<Room>().exit = dir;
            CreateCorridor((Direction)dir, (Direction)previousDir);
            
        }

    }

  

    void CreateCorridor(Direction dir, Direction prev)
    {
        
        newRoomLocation = new Vector3();
        previousPreviousDir = prev;

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
                    GameObject newFloorTile = Instantiate(floorTiles[0], tempRooms[roomCount].transform.Find("Exits").GetChild(0).transform.position + new Vector3(0, 0, corridorZSize * length), Quaternion.Euler(-90, 0, 0));
                newFloorTile.transform.position += (newFloorTile.transform.position - newFloorTile.transform.GetChild(0).position);
                newFloorTile.transform.SetParent(newRoom.transform.Find("MyCorridor"));
                    newRoomLocation = tempRooms[roomCount].transform.Find("Exits").GetChild(0).transform.position + new Vector3(0, 0, corridorZSize * length);
                }
            previousDir = (int)Direction.SOUTH;
            
            }

            else if (dir == Direction.EAST)
            {
                for (int length = 0; length < corridorLength.Random; length++)
                {
                    GameObject newFloorTile = Instantiate(floorTiles[0], tempRooms[roomCount].transform.Find("Exits").GetChild(1).transform.position + new Vector3(corridorZSize * length, 0, 0), Quaternion.Euler(-90, 90, 0));
                newFloorTile.transform.position += (newFloorTile.transform.position - newFloorTile.transform.GetChild(0).position);
                newFloorTile.transform.SetParent(newRoom.transform.Find("MyCorridor"));
                newRoomLocation = tempRooms[roomCount].transform.Find("Exits").GetChild(1).transform.position + new Vector3(corridorZSize * length, 0, 0);
                }
            previousDir = (int)Direction.WEST;
            
        }

            else if (dir == Direction.SOUTH)
            {
                for (int length = 0; length < corridorLength.Random; length++)
                {
                    GameObject newFloorTile = Instantiate(floorTiles[0], tempRooms[roomCount].transform.Find("Exits").GetChild(2).transform.position - new Vector3(0, 0, corridorZSize * length), Quaternion.Euler(-90,0,0));
                newFloorTile.transform.position += (newFloorTile.transform.position - newFloorTile.transform.GetChild(0).position);
                newFloorTile.transform.SetParent(newRoom.transform.Find("MyCorridor"));
                newRoomLocation = tempRooms[roomCount].transform.Find("Exits").GetChild(2).transform.position - new Vector3(0, 0, corridorZSize * length);
                }
            previousDir = (int)Direction.NORTH;
           
        }

            else if (dir == Direction.WEST)
            {
                for (int length = 0; length < corridorLength.Random; length++)
                {
                    GameObject newFloorTile = Instantiate(floorTiles[0], tempRooms[roomCount].transform.Find("Exits").GetChild(3).transform.position - new Vector3(corridorZSize * length, 0, 0), Quaternion.Euler(-90, 90, 0));
                newFloorTile.transform.position += (newFloorTile.transform.position - newFloorTile.transform.GetChild(0).position);
                newFloorTile.transform.SetParent(newRoom.transform.Find("MyCorridor"));
                newRoomLocation = tempRooms[roomCount].transform.Find("Exits").GetChild(3).transform.position - new Vector3(corridorZSize * length, 0, 0);
                }
            previousDir = (int)Direction.EAST;
            
        }
        

        //previousDir =((int)dir + 2) % 4;
        roomCount++;
        //(Direction)previousDir;

        CreateRoomsAndCorridors(newRoomLocation);

    }

}
