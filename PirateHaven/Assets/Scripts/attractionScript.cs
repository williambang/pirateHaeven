using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attractionScript : MonoBehaviour {

	[System.Serializable]
    public class visiting
    {
        public GameObject Visitor;
        public float stayingTime;
    }

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

	void addVisitor(GameObject newVisitor, float time) {

		currentVisitors.Add( new visiting 
		{
			Visitor = newVisitor,
			stayingTime = time
		});
	
		var i = GetComponent<buildingManager>().buildingLevel - 1;
		GetComponent<buildingGuiHandler>().peopleText.text = currentVisitors.ToString() + "/" + maxVisitors[i].ToString();

	}
}
