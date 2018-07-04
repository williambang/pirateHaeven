using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class createBuildingPlots : MonoBehaviour {

    public int rows;
    public int created;
    private int radius;
    public GameObject ringMaker;

	// Use this for initialization
	void Start () {

        addRing();

    }
	
	// Update is called once per frame
	void Update () {



    }

    void addRing()
    {

        if (created < rows)
        {
            GameObject ring = Instantiate(ringMaker, gameObject.transform.position, gameObject.transform.rotation);
            ring.GetComponent<wat>().radius += (3*created);
            ring.transform.parent = this.transform;
            created += 1;


            addRing();
        }

    }

 
}
