using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;

public class visitorScript : MonoBehaviour {

    public enum visitorState { wandering, leaving, walkingToAttraction, lookingForAttraction};

	public visitorState currentState;

    public GameObject targetPoint;

	public NavMeshAgent agent;  

	private GameObject gameManager; 

	public GameObject lastPoint;   

    public float wanderingTime;


	void Start () {
		gameManager = GameObject.FindGameObjectWithTag("gameManager");
        currentState = visitorState.wandering;
		resetVisitor();
	}
	
	void OnTriggerStay(Collider other) {
		if (other.gameObject == targetPoint) {

			if(currentState == visitorState.wandering) {
                resetVisitor();
			}
			else if( currentState == visitorState.walkingToAttraction ) 
            {
                var bldMnger = other.transform.parent.GetComponent<buildingManager>();	
				var attrScript = other.transform.parent.GetComponent<attractionScript>();	
                int maxVisitors = attrScript.maxVisitors[bldMnger.buildingLevel - 1];	
				{
					if( attrScript.currentVisitors.Count < maxVisitors ) {
						var attractionEntrance = other.gameObject;
						startVising(attractionEntrance);
					}
					else {
						resetVisitor();
					}
				}
			}
		}
    } 

	public void resetVisitor() {
        
		lastPoint = targetPoint;
		targetPoint = null;
		var point = getAttractivePoint();
		targetPoint = point;
		agent.SetDestination(point.transform.position);	
        wanderingTime = Random.Range(0, 30);

        StartCoroutine(waitForWander());

        currentState = visitorState.wandering;
	
	}


	private GameObject getAttractivePoint()
    {
			//var gm = gameManager.GetComponent<gameManagerScript>();
			var wanderPoints = gameManager.GetComponent<gameManagerScript>().wanderingPoints;
            GameObject attractivePoint = null;
            int attractiveWeight = 0;

            for (int i = 0; i < wanderPoints.Count; i++)
            {
                attractiveWeight += wanderPoints[i].attractiveness;
            }

            int roll = Random.Range(0, attractiveWeight);
			
            for (int j = 0; j < wanderPoints.Count; j++)
            {

					if (roll <= wanderPoints[j].attractiveness)
					{
							attractivePoint = wanderPoints[j].point;
							break;
					}

					roll -= wanderPoints[j].attractiveness;

			}
			return attractivePoint;
    }

    IEnumerator waitForWander()
    {
        
        yield return new WaitForSeconds(wanderingTime);
        //Debug.Log("I, " + this.gameObject.name + ", am looking for something to do!");
        findAttraction();

    }

    private void findAttraction() 
    {

        var gmScript = gameManager.GetComponent<gameManagerScript>();
        var openAttractions = gmScript.attractionBuildings;

        if(openAttractions.Count > 0) {

            GameObject attractionBuilding = null;
                   
            int attractiveWeight = 0;

            for (int i = 0; i < openAttractions.Count; i++)
            {
                attractiveWeight += openAttractions[i].attract;
            }

            int roll = Random.Range(0, attractiveWeight);


            for (int j = 0; j < openAttractions.Count; j++)
            {
                if (roll <= openAttractions[j].attract)
                {
                    attractionBuilding = openAttractions[j].building;
                    break;

                }

                roll -= openAttractions[j].attract;

            }
            
            targetPoint = attractionBuilding.GetComponent<buildingManager>().entrancePoint;
            agent.SetDestination(targetPoint.transform.position);
            currentState = visitorState.walkingToAttraction;

        }
        else {
            currentState = visitorState.lookingForAttraction;
            resetVisitor();
        }

    }

    public void startVising(GameObject attractionEntrance) {

        var attrScript = attractionEntrance.transform.parent.GetComponent<attractionScript>();
        attrScript.addVisitor(this.gameObject);

    }

}
