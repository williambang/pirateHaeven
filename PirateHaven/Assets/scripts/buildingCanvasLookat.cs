using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buildingCanvasLookat : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        transform.LookAt(GameObject.FindGameObjectWithTag("MainCamera").transform);

	}
}
