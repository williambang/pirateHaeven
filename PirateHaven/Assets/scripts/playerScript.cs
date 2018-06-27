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
                buildingMenu.active = false;
                ringsOfDeath.active = false;
            }
            else
            {

                buildMarker.GetComponent<buildingScript>().buildMode = true;
                buildMode = true;
                liveBuildMarker = Instantiate(buildMarker, Input.mousePosition, Quaternion.identity);

                buildingMenu.active = true;
                ringsOfDeath.active = true;

                //text.text = "BUILDMODE ON";

            }

            if (buildMode == false)
            {



            }
        }

          


        /*

        // BUILDMODE IS ON

        if (buildMode == true)
        {


            // Marker Position and Rotation

            markerRay = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (coll.Raycast(markerRay, out planeHit, 100.0f))
            {

                Vector3 posMarker = new Vector3(planeHit.point.x, 0.1f, planeHit.point.z);

                if (mouseDown == false)
                {
                    cubeMarker.transform.position = posMarker;
                }
                else if (canBuild == true)
                {
                    Vector3 targetPosition = new Vector3(posMarker.x, cubeMarker.transform.position.y, posMarker.z);
                }


                if (Input.GetMouseButtonDown(0))
                {
                    Debug.Log("GetMouseDown!");
                    if (canBuild == true)
                    {
                        mouseDown = true;
                        Debug.Log("Mouse down = true!");
                    }
                }


                if (Input.GetMouseButtonUp(0))
                {

                    Instantiate(objectToBuild, cubeMesh.transform.position, cubeMesh.transform.rotation);
                    Debug.Log("Object instantiated");

                }


            }

            /*
                   // Check if mouse is on plot and if we can build

                   hoverRay = Camera.main.ScreenPointToRay (Input.mousePosition); 

            if (Physics.Raycast (hoverRay, out hit)) { 

               objectHit = hit.transform.gameObject;

               if (cubeMarker.GetComponent<cubeMarkerScript>().onPlot == true) {

                   canBuild = true;

                           cubeMarker.GetComponent<cubeMarkerScript>().child.transform.position = cubeMarker.GetComponent<cubeMarkerScript>().plots[0].transform.position;

                           //float posX = cubeMarker.GetComponent<cubeMarkerScript>().plots[0].transform.position.x;
                           //float posY = cubeMarker.GetComponent<cubeMarkerScript>().plots[0].transform.position.y;
                           //Debug.Log(posX);
                           //Debug.Log(posX);

                           /* Vector3 cPos = cubeMarker.GetComponent<cubeMarkerScript>().child.transform.position;

                            Vector3 pPos = cubeMarker.GetComponent<cubeMarkerScript>().plots[0].transform.position;

                            Vector3 NewCpos = new Vector3(pPos.x, cPos.y, pPos.z);

                            cPos = NewCpos;




                       } else {

                   canBuild = false;

                        }
                   }


               }

               //BUILD MODE IS OFF
               if (buildMode == false) {



               }*/

        }
    }




