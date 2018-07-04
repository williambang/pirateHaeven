using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;

public class visitorScript : MonoBehaviour {

    public enum visitorState { wandering, leaving, walkingToAttraction};

	public visitorState currentState;

    public GameObject targetPoint;

	public NavMeshAgent agent;  

	private GameObject gameManager; 

	public GameObject lastPoint;   

    public float wanderingTime;



	void Start () {
		gameManager = GameObject.FindGameObjectWithTag("gameManager");
		resetVisitor();
	}
	
	void OnTriggerStay(Collider other) {
		if (other.gameObject == targetPoint) {
			if(currentState == visitorState.wandering) {
						resetVisitor();
			}
			else if( currentState == visitorState.walkingToAttraction ) {
				var bldMnger = other.transform.parent.GetComponent<buildingManager>();
				var assWorkers = bldMnger.currentAssignedWorkers;
				
				{
					if( assWorkers.Contains(gameObject) ) {
						var work = other.gameObject;
						startVising(work);
					}
					else {
						resetVisitor();
					}
				}
			}
		}
    } 

	public void goToAttraction(GameObject target) {

		targetPoint = target;
		agent.SetDestination(target.transform.position);
		currentState = visitorState.walkingToAttraction;
	}

	public void startVising(GameObject work) {

		var bldMnger = work.transform.parent.GetComponent<buildingManager>();
		bldMnger.currentWorkers.Add( gameObject );
		gameObject.SetActive(false);

	}

	public void resetVisitor() {
		lastPoint = targetPoint;
		targetPoint = null;
		currentState = visitorState.wandering;
		var point = getAttractivePoint();
		targetPoint = point;
		agent.SetDestination(point.transform.position);	
        wanderingTime = Random.Range(5, 60);
        StartCoroutine(wandering());	
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
        findAttraction();

    }

    public void findAttraction() 
    {

        var gmScript = gameManager.GetComponent<gameManagerScript>();
        var openAttractions = gmScript.attractionBuildings;
                   
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
                attractivePoint = wanderPoints[j].point;
                break;

            }

            roll -= openAttractions[j].attract;

        }

    }


/* 

    private void OnCollisionStay(Collision collision)
    {


     //   Debug.Log(collision);

        if (collision.transform.parent.gameObject.GetComponent<buildingStats>().buildingName == "Pub")
        {

          //  Debug.Log(collision);

            Debug.Log(collision.gameObject.name);

            Physics.IgnoreCollision(this.GetComponent<Collider>(), collision.collider);

            var other = collision.transform.parent;

            Debug.Log(other);

            if (other != null)
            {
                var bldStats1 = other.gameObject.GetComponent<buildingStats>();

                {

                    if (targetBuilding.gameObject == other.gameObject && bldStats1.maxVisitors > bldStats1.currentVisitors)
                    {

                        var bldStats = targetBuilding.GetComponent<buildingStats>();
                        var visitorPrefabs = bldStats.visitorPrefabs;
                        var currentVisitors = bldStats.currentVisitors;
                        for (int i = 0; i < visitorPrefabs.Length; i++)
                        {

                            if (targetBuilding != null)
                            {

                                if (visitorPrefabs[i] == null && inBuilding != true)
                                {

                                    bldStats.visitorPrefabs[i] = this.gameObject;
                                    inBuilding = true;
                                    bldStats.currentVisitors += 1;

                                /*
                                    for (int j = 0; j < attractionBuildings.Count; j++)
                                    {

                                        attractionBuildings.Clear();
                                        allBuildings = new GameObject[0];

                                    }
                                    
                                    targetBuilding = null;
                                    targetPoint = null;

                                    this.gameObject.SetActive(false);
                                }
                            }
                        }
                    } else if (bldStats1.maxVisitors <= bldStats1.currentVisitors)
                    {

                    //findMostAttractive();

                    }

                }
            }

        }
    }

    IEnumerator waitForWander()
    {

        wandering = true;
        yield return new WaitForSeconds(Random.Range(10, 60));
        wandering = false;

    }

*/

}
