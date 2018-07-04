using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;

public class visitorScript : MonoBehaviour {

    public enum visitorState {inBuilding, wandering, leaving, lookingForAttraction, walkingToAttraction};

    public GameObject[] allBuildings;

    public visitorState currentState;

    public float[] buildingPercentage;
    public int totalAttraction;

    public GameObject currentlyVisiting;
    public GameObject targetPoint;
    public GameObject targetBuilding;
    public GameObject collidingBuilding;

    private GameObject gameManager; 
    private gameManagerScript gmScript;

    public List<gameManagerScript.Attraction> attractionBuildings;

    public bool inBuilding;
    public bool wandering;

    private int maxIndex;
    private float maxValue;

    public NavMeshAgent agent;    

	// Use this for initialization
	void Start () {

        gameManager = GameObject.FindGameObjectWithTag("gameManager");
        gmScript = gameManager.GetComponent<gameManagerScript>();
    }

    void Update()
    {

        if (currentState == visitorState.lookingForAttraction)
        {

            if (targetPoint == null && gmScript.attractionBuildings.Count > 0)
            {
                Debug.Log("There are Attractions!");
                //findMostAttractive();
            }
            else {
                Debug.Log("There are no Attractions!");
            }

        /* 
            if (targetPoint != null)
            {
                agent.SetDestination(targetPoint.transform.position);
            }

            if (!agent.pathPending)
            {
                if (agent.remainingDistance <= agent.stoppingDistance)
                {
                    if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                    {
                        targetPoint = null;
                    }
                }
            }

        } else if (wandering)
        {

            targetPoint = null;
            inBuilding = false;
            currentlyVisiting = null;

        } */

    }
/*
    public void findMostAttractive()
    {

        if (!wandering)
        {

            var gm = gameManager.GetComponent<gameManagerScript>();

            int attractiveWeight = 0;

            for (int i = 0; i < attractionBuildings.Count; i++)
            {
                attractiveWeight += attractionBuildings[i].attract;
            }

            int roll = Random.Range(0, attractiveWeight);


            for (int j = 0; j < attractionBuildings.Count; j++)
            {
                if (roll <= attractionBuildings[j].attract)
                {


                    if (attractionBuildings[j].building.GetComponent<buildingManager>() == true && gm.attractionExists == true)
                    {

                        //findMostAttractive();
                        break;

                    }
                    else if (gm.attractionExists != true)
                    {

                        break;
                    }
                    else
                    {
                        targetPoint = attractionBuildings[j].building.GetComponent<buildingStats>().entrancePoint;
                        targetBuilding = attractionBuildings[j].building;
                    }
                    return;
                }

                roll -= attractionBuildings[j].attract;

            }

        }*/

        
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
