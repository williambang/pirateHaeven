using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class housingScript : MonoBehaviour {

	public int currentLiving;
	public int[] maxLiving;

	public GameObject gm;

	// Use this for initialization
	void Start () {
		
		gm = GameObject.FindGameObjectWithTag("gameManager");

	}

	void Update () {
		
		var buildingManager = GetComponent<buildingManager>();
		var gameManagerScript = gm.GetComponent<gameManagerScript>();

        if (buildingManager.currentTask == buildingManager.taskType.nothing && buildingManager.buildingLevel > 0 && currentLiving < maxLiving[buildingManager.buildingLevel - 1])
        {
			
            if (gameManagerScript.citizensWithHome != gameManagerScript.population)
            {

                currentLiving = currentLiving + 1;
                gameManagerScript.citizensWithHome = gameManagerScript.citizensWithHome + 1;
				

			} 

			updateUi();

        } else if (buildingManager.currentTask == buildingManager.taskType.constructing == true && currentLiving != 0) {

			gameManagerScript.citizensWithHome -= currentLiving;
			currentLiving -= currentLiving;

		}
	}

	void updateUi() {

		var i = GetComponent<buildingManager>().buildingLevel - 1;
		GetComponent<buildingGuiHandler>().peopleText.text = currentLiving.ToString() + "/" + maxLiving[i].ToString();
	}
}


