using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestScript : MonoBehaviour {

    // Use this for initialization
    public GameObject[] Common, Rare, Epic;
    public List<GameObject> listOfObjects;
    [Range(1,3)]
    public int rarity;
	public void OpenChest () {
		switch (rarity)
        {
            case 1:
                listOfObjects.AddRange(Common);
                break;
            case 2:
                listOfObjects.AddRange(Common);
                listOfObjects.AddRange(Rare);
                break;
            case 3:
                listOfObjects.AddRange(Common);
                listOfObjects.AddRange(Rare);
                listOfObjects.AddRange(Epic);
                break;
        }


        GameObject[] allItems = listOfObjects.ToArray();

        Instantiate(allItems[Random.Range(0, allItems.Length)], transform.position, Quaternion.identity);
        Destroy(gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
