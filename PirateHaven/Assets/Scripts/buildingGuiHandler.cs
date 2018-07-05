using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class buildingGuiHandler : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler {


		private buildingManager bldMnger;
		public GameObject gameManager;
		public GameObject hoverCanvas;
		public RectTransform uiWrapper;

		public Image backDrop;

	    public Text nameText;
        public Image taskProgress;
        public Image taskBackground;
		public Image taskIcon;
		public Text taskText;

        public Image typeIcon;
		public Text peopleText;

        public Text employeesText;        

        public Button addWorkerButton;
        public Button removeWorkerButton;

		public Button addEmployeeButton;
		public Button removeEmployeeButton;
        
        public Image workerIcon;

        public Button upgradeBuildingButton;
        public Button deleteBuildingButton;

		public Camera MainCamera;
		public GameObject player;

		public RectTransform taskWrapper;
		public RectTransform standardWrapper;
		public RectTransform employmentWrapper;
		public RectTransform constructionWrapper;
		public RectTransform peopleWrapper;

		public bool onGUI = true;
		public bool openGUI;

		public bool mouseClicked;

		public bool guiStarted = false;

		public bool mouseOverBuilding;

		public void startGui() {

		guiStarted = true;

		gameManager = GameObject.FindGameObjectWithTag("gameManager");
		MainCamera = gameManager.GetComponent<guiManager>().MainCamera;

		//Creates a new hover canvas and defines the uiWrapper.

 		hoverCanvas = Instantiate(gameManager.GetComponent<guiManager>().hoverCanvas, gameObject.transform.position, gameObject.transform.rotation);
		hoverCanvas.transform.parent = this.transform;
		uiWrapper = hoverCanvas.transform.GetChild(0).GetComponent<RectTransform>();

		//Fetches all the UI elements from the newly created Hover Canvas.

		//Wrapper elememnts
		taskWrapper = hoverCanvas.transform.Find("wrapper/task").GetComponent<RectTransform>();
		standardWrapper = hoverCanvas.transform.Find("wrapper/standard").GetComponent<RectTransform>();
		employmentWrapper = hoverCanvas.transform.Find("wrapper/employment").GetComponent<RectTransform>();
		constructionWrapper = hoverCanvas.transform.Find("wrapper/construction").GetComponent<RectTransform>();
		peopleWrapper = hoverCanvas.transform.Find("wrapper/people").GetComponent<RectTransform>();

		//Task elements
		taskText = hoverCanvas.transform.Find("wrapper/task/taskText").GetComponent<Text>();
		taskProgress = hoverCanvas.transform.Find("wrapper/task/progressBackground/progressBar").GetComponent<Image>();
		taskIcon = hoverCanvas.transform.Find("wrapper/task/progressBackground/progressIcon").GetComponent<Image>();
		taskBackground = hoverCanvas.transform.Find("wrapper/task/progressBackground").GetComponent<Image>();

		//People elements
		typeIcon = hoverCanvas.transform.Find("wrapper/people/type_icon").GetComponent<Image>();
		peopleText = hoverCanvas.transform.Find("wrapper/people/people_count").GetComponent<Text>();

		//Standard elements
		upgradeBuildingButton = hoverCanvas.transform.Find("wrapper/standard/upgradeButton").GetComponent<Button>();
		deleteBuildingButton = hoverCanvas.transform.Find("wrapper/standard/deleteButton").GetComponent<Button>();
		nameText = hoverCanvas.transform.Find("wrapper/standard/buildingName").GetComponent<Text>();

		//Employee elements
		employeesText = hoverCanvas.transform.Find("wrapper/employment/employeesText").GetComponent<Text>();
		addEmployeeButton = hoverCanvas.transform.Find("wrapper/employment/addEmployeeButton").GetComponent<Button>();
		removeEmployeeButton = hoverCanvas.transform.Find("wrapper/employment/removeEmployeeButton").GetComponent<Button>();

		//Construction elements
		workerIcon = hoverCanvas.transform.Find("wrapper/construction/worker_icon").GetComponent<Image>();
		addWorkerButton = hoverCanvas.transform.Find("wrapper/construction/addWorkerButton").GetComponent<Button>();
		removeWorkerButton = hoverCanvas.transform.Find("wrapper/construction/removeWorkerButton").GetComponent<Button>();

		backDrop = hoverCanvas.transform.Find("wrapper/black_backdrop").GetComponent<Image>();

		addWorkerButton.onClick.AddListener(ClickToAddWorker);
        removeWorkerButton.onClick.AddListener(ClickToRemoveWorker);
        upgradeBuildingButton.onClick.AddListener(ClickToUpgrade);
    	deleteBuildingButton.onClick.AddListener(ClickToDelete);
		addEmployeeButton.onClick.AddListener(ClickToAddEmployee);
        removeEmployeeButton.onClick.AddListener(ClickToRemoveEmployee);

		standardWrapper.gameObject.SetActive(false);
		employmentWrapper.gameObject.SetActive(false);
		constructionWrapper.gameObject.SetActive(false);
		peopleWrapper.gameObject.SetActive(false);
		taskWrapper.gameObject.SetActive(false);

	}

	void start() {
	}
	

	void Update () {

		var buildingManager = GetComponent<buildingManager>();
		

		if(guiStarted == true) {

		var guiManager = gameManager.GetComponent<guiManager>();

		// Gets the center of the building, using the bounds of the collider, recalculates that center position to screen space and sets the wrapper to follow it.
		Vector3 buildingCenter = this.GetComponent<Collider>().bounds.center;
		Vector3 screenPos = MainCamera.WorldToScreenPoint(buildingCenter);
		uiWrapper.position = screenPos;

		if(buildingManager.currentTask != buildingManager.taskType.nothing ){

			int a = (int)buildingManager.currentTask;

			taskIcon.sprite = guiManager.taskIcons[a];
			taskBackground.sprite = guiManager.backGround[a];
			taskBackground.color = new Color(255,255,255);

			showUi(taskWrapper.gameObject);
		}
		else {
			hideUi(taskWrapper.gameObject);
		}

		if(openGUI == true) {

			if(buildingManager.currentTask == buildingManager.taskType.constructing) {
				showUi(constructionWrapper.gameObject);
				showUi(standardWrapper.gameObject);
				hideUi(upgradeBuildingButton.gameObject);
				hideUi(employmentWrapper.gameObject);
				hideUi(peopleWrapper.gameObject);

			}else if(buildingManager.currentTask == buildingManager.taskType.nothing) {
				hideUi(constructionWrapper.gameObject);
				showUi(standardWrapper.gameObject);
				showUi(upgradeBuildingButton.gameObject);

				if (GetComponent<housingScript>()) {
					showUi(peopleWrapper.gameObject);
					
				}
				if (GetComponent<attractionScript>()) {
					showUi(peopleWrapper.gameObject);
					showUi(employmentWrapper.gameObject);
				}

			}

		} else {
			hideUi(peopleWrapper.gameObject);
			hideUi(employmentWrapper.gameObject);
			hideUi(constructionWrapper.gameObject);
			hideUi(standardWrapper.gameObject);
		}

		if (Input.GetMouseButtonUp(0) && onGUI == false && openGUI == true) {

			openGUI = false;

		}
		}

	}

	void OnMouseOver() {
		
		if(guiStarted == true && Input.GetMouseButtonUp(0) && openGUI == false) {
			
			openGUI = true;
			onGUI = true;			

		} else if (guiStarted == true && Input.GetMouseButtonUp(0) && onGUI == false && openGUI == true) {

			openGUI = false;	

		}

	}

	void ClickToAddEmployee() {

	var bldMnger = GetComponent<buildingManager>();
	var emplScript = GetComponent<employeeScript>();

        if (gameManager.GetComponent<gameManagerScript>().availableWorkers > 0 && bldMnger.currentAssignedWorkers.Count < emplScript.maxEmployees)
        {
			bldMnger.addClosestWorker();
			employeesText.text = bldMnger.currentAssignedWorkers.Count.ToString() + "/" + emplScript.maxEmployees.ToString();

        } 		
	}

	    void ClickToRemoveEmployee()
    {
	
		var bldMnger = GetComponent<buildingManager>();
		var emplScript = GetComponent<employeeScript>();

        if (bldMnger.currentAssignedWorkers.Count > 0)
        {
            bldMnger.removeWorker();
			employeesText.text = bldMnger.currentAssignedWorkers.Count.ToString() + "/" + emplScript.maxEmployees.ToString();
        }
	}

	
    void ClickToAddWorker()
    {
		var bldMnger = GetComponent<buildingManager>();

        if (gameManager.GetComponent<gameManagerScript>().availableWorkers > 0 && bldMnger.currentAssignedWorkers.Count < bldMnger.maxBuilders)
        {
			bldMnger.addClosestWorker();
			taskText.text = bldMnger.currentAssignedWorkers.Count.ToString() + "/" + bldMnger.maxBuilders.ToString();

        } 
    }

    void ClickToRemoveWorker()
    {
	
	var bldMnger = GetComponent<buildingManager>();

        if (bldMnger.currentAssignedWorkers.Count > 0)
        {
            bldMnger.removeWorker();
			taskText.text = bldMnger.currentAssignedWorkers.Count.ToString() + "/" + bldMnger.maxBuilders.ToString();
        }
	}

	void ClickToUpgrade()
    {

		var bldMnger = GetComponent<buildingManager>();
        //gameManager.GetComponent<gameManagerScript>().availableWorkers.Count += bldMnger.currentAssignedWorkers;

		var allWorkers = bldMnger.currentWorkers.Count;
		for (int i = 0; i < allWorkers; i++)
			{
				bldMnger.removeWorker();
			}

		bldMnger.startConstruction();
        
    }

	
    void ClickToDelete()
    {
		var bldMnger = GetComponent<buildingManager>();
		gameManager.GetComponent<gameManagerScript>().availableWorkers += bldMnger.currentAssignedWorkers.Count;

		if (GetComponent<housingScript>()) {
			gameManager.GetComponent<gameManagerScript>().citizensWithHome-= GetComponent<housingScript>().currentLiving;
		}
		if (GetComponent<attractionScript>()) {
			GetComponent<attractionScript>().closeAttraction();
		}

		bldMnger.destroyBuilding();
		
	}
    

	void OnMouseExit () {
		onGUI = false;
	}

	public void OnPointerExit(PointerEventData pointerEventData) {
		onGUI = false;
	}

	public void OnPointerEnter(PointerEventData pointerEventData) {
		onGUI = true;
	}

	void hideUi(GameObject element) {
		element.SetActive(false);
	}

	void showUi(GameObject element) {
		element.SetActive(true);
	}


}

