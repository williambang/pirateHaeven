using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class buildingStats : MonoBehaviour {

    public string name;
    public int moneyCost;
    public int ressourcesCost;
    public int rumCost;

    public bool constructionCompleted = false;
    public float constructionAmount;

    public float attractiveness;
    public int maxCapacity;
    public int dailyCost;
    public int dailyIncome;
    public int currentCapacity;
    public int buildingLevel = 1;
    public GameObject[] currentWorkers;

    public int totalVisitors;

    public GameObject[] famousVisitors;

    public bool overlapping = false;

	// Use this for initialization
	void Start () {


	}
	
	// Update is called once per frame
	void Update () {
	
      
	}

    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "placedBuilding")
        {

            overlapping = true;

        }

        

    }

    private void OnTriggerExit(Collider other)
    {

        if (other.tag == "placedBuilding")
        {

            overlapping = false;

        }

    }

}
