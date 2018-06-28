using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class buildingStats : MonoBehaviour {

    public GameObject player;
    public GameObject buildingSettings;

    public string buildingName;
    public string ogBuildingName;
    public int moneyCost;
    public int ressourcesCost;
    public int rumCost;
    public int upgradeCost;

    public GameObject currentBuilding;
    public GameObject plot;

    public bool buildingSettingsOpen = false;
    public bool constructionCompleted = false;
    public bool constructionStarted = false;
    public float constructionCurrentAmount;
    public float constructionAmount;
    public Image constructionBar;

    public Button addWorkerButton;
    public Button removeWorkerButton;

    private bool addEmployeeClicked = false;
    private int amountOfLetters;

    public bool buildingIsHousing;

    public GameObject canvasObj;
    public GameObject hoverCanvas;
    public GameObject currentCanvas;

    public GameObject worksite;

    public GameObject[] level;

    public GameObject level01;
    public GameObject level02;
    public GameObject level03;

    public GameObject gm;

    public float attractiveness;
    
    public int dailyCost;
    public int dailyIncome;

    public int currentLiving;
    public int maxLiving;

    public int currentVisitors;
    public int maxVisitors;

    public int currentEmployed;
    public int maxEmployed;

    public int currentWorkers;
    public int maxWorkers;

    public int buildingLevel = 0;

    //public GameObject[] currentWorkers;

    public int totalVisitors;

    public GameObject[] famousVisitors;

    public bool overlapping = false;

	// Use this for initialization
	void Start () {

        gm = GameObject.FindGameObjectWithTag("gameManager");
        player = GameObject.FindGameObjectWithTag("MainCamera");

        ogBuildingName = buildingName;

    }
	
	// Update is called once per frame
	void Update () {

        var workersWithHome = gm.GetComponent<gameManagerScript>().workersWithHome;
        var workers = gm.GetComponent<gameManagerScript>().workers;
        var workersWithoutHome = (workers -= workersWithHome);

        if (buildingIsHousing && constructionCompleted == true && currentLiving != maxLiving)
        {

            if (workersWithoutHome > 0 && workers != 0)
            {

                // Add worker to living quarters
                currentLiving = currentLiving + 1;

                // Add worker with home to workersWithHome in Game Manager

                gm.GetComponent<gameManagerScript>().workersWithHome = workersWithHome + 1;


            }

        }

        if (constructionStarted == true && constructionCompleted == false && constructionCurrentAmount == 0)
        {

            Vector3 newCanvasPos = new Vector3(gameObject.transform.position.x, 3.5f, gameObject.transform.position.z);

            currentCanvas = Instantiate(canvasObj, newCanvasPos, gameObject.transform.rotation);
            currentCanvas.transform.parent = transform;
            constructionBar = currentCanvas.transform.GetChild(0).transform.GetChild(0).GetComponent<Image>();
            addWorkerButton = currentCanvas.transform.GetChild(2).GetComponent<Button>();
            removeWorkerButton = currentCanvas.transform.GetChild(1).GetComponent<Button>();
            addWorkerButton.onClick.AddListener(ClickToAddWorker);
            removeWorkerButton.onClick.AddListener(ClickToRemoveWorker);



            StartCoroutine(constructionAddPercentage());
            constructionCurrentAmount += 1;



        }

        if (constructionStarted == true)
        {

            currentCanvas.transform.GetChild(3).GetComponent<Text>().text = currentWorkers.ToString();
            

        }

        

        if (constructionCurrentAmount >= constructionAmount && constructionStarted == true)
        {

            constructionCompleted = true;
            Destroy(currentCanvas);

            if (buildingLevel == 0)
            {
                buildingLevel = 1;
                constructionCurrentAmount = 0;
                constructionAmount *= 2;
                GameObject building = Instantiate(level[buildingLevel], this.transform.position, this.transform.rotation);

                building.transform.parent = this.transform;
                Destroy(currentBuilding);
                currentBuilding = building;

                constructionBar.fillAmount = 0;
                Destroy(currentCanvas);

                GameObject.FindGameObjectWithTag("gameManager").GetComponent<gameManagerScript>().availableWorkers += currentWorkers;
                currentWorkers = 0;
                
                

            }else if (buildingLevel == 1)
            {

                constructionCurrentAmount = 0;
                constructionAmount *= 2;
                GameObject building = Instantiate(level02, gameObject.transform.position, gameObject.transform.rotation);

                building.transform.parent = this.transform;
                Destroy(currentBuilding);
                currentBuilding = building;

                GameObject.FindGameObjectWithTag("gameManager").GetComponent<gameManagerScript>().availableWorkers += currentWorkers;
                currentWorkers = 0;

                buildingLevel = 2;

            } else if (buildingLevel == 2)
            {

                constructionCurrentAmount = 0;
                constructionAmount *= 2;
                GameObject building = Instantiate(level03, gameObject.transform.position, gameObject.transform.rotation);

                building.transform.parent = this.transform;
                Destroy(currentBuilding);
                currentBuilding = building;

                GameObject.FindGameObjectWithTag("gameManager").GetComponent<gameManagerScript>().availableWorkers += currentWorkers;
                currentWorkers = 0;

                buildingLevel = 3;

            }

            constructionStarted = false;

        }

      
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

    private void OnMouseEnter()
    {

        if (constructionCompleted == true)
        {

            Vector3 newCanvasPos = new Vector3(gameObject.transform.position.x, (this.GetComponent<BoxCollider>().size.y - 2f), gameObject.transform.position.z);

            currentCanvas = Instantiate(hoverCanvas, newCanvasPos, gameObject.transform.rotation);
            currentCanvas.transform.parent = transform;


            if (maxLiving > 0 && buildingIsHousing && maxEmployed <= 0)
            {

                currentCanvas.transform.GetChild(0).GetComponent<InputField>().text = buildingName.ToString();
                currentCanvas.transform.GetChild(1).GetComponent<Text>().text = currentLiving.ToString() + "/" + maxLiving.ToString();

                Button upgradeBuildingButton = currentCanvas.transform.GetChild(2).gameObject.GetComponent<Button>();
                Button deleteBuildingButton = currentCanvas.transform.GetChild(3).gameObject.GetComponent<Button>();

                upgradeBuildingButton.onClick.AddListener(ClickToUpgrade);
                deleteBuildingButton.onClick.AddListener(ClickToDelete);

            } else if (maxVisitors > 0 && !buildingIsHousing && maxEmployed <= 0)
            {

                currentCanvas.transform.GetChild(0).GetComponent<InputField>().text = buildingName.ToString();
                currentCanvas.transform.GetChild(1).GetComponent<Text>().text = currentVisitors.ToString() + "/" + maxVisitors.ToString();

                Button upgradeBuildingButton = currentCanvas.transform.GetChild(2).gameObject.GetComponent<Button>();
                Button deleteBuildingButton = currentCanvas.transform.GetChild(3).gameObject.GetComponent<Button>();

                upgradeBuildingButton.onClick.AddListener(ClickToUpgrade);
                deleteBuildingButton.onClick.AddListener(ClickToDelete);

            } else if (maxVisitors > 0 && !buildingIsHousing && maxEmployed > 0)
            {

                currentCanvas.transform.GetChild(0).GetComponent<InputField>().text = buildingName.ToString();
                currentCanvas.transform.GetChild(1).GetComponent<Text>().text = currentVisitors.ToString() + "/" + maxVisitors.ToString();
                currentCanvas.transform.GetChild(4).gameObject.SetActive(true);
                currentCanvas.transform.GetChild(5).gameObject.SetActive(true);
                currentCanvas.transform.GetChild(6).gameObject.SetActive(true);
                currentCanvas.transform.GetChild(6).GetComponent<Text>().text = currentEmployed.ToString();


                Button addEmployeeButton = currentCanvas.transform.GetChild(5).gameObject.GetComponent<Button>();
                Button removeEmployeeButton = currentCanvas.transform.GetChild(4).gameObject.GetComponent<Button>();

                Button upgradeBuildingButton = currentCanvas.transform.GetChild(2).gameObject.GetComponent<Button>();
                Button deleteBuildingButton = currentCanvas.transform.GetChild(3).gameObject.GetComponent<Button>();

                upgradeBuildingButton.onClick.AddListener(ClickToUpgrade);
                deleteBuildingButton.onClick.AddListener(ClickToDelete);

                addEmployeeButton.onClick.AddListener(ClickToAddEmployee);
                removeEmployeeButton.onClick.AddListener(ClickToRemoveEmployee);

            }
            else if (maxVisitors <= 0 && !buildingIsHousing && maxEmployed > 0)
            {

                currentCanvas.transform.GetChild(0).GetComponent<InputField>().text = buildingName.ToString();

                currentCanvas.transform.GetChild(4).gameObject.SetActive(true);
                currentCanvas.transform.GetChild(5).gameObject.SetActive(true);
                currentCanvas.transform.GetChild(6).gameObject.SetActive(true);
                currentCanvas.transform.GetChild(6).GetComponent<Text>().text = currentEmployed.ToString();


                Button addEmployeeButton = currentCanvas.transform.GetChild(5).gameObject.GetComponent<Button>();
                Button removeEmployeeButton = currentCanvas.transform.GetChild(4).gameObject.GetComponent<Button>();

                Button upgradeBuildingButton = currentCanvas.transform.GetChild(2).gameObject.GetComponent<Button>();
                Button deleteBuildingButton = currentCanvas.transform.GetChild(3).gameObject.GetComponent<Button>();

                upgradeBuildingButton.onClick.AddListener(ClickToUpgrade);
                deleteBuildingButton.onClick.AddListener(ClickToDelete);

                addEmployeeButton.onClick.AddListener(ClickToAddEmployee);
                removeEmployeeButton.onClick.AddListener(ClickToRemoveEmployee);

            }
            else
            {

                currentCanvas.transform.GetChild(0).GetComponent<InputField>().text = buildingName.ToString();

                Button upgradeBuildingButton = currentCanvas.transform.GetChild(2).gameObject.GetComponent<Button>();
                Button deleteBuildingButton = currentCanvas.transform.GetChild(3).gameObject.GetComponent<Button>();

                upgradeBuildingButton.onClick.AddListener(ClickToUpgrade);
                deleteBuildingButton.onClick.AddListener(ClickToDelete);

            }
        }
    }

 

    void ClickToAddWorker()
    {
        if (GameObject.FindGameObjectWithTag("gameManager").GetComponent<gameManagerScript>().availableWorkers > 0 && currentWorkers < maxWorkers)
        {
            GameObject.FindGameObjectWithTag("gameManager").GetComponent<gameManagerScript>().availableWorkers -= 1;

            currentWorkers = currentWorkers + 1;
            currentCanvas.transform.GetChild(6).GetComponent<Text>().text = currentWorkers.ToString();

        }
        

    }

    void ClickToRemoveWorker()
    {
        if (currentWorkers > 0)
        {

            GameObject.FindGameObjectWithTag("gameManager").GetComponent<gameManagerScript>().availableWorkers += 1;

            currentWorkers = currentWorkers - 1;
            currentCanvas.transform.GetChild(6).GetComponent<Text>().text = currentWorkers.ToString();

        }


    }

    void ClickToAddEmployee()
    {

        if (GameObject.FindGameObjectWithTag("gameManager").GetComponent<gameManagerScript>().availableWorkers > 0 && currentEmployed < maxEmployed)
        {
            GameObject.FindGameObjectWithTag("gameManager").GetComponent<gameManagerScript>().availableWorkers -= 1;

            currentEmployed = currentEmployed + 1;
            currentCanvas.transform.GetChild(6).GetComponent<Text>().text = currentEmployed.ToString();
        }


    }

    void ClickToRemoveEmployee()
    {
        if (currentEmployed > 0)
        {

            GameObject.FindGameObjectWithTag("gameManager").GetComponent<gameManagerScript>().availableWorkers += 1;

            currentEmployed = currentEmployed - 1;
            currentCanvas.transform.GetChild(6).GetComponent<Text>().text = currentEmployed.ToString();

        }

        
    }

    void ClickToUpgrade()
    {

        if (constructionCompleted)
        {

            if (gm.GetComponent<gameManagerScript>().money >= upgradeCost) {

                Destroy(currentCanvas);
                Destroy(currentBuilding);

                Vector3 newCanvasPos = new Vector3(gameObject.transform.position.x, 3.5f, gameObject.transform.position.z);

                currentCanvas = Instantiate(canvasObj, newCanvasPos, gameObject.transform.rotation);
                currentCanvas.transform.parent = transform;
                constructionBar = currentCanvas.transform.GetChild(0).transform.GetChild(0).GetComponent<Image>();
                addWorkerButton = currentCanvas.transform.GetChild(2).GetComponent<Button>();
                removeWorkerButton = currentCanvas.transform.GetChild(1).GetComponent<Button>();
                addWorkerButton.onClick.AddListener(ClickToAddWorker);
                removeWorkerButton.onClick.AddListener(ClickToRemoveWorker);

                constructionCompleted = false;
                constructionStarted = true;

                
                currentBuilding = Instantiate(worksite, gameObject.transform.position, gameObject.transform.rotation);

                StartCoroutine(constructionAddPercentage());
                constructionCurrentAmount += 1;



            }
            

        }


    }

    void ClickToDelete()
    {
         if (constructionCompleted)
          {

              if (currentEmployed > 0)
              {

                  gm.GetComponent<gameManagerScript>().availableWorkers += currentEmployed;

                  if (currentVisitors > 0)
                  {

                      gm.GetComponent<gameManagerScript>().availableVisitors += currentVisitors;
                      Destroy(gameObject);

                  } else
                  {

                      Destroy(gameObject);

                  }


              } else if (currentLiving > 0)
              {

                  gm.GetComponent<gameManagerScript>().workersWithHome -= currentLiving;
                  Destroy(gameObject);

              } else if (currentVisitors > 0)
              {

                  gm.GetComponent<gameManagerScript>().availableVisitors += currentVisitors;
                  Destroy(gameObject);

              } else
              {

                  Destroy(gameObject);

              }


          }

          if (constructionStarted)
          {

                  gm.GetComponent<gameManagerScript>().availableWorkers += currentWorkers;
                  Destroy(gameObject);

          }




    }


    private void OnMouseOver()
    {

        if (player.GetComponent<playerScript>().buildMode == false && Input.GetMouseButtonUp(0) && constructionCompleted == true && buildingSettingsOpen == false && addEmployeeClicked == false)
        {
            /*
            buildingSettingsOpen = true;
            GameObject bs = Instantiate(buildingSettings, buildingSettings.transform.position, buildingSettings.transform.rotation) as GameObject;

            bs.GetComponent<buildingSettings>().buildingObject = this.gameObject;

            bs.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, false);

            bs.GetComponent<buildingSettings>().buildingName = buildingName.ToString();
            bs.transform.GetChild(2).GetComponent<InputField>().text = buildingName.ToString();*/

        }

        if (constructionCompleted)
        {

            if (maxVisitors > 0 && !buildingIsHousing && currentCanvas != null)
            {

                currentCanvas.transform.GetChild(4).GetComponent<Text>().text = currentEmployed.ToString();
                buildingName = currentCanvas.transform.GetChild(0).GetComponent<InputField>().text;

            }

        }
        

    }

    private void OnMouseExit()
    {

        if (constructionCompleted == true)
        {



            foreach (char c in buildingName)
            {

                if (char.IsLetterOrDigit(c))
                {

                    amountOfLetters += 1;
                }
            }


            if (buildingName.Contains(" ") && amountOfLetters == 0)
            {

                buildingName = ogBuildingName;
                amountOfLetters = 0;

            } else if (buildingName == string.Empty)
            {
                buildingName = ogBuildingName;
                amountOfLetters = 0;
            }

            Destroy(currentCanvas);
            currentCanvas = null;

        }

    }

    IEnumerator constructionAddPercentage()
    {


        yield return new WaitForSeconds(1);

        constructionCurrentAmount += (currentWorkers * GameObject.FindGameObjectWithTag("gameManager").GetComponent<gameManagerScript>().workerLevel);
        constructionBar.fillAmount = (constructionCurrentAmount / constructionAmount);

        if (constructionCurrentAmount <= constructionAmount)
        {

            StartCoroutine(constructionAddPercentage());

        }
        
    }



}
