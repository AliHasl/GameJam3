using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabScatter : MonoBehaviour {


    public float width = 5.0f;
    public float length = 5.0f;
    private float xSize = 10.0f;
    private float zSize = 10.0f;
    public Vector3 centrePoint;
    public GameObject prefab;
    public int minObjects = 0;
    public int maxObjects;
    private int objectsToPlace;
    private int currentObjects = 0;

    public ScatterPrefabConfig[] prefabs;

    public Transform[] SpawnLocations;



    // Use this for initialization
    public virtual void Start() {
        

        centrePoint = transform.position;


        xSize = width;
        zSize = length;
        List<int> uniqueSpawnPoints = new List<int>();
        objectsToPlace = Random.Range(minObjects, maxObjects);

        while (uniqueSpawnPoints.Count < objectsToPlace){
            int numberToAdd = Random.Range(0, 9);
            while (uniqueSpawnPoints.Contains(numberToAdd))
            {
                numberToAdd = Random.Range(0, 9);
            }
            uniqueSpawnPoints.Add(numberToAdd);
        }

    
        foreach(int i in uniqueSpawnPoints)
        {
            GameObject spawnedObject = Instantiate(prefabs[0].objectToCreate, SpawnLocations[i].position, Quaternion.identity);
        }
            
       

    }

    public virtual void Update()
    {
        if (Application.isEditor)
        {
            Debug.DrawLine(transform.position + new Vector3(-width, 2, length), transform.position + new Vector3(width, 2, length));
            Debug.DrawLine(transform.position + new Vector3(width, 2, length), transform.position + new Vector3(width, 2, -length));
            Debug.DrawLine(transform.position + new Vector3(width, 2, -length), transform.position + new Vector3(-width, 2, -length));
            Debug.DrawLine(transform.position + new Vector3(-width, 2, -length), transform.position + new Vector3(-width, 2, length));
        }
    }

}
    [System.Serializable]
    public class ScatterPrefabConfig
    {
        public GameObject objectToCreate;
        public float offset;
    }

