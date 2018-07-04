using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class buildingManager : MonoBehaviour {

	public class working {
		GameObject workerInside;
		int Level;
	}

	public enum taskType {constructing, onFire, producing, fighting, brewing, sugaring, nothing};
	public enum bldType {housing, attraction, production, utility};

	public bool overlapping;
	public bool preview;

	public GameObject gm;

	public GameObject entrancePoint;

	public bldType buildingType = bldType.attraction;

	public string buildingName;
	public string ogBuildingName;
	public int attractiveness;
	public int buildingLevel = 0;

	public int[] constructionCost;
	public int[] ressourcesCost;

	public float taskProgressMax;
	public float currentTaskProgress;
	public taskType currentTask = taskType.nothing;

	public List<GameObject> currentAssignedWorkers = new List<GameObject>();
	public List<GameObject> currentWorkers = new List<GameObject>();
    public int maxBuilders;
	public int maxFiremen;

	public bool harborBuilding = false;

	public bool firestarter = false;

	public GameObject currentBuilding;
	public GameObject[] worksite;
    public GameObject[] level;


    void Awake()
    {
        gm = GameObject.FindGameObjectWithTag("gameManager");
		
    }

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

		if(firestarter == true) {
			startFire();
			firestarter = false;
		}

	}

    //Construction Related Functions;

	public void startConstruction() {

		gm = GameObject.FindGameObjectWithTag("gameManager");	
	
		if (buildingLevel < level.Length) {

			currentTask = taskType.constructing;
			gm.GetComponent<gameManagerScript>().money -= constructionCost[buildingLevel];
			Destroy(currentBuilding);
			currentBuilding = Instantiate(worksite[buildingLevel], gameObject.transform.position, gameObject.transform.rotation);
			currentBuilding.transform.parent = this.transform;
			StartCoroutine(constructionHandler());
			this.GetComponent<buildingGuiHandler>().taskText.text = "0/" + maxBuilders.ToString();

		}

	}

	    IEnumerator constructionHandler()
    {

        yield return new WaitForSeconds(0.01f);

		//Add construction progress and update progress bar.
        currentTaskProgress += currentWorkers.Count * 0.1f;
		this.GetComponent<buildingGuiHandler>().taskProgress.fillAmount = (currentTaskProgress / taskProgressMax);

        if (currentTaskProgress < taskProgressMax)
        {

            StartCoroutine(constructionHandler());
        }
		else{

			currentTask = taskType.nothing;

			buildingLevel += 1;
			currentTaskProgress = 0;
			GameObject building = Instantiate(level[buildingLevel - 1], this.transform.position, this.transform.rotation);

			building.transform.parent = this.transform;
			Destroy(currentBuilding);
			currentBuilding = building; 

			gm.GetComponent<gameManagerScript>().availableWorkers += currentAssignedWorkers.Count;
			
			Debug.Log(currentWorkers.Count);

			var allWorkers = currentWorkers.Count;
			for (int i = 0; i < allWorkers; i++)
            {
                removeWorker();
            } 			

		}
        
    }

	//Fire Related funcitons

	public void startFire() {

		currentTask = taskType.onFire;
		currentTaskProgress = 0;
		StopAllCoroutines();
		StartCoroutine(fireHandler());
		this.GetComponent<buildingGuiHandler>().taskText.text = "0/" + maxFiremen.ToString();
	}

	IEnumerator fireHandler()
    {

        yield return new WaitForSeconds(0.01f);

        currentTaskProgress += 0.1f;

		this.GetComponent<buildingGuiHandler>().taskProgress.fillAmount = (currentTaskProgress / taskProgressMax);
		Debug.Log("ADD FILL AMOUNT!");

        if (currentTaskProgress < taskProgressMax)
        {
            StartCoroutine(fireHandler());
        }
		else {
			destroyBuilding();
		}
    }

	public void destroyBuilding() {

		var allWorkers = currentWorkers.Count;
		for (int i = 0; i < allWorkers; i++)
			{
				removeWorker();
			}
		Destroy(this.gameObject);
	}

	//Worker Related Functions

	public void addClosestWorker()
    {
		gm.GetComponent<gameManagerScript>().availableWorkers -= 1;
		

        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Worker");
        GameObject closestWorker = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in gos)
        {
			if(go.GetComponent<workerScript>().currentState == workerScript.workerState.wandering) {

            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closestWorker = go;
                distance = curDistance; 
            }
		  }
        }
		closestWorker.GetComponent<workerScript>().goToWork(entrancePoint);
		currentAssignedWorkers.Add( closestWorker );
    }

	public void removeWorker() {

		gm.GetComponent<gameManagerScript>().availableWorkers += 1; //Gamemanager

		if(currentAssignedWorkers.Count == currentWorkers.Count) {
				var lastWorker = currentWorkers[currentWorkers.Count - 1];
				lastWorker.SetActive(true);
				lastWorker.GetComponent<workerScript>().resetWorker();
				currentWorkers.Remove( lastWorker );
		}

		var lastAssignedWorker = currentAssignedWorkers[currentAssignedWorkers.Count - 1];
		currentAssignedWorkers.Remove( lastAssignedWorker );

	}

}

