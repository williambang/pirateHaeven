using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class buildingStats : MonoBehaviour {

    public GameObject player;
    public GameObject buildingSettings;

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

        player = GameObject.FindGameObjectWithTag("MainCamera");

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

    private void OnMouseOver()
    {
        
        if (player.GetComponent<playerScript>().buildMode == false && Input.GetMouseButtonUp(0))
        {

            GameObject bs = Instantiate(buildingSettings, buildingSettings.transform.position, buildingSettings.transform.rotation) as GameObject;

            bs.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, false);

            bs.GetComponent<buildingSettings>().buildingName.text = name;
            bs.GetComponent<buildingSettings>().buildingObject = this.gameObject;

        }

    }

}
