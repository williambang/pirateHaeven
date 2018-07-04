using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gameManagerScript : MonoBehaviour {


    [System.Serializable]
    public class Attraction
    {
        public GameObject building;
        public int attract = 0;
    }

    [System.Serializable]
    public class WanderingPoint
    {
        public GameObject point;
        public int attractiveness;
    }

    public bool gamePaused;
    public bool fastForwarded;
    public float standardTimeScale = 1.0f;

    public int money;
    public int rum;
    public int ressources;
    public int availableWorkers;
    public int population;
    public int citizensWithHome;

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
    public Text visitorsText;
    public Text populationText;
    public Text attractionText;
    public Text availableWorkersText;
    public Text citizensWithoutHomeText;

    public Button spawnVisitor;
    public Button spawnCitizen;
    public GameObject visitorPrefab;
    public GameObject citizenPrefab;

    public List<Attraction> attractionBuildings;

    public List<WanderingPoint> wanderingPoints;

    // Use this for initialization
    void Start () {

        spawnVisitor.onClick.AddListener(ClickToSpawnVisitor);
        spawnCitizen.onClick.AddListener(ClickToSpawnCitizen);
    }

    // Update is called once per frame
    void Update()
    {

        moneyText.text = money.ToString();
        ressourcesText.text = ressources.ToString();
        visitorsText.text = visitors.ToString();
        populationText.text = population.ToString();
        attractionText.text = attractiveness.ToString();
        availableWorkersText.text = availableWorkers.ToString();
        citizensWithoutHomeText.text = (population - citizensWithHome).ToString();

    }

    public void pauseGame() {


        if (gamePaused == true) {

            Time.timeScale = 0f;
            gamePaused = false;

        } else if (gamePaused == false) {

            Time.timeScale = standardTimeScale;
            gamePaused = true;
        }

    }

    
    public void fastForward() {


        if (fastForwarded == true) {

            Time.timeScale = standardTimeScale * 2;
            fastForwarded = false;
 
        } else if (fastForwarded == false) {

            Time.timeScale = standardTimeScale;
            fastForwarded = true;

        }

    }

    


    void ClickToSpawnVisitor ()
    {

        Instantiate(visitorPrefab, gameObject.transform.position, gameObject.transform.rotation);

    }

        void ClickToSpawnCitizen ()
    {
        string[] name = {"jack", "smith", "johnny", "davy", "gibbs", "jim"};
        int val = Random.Range(0, name.Length - 1);
        string givenName = name[val];

        /*GameObject newCitizen = */ 
        var newCitizen = Instantiate(citizenPrefab, gameObject.transform.position, gameObject.transform.rotation);
        newCitizen.name = givenName;
        population += 1;
        availableWorkers +=1;
    }
}
