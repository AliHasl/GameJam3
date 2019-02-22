using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMonsters : PrefabScatter {

    [SerializeField]
    private Room thisRoom;
    private bool monstersSpawned = false;

	// Use this for initialization
	public override void Start () {
        base.Start();
        thisRoom = transform.GetComponent<Room>();
	}
	
	// Update is called once per frame
	public override void Update () {
        base.Update();

        if (monstersSpawned)
        {
            if(transform.Find("Monsters").childCount == 0)
            {
                Debug.Log("childCount = " + transform.Find("Monsters").childCount);
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
