using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class buildingButtonScript : MonoBehaviour {

    public GameObject buildingToBuild;
    public GameObject buildMarker;

	// Use this for initialization
	void Start () {

        

        Button btn = this.gameObject.GetComponent<Button>();
        btn.onClick.AddListener(ClickToSelect);


    }
	
	// Update is called once per frame
	void Update () {

        buildMarker = GameObject.FindGameObjectWithTag("buildingMarker");

    }

    void ClickToSelect()
    {

        buildMarker.GetComponent<buildingScript>().setPreview(buildingToBuild);

    }
}
