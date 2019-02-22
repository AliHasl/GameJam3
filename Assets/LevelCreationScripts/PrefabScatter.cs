using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabScatter : MonoBehaviour {


    public float width = 5.0f;
    public float length = 5.0f;
    public GameObject[] prefab;
    public int minObjects = 0;
    public int maxObjects;
    private int objectsToPlace;
    private int currentObjects = 0;

    //public ScatterPrefabConfig[] prefabs;

    public Transform[] SpawnLocations;

   


    // Use this for initialization
    public virtual void Start() {

        
            
      
    }

    public virtual void Spawn()
    {
        List<int> uniqueSpawnPoints = new List<int>();
        objectsToPlace = Random.Range(minObjects, maxObjects + 1);

        while (uniqueSpawnPoints.Count < objectsToPlace)
        {
            int numberToAdd = Random.Range(0, 9);
            while (uniqueSpawnPoints.Contains(numberToAdd))
            {
                numberToAdd = Random.Range(0, 9);
            }
            uniqueSpawnPoints.Add(numberToAdd);
            
        }

     

        foreach (int i in uniqueSpawnPoints)
        {

            int randomPrefab = Random.Range(0, prefab.Length);

            GameObject spawnedObject = Instantiate(prefab[randomPrefab], SpawnLocations[i].position, Quaternion.identity);

            //Instantiate(prefab[0]);

            if (spawnedObject.tag == "Enemy")
            {
                spawnedObject.transform.SetParent(transform.Find("Monsters"));
                //spawnedObject.transform.position = new Vector3(1000, 1000, 1000);
                Debug.Log("Enemy was supposed to Spawn here: " + SpawnLocations[i].position);
                Debug.Log("Enemy actually spawned here: " + spawnedObject.transform.position);
            }
            else
            {
                spawnedObject.transform.SetParent(transform);
            }
            
        }
    }



    public virtual void Update()
    {
        
    }

}
    /*[System.Serializable]
    public class ScatterPrefabConfig
    {
        public GameObject objectToCreate;
        public float offset;
    }*/

