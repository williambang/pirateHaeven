using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class timeMove : MonoBehaviour {

public float ElapsedTime;
public float FinishTime;
public Vector3 StartPosition;
public Vector3 Target;

void Start()
    {
        StartCoroutine(particle());
    }

void Update ()
	{
    	ElapsedTime += Time.deltaTime;
    	transform.position = Vector3.Lerp (StartPosition, Target, ElapsedTime / FinishTime);
	}

 IEnumerator particle()
    {
        yield return new WaitForSeconds(FinishTime);
		//Instantiate HERE!
    }

}

