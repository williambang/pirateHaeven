using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class employeeScript : MonoBehaviour {
	
	public int maxEmployees;
	public GameObject gm;

	public float visitorServiceCost;
	public float naturalRessources;
	public float rum;

	public float earningRate;
	public float serviceTime;
	public float serviceSpeed;

<<<<<<< HEAD

=======
>>>>>>> aae17aaa00ddc2517c85a144ade1dd7d0843a08b
	public float currentEarnings;
	public float earnings;

	// Use this for initialization
	void Start () {
		
		gm = GameObject.FindGameObjectWithTag("gameManager");

		if (GetComponent<buildingGuiHandler>().guiStarted == true) {

			GetComponent<buildingGuiHandler>().employeesText.text = "0/" + maxEmployees.ToString();

		}

		StartCoroutine(earningsLoop());

	}

	public void openForWork() {

		gm = GameObject.FindGameObjectWithTag("gameManager");	

	}

	IEnumerator earningsLoop () {

		Debug.Log("Earning loop start");

		var buildingManager = GetComponent<buildingManager>();
		var visitorScript = GetComponent<attractionScript>();

		if (buildingManager.currentTask != buildingManager.taskType.constructing && buildingManager.currentWorkers.Count > 0) {

			if (GetComponent<attractionScript>()) {


				if (visitorScript.currentVisitors.Count > 0) {

					earnings = visitorScript.currentVisitors.Count * visitorServiceCost;
					
				}

			} else {

				earnings = buildingManager.buildingLevel * naturalRessources;
				earnings = buildingManager.buildingLevel * rum;

			}

<<<<<<< HEAD
=======
			//float a = serviceSpeed * 0.01f;
			//Debug.Log(serviceSpeed);

>>>>>>> aae17aaa00ddc2517c85a144ade1dd7d0843a08b
			earningRate = (1.0f / serviceSpeed) * (1.0f / buildingManager.currentWorkers.Count);

			currentEarnings += earnings;

<<<<<<< HEAD
			Debug.Log("Add " + earnings + " to Current Earnings");

		} else {
=======
		} else { 
>>>>>>> aae17aaa00ddc2517c85a144ade1dd7d0843a08b

			earningRate = 1;

		}

		Debug.Log(earningRate);
		Debug.Log(serviceTime);



		yield return new WaitForSeconds(earningRate * serviceTime);

<<<<<<< HEAD
		Debug.Log(earningRate * serviceTime);

=======
>>>>>>> aae17aaa00ddc2517c85a144ade1dd7d0843a08b
		StartCoroutine(earningsLoop());

	}
	
}
