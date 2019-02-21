using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMonsters : PrefabScatter {

    private Room thisRoom;
    private bool monstersSpawned = false;

	// Use this for initialization
	public override void Start () {
        base.Start();
        thisRoom = GetComponent<Room>();
	}
	
	// Update is called once per frame
	public override void Update () {
        base.Update();

        if (monstersSpawned)
        {
            if(thisRoom.transform.Find("Monsters").childCount == 0)
            {
                thisRoom.OpenExit();
            }
        }


	}

    public void CreateCreatures()
    {
         base.Spawn();
        monstersSpawned = true;
    }
}
