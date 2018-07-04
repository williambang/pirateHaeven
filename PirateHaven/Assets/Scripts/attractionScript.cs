using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attractionScript : MonoBehaviour {

	[System.Serializable]
    public class visiting
    {
        public GameObject visitor;
        public float stayingTime;
    }

	public float visitingMultiplier = 10;

	public List<visiting> currentVisitors;
	public int[] maxVisitors;

	private bool attractionOpen = false;

	public GameObject gm;

	public gameManagerScript.Attraction self;

	// Use this for initialization
	void Start () {


	}
	
	// Update is called once per frame
	void Update () {
		
		var buildingManager = GetComponent<buildingManager>();

		if (buildingManager.currentTask == buildingManager.taskType.nothing && buildingManager.buildingLevel > 0 && attractionOpen == false)
        {
			startAttraction();
        }
		else if (buildingManager.currentTask == buildingManager.taskType.constructing == true && currentVisitors.Count != 0) {

			closeAttraction();

		}

	}

	public void startAttraction() {

		// Get References
		gm = GameObject.FindGameObjectWithTag("gameManager");	
		var buildingManager = GetComponent<buildingManager>();
		var guiHandler = GetComponent<buildingGuiHandler>();
		var guiManager = gm.GetComponent<guiManager>();
		
		//Setup UI
		int a = (int)buildingManager.buildingType;
		guiHandler.typeIcon.sprite = guiManager.typeIcons[a]; 
		guiHandler.typeIcon.color = new Color(255,255,255);

		GetComponent<buildingGuiHandler>().peopleText.text = currentVisitors.Count.ToString() + "/" + maxVisitors[0].ToString();

		// Add to list of attractions
		var attractionList = gm.GetComponent<gameManagerScript>().attractionBuildings;

		self = new gameManagerScript.Attraction
		{
			building = this.gameObject,
			attract = GetComponent<buildingManager>().attractiveness
		};

		attractionList.Add(self);

		attractionOpen = true;
	}

	public void closeAttraction() {
		
		gm = GameObject.FindGameObjectWithTag("gameManager");
		gm.GetComponent<gameManagerScript>().attractionBuildings.Remove(self);

		for (int i = 0; i < currentVisitors.Count; i++)
			{
				//
			}
	}

	public void addVisitor(GameObject newVisitor) {

		var visitorStayingTime = Random.Range(10,12);

		currentVisitors.Add( new visiting 
		{
			visitor = newVisitor,
			stayingTime = visitorStayingTime
		});
	
		var i = GetComponent<buildingManager>().buildingLevel - 1;
		GetComponent<buildingGuiHandler>().peopleText.text = currentVisitors.Count.ToString() + "/" + maxVisitors[i].ToString();

		newVisitor.SetActive(false);

	}

    IEnumerator attractionVisit(float stayingTime)
    {
        yield return new WaitForSeconds(stayingTime * visitingMultiplier);
        leaveAttraction();

    }

    public void leaveAttraction() {
        Debug.Log("LEAVING ATTRACTION!");
    }
}
