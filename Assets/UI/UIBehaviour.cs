﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBehaviour : MonoBehaviour {

    public GameObject heartPrefab;
    List<GameObject> heartDisplay;
    private int healthIndex = 0;
    [SerializeField]
    private int currentHealth = 0;
    public bool testAdd = false;
    public bool testDecreaseHealth = false;

    private void testAddHeart()
    {
        if (testAdd)
        {
            addHeart();
            testAdd = false;
        }
    }

    private void testingDecreaseHealth()
    {
        if (testDecreaseHealth)
        {
            currentHealth--;
            UpdateHealthBar(currentHealth);
            testDecreaseHealth = false;
        }
    }

	// Use this for initialization
	void Start () {
        heartDisplay = new List<GameObject>();
	}
	
	// Update is called once per frame
	void Update () {

        testAddHeart();
        testingDecreaseHealth();

	  /*      foreach(GameObject heart in heartDisplay)
        {

        }*/	
	}
    
    public void IncreaseHealth()
    {
        
    }

    public void IncreaseHealth(int amount)
    {

    }

    public void DecreaseHealth()
    {
        
    }

    public void DecreaseHealth(int amount)
    {

    }

    public void setMaxHealth(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            addHeart();
        }

        currentHealth = amount * 2;
    }

    public void setHealth(int amount)
    {
        
    }

    public void addHeart()
    {
        GameObject newHeart = Instantiate(heartPrefab);
        newHeart.transform.SetParent(transform.Find("StatusBar").Find("HealthBar"));
        newHeart.transform.position = transform.Find("StatusBar").Find("HealthBar").position + new Vector3(heartDisplay.Count * 50, 0, 0);
        heartDisplay.Add(newHeart);
        healthIndex++;
        Debug.Log("HeartAdded at " + newHeart.transform.position);
        currentHealth = heartDisplay.Count * 2;
    }

    public void removeHeart()
    {
        if(heartDisplay.Count != 0)
        {
            heartDisplay.RemoveAt(heartDisplay.Count - 1);
        }
    }

    public void UpdateHealthBar(int currentHealth)
    {
        //heartDisplay[0].transform.GetChild(2).GetComponent<Image>().enabled = false;        //Disables the full heart
        //heartDisplay[0].transform.GetChild(1).GetComponent<Image>().enabled = false;        //Disables the half heart

        foreach(GameObject hrt in heartDisplay)
        {
            hrt.transform.GetChild(1).GetComponent<Image>().enabled = false;
            hrt.transform.GetChild(2).GetComponent<Image>().enabled = false;
        }


        for(int halfHealth = 0; halfHealth < currentHealth / 2; halfHealth++)
        {
            heartDisplay[halfHealth].transform.GetChild(2).GetComponent<Image>().enabled = true;        //Enables the full heart
            heartDisplay[halfHealth].transform.GetChild(1).GetComponent<Image>().enabled = true;        //Enables the half heart
        }

        Debug.Log("half current health = " + (float)currentHealth / 2.0f);

        if((float)currentHealth % 2.0f != 0.0f)
        {
            heartDisplay[(int)currentHealth / 2].transform.GetChild(1).GetComponent<Image>().enabled = true;        //Enables the half heart
            heartDisplay[(int)currentHealth / 2].transform.GetChild(2).GetComponent<Image>().enabled = false;       //Disables the full heart
        }
        else
        {
            heartDisplay[(int)currentHealth / 2].transform.GetChild(1).GetComponent<Image>().enabled = false;        //Disable the half heart
            heartDisplay[(int)currentHealth / 2].transform.GetChild(2).GetComponent<Image>().enabled = false;       //Disables the full heart
        }


    }

}