using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;

public class workerScript : MonoBehaviour {

	public enum workerState {working, wandering, walkingToWork};
	public workerState currentState;

    public GameObject targetPoint;

	public NavMeshAgent agent;  

	private GameObject gameManager; 

	public GameObject lastPoint;


	void Start () {
		gameManager = GameObject.FindGameObjectWithTag("gameManager");
		resetWorker();
	}
	
	void OnTriggerStay(Collider other) {
		if (other.gameObject == targetPoint) {
			if(currentState == workerState.wandering) {
						resetWorker();
			}
			else if( currentState == workerState.walkingToWork ) {
				var bldMnger = other.transform.parent.GetComponent<buildingManager>();
				var assWorkers = bldMnger.currentAssignedWorkers;
				
				{
					if( assWorkers.Contains(gameObject) ) {
						var work = other.gameObject;
						startWorking(work);
					}
					else {
						resetWorker();
					}
				}
			}
		}
    } 

	public void goToWork(GameObject target) {

		targetPoint = target;
		agent.SetDestination(target.transform.position);
		currentState = workerState.walkingToWork;
	}

	public void startWorking(GameObject work) {

		var bldMnger = work.transform.parent.GetComponent<buildingManager>();
		bldMnger.currentWorkers.Add( gameObject );
		gameObject.SetActive(false);

	}

	public void resetWorker() {
		lastPoint = targetPoint;
		targetPoint = null;
		currentState = workerState.wandering;
		var point = getAttractivePoint();
		targetPoint = point;
		agent.SetDestination(point.transform.position);		
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

}

