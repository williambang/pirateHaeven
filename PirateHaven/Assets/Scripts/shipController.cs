using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
/* 
    
public class shipController : MonoBehaviour {

    public GameObject[] docks;
    public bool shipDocked;

    public GameObject shipExit;

    public bool shipTakeoff;

    public GameObject dockSpot;
    public GameObject halfwaySpot;

    private bool halfway;
    public bool halfwayExit;

    public NavMeshAgent agent;

	// Use this for initialization
	void Start () {

        shipExit = GameObject.FindGameObjectWithTag("shipExit");

	}

    // Update is called once per frame
    void Update()
    {
    docks = GameObject.FindGameObjectsWithTag("dock");

        if (docks.Length > 0)
        {
            Debug.Log("Not null");

            for (int i = 0; i < docks.Length; i++)
            {
                Debug.Log("Found dock");
                var dock = docks[i].GetComponent<dockScript>();

                if (docks[i].GetComponent<buildingStats>().constructionCompleted == true)
                {
                    for (int d = 0; d < dock.dockedShips.Length; d++)
                    {
                        Debug.Log("Found ship spot");

                        if (dock.dockedShips[d] == null && !shipDocked)
                        {
                            shipDocked = true;
                            dock.dockedShips[d] = this.gameObject;

                            dockSpot = dock.shipDock[d];
                            halfwaySpot = dock.halfWay[d];

                            
                            halfway = true;
                            agent.SetDestination(halfwaySpot.transform.position);
                            Debug.Log("Set destination");

                        }
                    }
                }
            }
        }


        
                if (!agent.hasPath)
                {

                    

                }
        if (!agent.pathPending && docks.Length > 0 && shipTakeoff == false)
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f && halfway)
                {

                    agent.SetDestination(dockSpot.transform.position);
                }
            }
        }

        if (shipTakeoff == true)
        {
            shipDocked = false;
            agent.SetDestination(shipExit.transform.position);
            StartCoroutine(changeSpeed());


        }

        if (dockSpot == null)
        {

            agent.SetDestination(shipExit.transform.position);

        }

        

    }

    IEnumerator changeSpeed()
    {

        agent.speed = 0.1f;
        agent.acceleration = 0.03f;
        yield return new WaitForSeconds(15);
        agent.acceleration = 0.15f;
        agent.speed = 0.8f;


    }
}

*/

