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
    public int maxObjects;
    private int currentObjects;



	// Use this for initialization
	public virtual void Start () {

        //if(centrePoint == null)
        //{
            centrePoint = transform.position;
        //}

        xSize = width;
        zSize = length;

		if(currentObjects <= maxObjects)
        {
            xSize = Random.Range(xSize, -xSize);
            zSize = Random.Range(zSize, -zSize);
            float posX = Random.Range(centrePoint.x, centrePoint.x + xSize);
            float posZ = Random.Range(centrePoint.z, centrePoint.z + zSize);
            GameObject spawnedObject = Instantiate(prefab, new Vector3(posX, 0.0f, posZ), Quaternion.identity);
            currentObjects++;
        }

        
	}

   public virtual void Update()
    {
        if (Application.isEditor)
        {
            Debug.DrawLine(transform.position + new Vector3(-width, 1 ,length), transform.position + new Vector3(width, 1, length));
            Debug.DrawLine(transform.position + new Vector3(width, 1, length), transform.position + new Vector3(width, 1, -length));
            Debug.DrawLine(transform.position + new Vector3(width, 1, -length), transform.position + new Vector3(-width, 1, -length));
            Debug.DrawLine(transform.position + new Vector3(-width, 1, -length), transform.position + new Vector3(-width, 1, length));
        }
    }


    [System.Serializable]
    public class ScatterPrefabConfig
    {

    }
}
