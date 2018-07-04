using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dockPlotting : MonoBehaviour {

    public int numberPlots;
    public GameObject dockPlot;
    public float offset;

    private float currentOffset;

	// Use this for initialization
	void Start () {

        var firstObj = Instantiate(dockPlot, gameObject.transform.position, gameObject.transform.rotation);
        firstObj.transform.parent = this.transform;


        var rot = gameObject.transform.rotation;


        for (int i = 0; i < numberPlots; i++)
        {
            currentOffset += offset;

            var plusPos = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z + currentOffset);
            var minusPos = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z - currentOffset);

            var obj1 = Instantiate(dockPlot, plusPos, rot);
            obj1.transform.parent = this.transform;

            var obj = Instantiate(dockPlot, minusPos, rot);
            obj.transform.parent = this.transform;



        }



    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
