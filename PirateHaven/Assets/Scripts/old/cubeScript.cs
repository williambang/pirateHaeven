using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
 
public class cubeScript : MonoBehaviour {

	public string cubeName;

	public float ElapsedTime;
	public float FinishTime;
	public Vector3 StartPosition;
	public Vector3 TargetPosition;
	public GameObject impactParticle;
	public GameObject cubeMesh;
	public float offset;
	public float target;

	// Use this for initialization
	void Start() {

		cubeName = "con_" + Time.time; 
		Debug.Log(cubeName);

        StartCoroutine(particle());

		StartPosition = new Vector3(cubeMesh.transform.position.x, offset, cubeMesh.transform.position.z);
		TargetPosition = new Vector3(cubeMesh.transform.position.x, target, cubeMesh.transform.position.z);
    }
	
	// Update is called once per frame
	void Update () {


		ElapsedTime += Time.deltaTime;
    	cubeMesh.transform.position = Vector3.Lerp (StartPosition, TargetPosition, ElapsedTime / FinishTime);



	}

	IEnumerator particle() {

        yield return new WaitForSeconds(FinishTime);
		//Instantiate HERE!
		Instantiate (impactParticle, TargetPosition, Quaternion.identity);
    }
}
