using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class destroyBuilding : MonoBehaviour {

    public GameObject building;
    public GameObject parent;

    public bool isDestroyButton;

	// Use this for initialization
	void Start () {

        Button btn = this.gameObject.GetComponent<Button>();
        btn.onClick.AddListener(ClickToDestroy);

        if (isDestroyButton == true)
        {
            building = parent.GetComponent<buildingSettings>().buildingObject;

        }
       

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void ClickToDestroy()
    {

        Destroy(building);
        Destroy(parent);

    }
}
