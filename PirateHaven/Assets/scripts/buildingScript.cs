using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buildingScript : MonoBehaviour {

    public GameObject GameManager;

    public bool buildMode = false;
    public bool onPlot = false;

    GameObject[] grounds;
    GameObject ground;

    //public List<GameObject> plots = new List<GameObject>();

    public GameObject plot;

    public GameObject objectToBuild;
    public GameObject liveObjectToBuild;

    Ray markerRay;
    Ray hoverRay;
    RaycastHit planeHit;

    private bool previewChanging = false;

    // Use this for initialization
    void Start () {


        GameManager = GameObject.FindGameObjectWithTag("gameManager");

        grounds = GameObject.FindGameObjectsWithTag("ground");
        ground = grounds[0];

    }

    // Update is called once per frame
    void Update()
    {
        var gm = GameManager.GetComponent<gameManagerScript>();

        if (buildMode == true)
        {

            // Marker Position and Rotation 

            markerRay = Camera.main.ScreenPointToRay(Input.mousePosition);

            Collider groundColl = ground.GetComponent<MeshCollider>();

            if (groundColl.Raycast(markerRay, out planeHit, 100.0f))
            {

                Vector3 posMarker = new Vector3(planeHit.point.x, 0.1f, planeHit.point.z);
                this.transform.position = posMarker;

                // Building object
                if (Input.GetMouseButtonUp(0))
                {
                    if(liveObjectToBuild != null) { 

                    var objComp = liveObjectToBuild.GetComponent<buildingStats>();

                    if (plot != null && previewChanging == false && liveObjectToBuild.GetComponent<buildingStats>().overlapping == false && gm.money >= objComp.moneyCost && gm.rum >= objComp.rumCost && gm.ressources >= objComp.ressourcesCost)
                    {
                        if (gm.money >= objComp.moneyCost) { 

                            if (gm.rum >= objComp.rumCost)
                            {

                                if (gm.ressources >= objComp.ressourcesCost)
                                {

                                    // #1 Build structure framework with no model & and set define build stats. 

                                    GameObject liveBuilding = Instantiate(objectToBuild, liveObjectToBuild.transform.position, liveObjectToBuild.transform.rotation);
                                    var bldStats = liveBuilding.GetComponent<buildingStats>();

                                    // #2 Build construction site and parent it under live building & and set as current building.

                                    GameObject constructionSite = bldStats.worksite;
                                    GameObject liveConstructionSite = Instantiate(constructionSite, liveObjectToBuild.transform.position, liveObjectToBuild.transform.rotation);
                                    liveConstructionSite.transform.parent = liveBuilding.transform;
                                    bldStats.currentBuilding = liveConstructionSite;

                                    // #3 Calculate cost between building and Game Manager

                                    gm.money -= bldStats.moneyCost;
                                    gm.attractiveness += bldStats.attractiveness;
                                    gm.ressources -= bldStats.ressourcesCost;
                                    gm.rum -= bldStats.rumCost;

                                    // #4 Mark plot occupied.

                                    plot.GetComponent<pointScript>().occupied = true;
                                    bldStats.plot = plot;

                                    // #5 Set tags and tie up loose ends.

                                    liveBuilding.tag = "placedBuilding";
                                    bldStats.constructionStarted = true;
                                   // Debug.Log("Object instantiated");
                                    plot = null;

                                    Destroy(liveObjectToBuild);
                                    liveObjectToBuild = null;
                                    objComp = null;

                                }


                            }

                        }

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

        previewChanging = false;

        //Spawning Level 01 Building for preview.

        GameObject previewBuild = objectToBuild.GetComponent<buildingStats>().level01;

        GameObject livePreviewBuild = Instantiate(previewBuild, liveObjectToBuild.transform.position, liveObjectToBuild.transform.rotation);
        livePreviewBuild.transform.parent = liveObjectToBuild.transform;

    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "plot" && other.GetComponent<pointScript>().occupied == false && liveObjectToBuild != null)
        {

           // Renderer rend = liveObjectToBuild.transform.GetChild(0).GetComponent<Renderer>();

           // rend.material.shader = Shader.Find("_Color");
           // rend.material.SetColor("_Color", Color.green);

           // rend.material.shader = Shader.Find("Specular");
          //  rend.material.SetColor("_SpecColor", Color.green);

         //   Debug.Log(other);

            plot = other.gameObject;
            liveObjectToBuild.transform.position = plot.transform.position;

            liveObjectToBuild.transform.parent = plot.transform;
  
            liveObjectToBuild.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);


        } else
        {
            /*
            Renderer rend = liveObjectToBuild.transform.GetChild(0).GetComponent<Renderer>();

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

        }

    }


    }

  