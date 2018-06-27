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
    public GameObject constructionSite;

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


                if (Input.GetMouseButtonUp(0))
                {

                    var objComp = liveObjectToBuild.GetComponent<buildingStats>();

                    if (plot != null && previewChanging == false && liveObjectToBuild.GetComponent<buildingStats>().overlapping == false && gm.money >= objComp.moneyCost && gm.rum >= objComp.rumCost && gm.ressources >= objComp.ressourcesCost)
                    {

                        gm.money -= objComp.moneyCost;
                        gm.attractiveness += objComp.attractiveness;
                        gm.ressources -= objComp.ressourcesCost;
                        gm.rum -= objComp.rumCost;

                        plot.GetComponent<pointScript>().occupied = true;
                        Instantiate(objectToBuild, liveObjectToBuild.transform.position, liveObjectToBuild.transform.rotation);
                        Debug.Log("Object instantiated");
                        plot = null;

                        objectToBuild = null;
                        Destroy(liveObjectToBuild);
                        liveObjectToBuild = null;
                        


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
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "plot" && other.GetComponent<pointScript>().occupied == false && liveObjectToBuild != null)
        {

            Renderer rend = liveObjectToBuild.transform.GetChild(0).GetComponent<Renderer>();

            rend.material.shader = Shader.Find("_Color");
            rend.material.SetColor("_Color", Color.green);

            rend.material.shader = Shader.Find("Specular");
            rend.material.SetColor("_SpecColor", Color.green);

            Debug.Log(other);

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
            Debug.Log(other);
            plot = null;

            Renderer rend = liveObjectToBuild.transform.GetChild(0).GetComponent<Renderer>();

            rend.material.shader = Shader.Find("_Color");
            rend.material.SetColor("_Color", Color.red);

            rend.material.shader = Shader.Find("Specular");
            rend.material.SetColor("_SpecColor", Color.red);

        }

    }


    }

  