using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gameManagerScript : MonoBehaviour {

    public int money;
    public int rum;
    public int ressources;
    public int workers;
    public int availableWorkers;
    public int workersWithHome;
    public int population;
    public int visitors;
    public int availableVisitors;
    public int dockedShips;

    public int smallShips;
    public int mediumShips;
    public int largeShips;

    public float attractiveness;
    public int harborLevel = 1;
    public int workerLevel = 1;

    public int tax;
    public int dockPrice;

    public Text moneyText;
    public Text ressourcesText;
    public Text rumText;
    public Text visitorsText;
    public Text populationText;
    public Text attractionText;


	// Use this for initialization
	void Start () {

        availableWorkers = workers;

	}
	
	// Update is called once per frame
	void Update () {

        moneyText.text = money.ToString();
        ressourcesText.text = ressources.ToString();
        rumText.text = workers.ToString();
        visitorsText.text = visitors.ToString();
        populationText.text = population.ToString();
        attractionText.text = attractiveness.ToString();


    }
}
