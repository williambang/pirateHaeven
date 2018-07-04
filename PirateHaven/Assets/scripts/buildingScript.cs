using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buildingScript : MonoBehaviour {



    public GameObject gm;

    public bool buildMode = false;
    public bool onPlot = false;
    public bool clicked = false;

    GameObject[] grounds;
    public GameObject ground;

    //public List<GameObject> plots = new List<GameObject>();

    public GameObject plot;

    public GameObject objectToBuild;
    public GameObject liveObjectToBuild;

    Ray markerRay;
    Ray hoverRay;
    RaycastHit planeHit;

    private Collider groundColl;
    private Vector3 posMarker;

    private bool previewChanging = false;

    // Use this for initialization
    void Start () {

        
        gm = GameObject.FindGameObjectWithTag("gameManager");

        grounds = GameObject.FindGameObjectsWithTag("ground");
        ground = grounds[0];


        

    }

    // Update is called once per frame
    void Update()
    {

        if (buildMode == true)
        {

            if (liveObjectToBuild != null)
            {

                    // Marker Position and Rotation 

                    markerRay = Camera.main.ScreenPointToRay(Input.mousePosition);

                    groundColl = ground.GetComponent<MeshCollider>();

                    if (groundColl.Raycast(markerRay, out planeHit, 100.0f))
                    {

                        posMarker = new Vector3(planeHit.point.x, 0.1f, planeHit.point.z);
                        this.transform.position = posMarker;

                        clickToBuild();
                        clicked = false;

                    }

            }
        }

    }

    void clickToBuild() {

    if (Input.GetMouseButton(0) && clicked != true) {

        var gmScript = gm.GetComponent<gameManagerScript>();

         if (liveObjectToBuild != null)
        {
            var buildingManager = liveObjectToBuild.GetComponent<buildingManager>();

                if (plot != null && previewChanging == false && liveObjectToBuild.GetComponent<buildingManager>().overlapping == false)
                {
                    if (gmScript.money >= buildingManager.constructionCost[0])
                    {

                        if (gmScript.ressources >= buildingManager.ressourcesCost[0])
                        {

                            // #1 Build structure framework with no model & and set define build stats. 

                            GameObject liveBuilding = Instantiate(objectToBuild, liveObjectToBuild.transform.position, liveObjectToBuild.transform.rotation);
                            var liveBuildingManager = liveBuilding.GetComponent<buildingManager>();


                            // #2 Build construction site and parent it under live building & and set as current building.

                            GameObject constructionSite = liveBuildingManager.worksite[0];
                            GameObject liveConstructionSite = Instantiate(constructionSite, liveObjectToBuild.transform.position, liveObjectToBuild.transform.rotation);
                            liveConstructionSite.transform.parent = liveBuilding.transform;
                            liveBuildingManager.currentBuilding = liveConstructionSite;

                            // #3 Calculate cost between building and Game Manager

                            gmScript.money -= liveBuildingManager.constructionCost[0];
                            gmScript.attractiveness += liveBuildingManager.attractiveness;
                            gmScript.ressources -= liveBuildingManager.ressourcesCost[0];

                            // #4 Create UI for building
                            liveBuilding.GetComponent<buildingGuiHandler>().startGui();


                            // #5 Set tags and tie up loose ends.

                            if (!liveBuildingManager.harborBuilding)
                            {
                                liveBuilding.tag = "placedBuilding";
                            } else
                            {
                                liveBuilding.tag = "dock";
                                liveBuilding.transform.parent = GameObject.FindGameObjectWithTag("water").transform;
                            }

                                liveBuildingManager.startConstruction();

                            

                            // Debug.Log("Object instantiated");
                            plot = null;

                            Destroy(liveObjectToBuild);
                            liveObjectToBuild = null;
                            buildingManager = null;
                            clicked = true;

                        }
                    }
                }
            }
        }
        
    }
  
    public void setPreview (GameObject building)
    {
        previewChanging = true;

        Destroy(liveObjectToBuild);

        objectToBuild = building;

        liveObjectToBuild = Instantiate(objectToBuild, this.transform.position, Quaternion.identity);
        liveObjectToBuild.transform.parent = this.transform;
        GameObject tempCanvas = liveObjectToBuild.GetComponent<buildingGuiHandler>().hoverCanvas;
        Destroy(tempCanvas); 

        previewChanging = false;

        //Spawning Level 01 Building for preview.

        GameObject previewBuild = objectToBuild.GetComponent<buildingManager>().level[0];

        GameObject livePreviewBuild = Instantiate(previewBuild, liveObjectToBuild.transform.position, liveObjectToBuild.transform.rotation);
        livePreviewBuild.transform.parent = liveObjectToBuild.transform;

    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "plot" && other.GetComponent<pointScript>().occupied == false && liveObjectToBuild != null && liveObjectToBuild.GetComponent<buildingManager>().harborBuilding == false)
        {
/*
           Renderer rend = liveObjectToBuild.transform.GetChild(0).GetComponent<Renderer>();

           rend.material.shader = Shader.Find("_Color");
           rend.material.SetColor("_Color", Color.green);

           rend.material.shader = Shader.Find("Specular");
          rend.material.SetColor("_SpecColor", Color.green);*/

         //   Debug.Log(other);

            plot = other.gameObject;
            liveObjectToBuild.transform.position = plot.transform.position;

            liveObjectToBuild.transform.parent = plot.transform;
  
            liveObjectToBuild.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);


        } else if (other.tag == "dockPlot" && other.GetComponent<pointScript>().occupied == false && liveObjectToBuild != null && liveObjectToBuild.GetComponent<buildingManager>().harborBuilding == true)
        {
            /*
            Renderer rend = liveObjectToBuild.transform.GetChild(0).GetComponent<Renderer>();

            rend.material.shader = Shader.Find("_Color");
            rend.material.SetColor("_Color", Color.green);

            rend.material.shader = Shader.Find("Specular");
            rend.material.SetColor("_SpecColor", Color.green);*/

            //   Debug.Log(other);

            plot = other.gameObject;
            liveObjectToBuild.transform.position = plot.transform.position;

            liveObjectToBuild.transform.parent = plot.transform;

            liveObjectToBuild.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);


        } else
        {
            
          //  Renderer rend = liveObjectToBuild.transform.GetChild(0).GetComponent<Renderer>();
            /*
            rend.material.shader = Shader.Find("_Color");
            rend.material.SetColor("_Color", Color.red);

            rend.material.shader = Shader.Find("Specular");
            rend.material.SetColor("_SpecColor", Color.red);
            */
        }

    }


    private void OnTriggerExit(Collider other)
    {

        if (other.tag == "plot" && other.GetComponent<pointScript>().occupied == false && liveObjectToBuild != null)
        {

            liveObjectToBuild.transform.position = this.transform.position;
            liveObjectToBuild.transform.parent = this.transform;
        //    Debug.Log(other);
            plot = null;

            //Renderer rend = liveObjectToBuild.transform.GetChild(0).GetComponent<Renderer>();

            //rend.material.shader = Shader.Find("_Color");
            //rend.material.SetColor("_Color", Color.red);

            //rend.material.shader = Shader.Find("Specular");
            //rend.material.SetColor("_SpecColor", Color.red);

        } else if (other.tag == "dockPlot" && other.GetComponent<pointScript>().occupied == false && liveObjectToBuild != null)
        {

            liveObjectToBuild.transform.position = this.transform.position;
            liveObjectToBuild.transform.parent = this.transform;
            //    Debug.Log(other);
            plot = null;

            //Renderer rend = liveObjectToBuild.transform.GetChild(0).GetComponent<Renderer>();

            //rend.material.shader = Shader.Find("_Color");
            //rend.material.SetColor("_Color", Color.red);

            //rend.material.shader = Shader.Find("Specular");
            //rend.material.SetColor("_SpecColor", Color.red);

        }

    }


    }

  