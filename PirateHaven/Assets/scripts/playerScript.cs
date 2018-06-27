using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class playerScript : MonoBehaviour {

	
 	Ray hoverRay;      
	Ray markerRay;
	Ray selectRay;
 	RaycastHit hit; 
	RaycastHit planeHit;

    public GameObject buildMarker;
    public GameObject liveBuildMarker;
    public GameObject cubeMesh;

    public GameObject buildingMenu;
    public GameObject ringsOfDeath;

    public bool buildMode = false;
	public GameObject objectHit;
    //public Text text;
	public Collider coll;
	public bool canBuild = true;
	public bool mouseDown;

    public bool clickedBuildingButton;

    void Start () {
        buildingMenu.SetActive(false);
        //text.text = "BUILDMODE OFF";

    }
    void Update()
    {



        //SWITCH MODE
        if (Input.GetKeyUp(KeyCode.Space))
        {

            if (buildMode == true)
            {

                buildMarker.GetComponent<buildingScript>().buildMode = false;
                buildMode = false;
                Destroy(liveBuildMarker);
                //text.text = "BUILDMODE OFF";
                buildingMenu.SetActive(false);
                ringsOfDeath.SetActive(false);
            }
            else
            {

                buildMarker.GetComponent<buildingScript>().buildMode = true;
                buildMode = true;
                liveBuildMarker = Instantiate(buildMarker, Input.mousePosition, Quaternion.identity);

                buildingMenu.SetActive(true);
                ringsOfDeath.SetActive(true);

                //text.text = "BUILDMODE ON";

            }

            if (buildMode == false)
            {



            }
        }

        }
    }




