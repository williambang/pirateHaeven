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

    public string[] month = {"January", "February", "March", "April", "May", "June", "July", "August", "September", "Ocotober", "November", "December"};

    public bool gamePaused;
    public bool fastForwarded;
    public float standardTimeScale = 1.0f;

    public GameObject sun;
    public float sunRadius;

    public float minutesPerDay;
    public int currentDay = 1;
    public int currentMonth = 1;
    public int currentYear = 1720;
    public int currentHourTime;
    public int currentMinuteTime;

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

    public Text yearText;
    public Text monthText;
    public Text dayText;
    public Text clockText;

    public Button spawnVisitor;
    public Button spawnCitizen;
    public GameObject visitorPrefab;
    public GameObject citizenPrefab;

    public List<Attraction> attractionBuildings;

    public List<WanderingPoint> wanderingPoints;

    string[] firstName = {"Edward", "Bartholemew", "William", "Mary", "Ann", "Henry", "Francis", "Jack", "Hector", "Davy", "Benjamin", "Charles", "Howell", "Samuel", "Thomas"};
    string[] lastName = {"Teach", "Kenway", "Jones", "Davis", "Every", "Williams", "Tew", "Kidd", "Low", "Smith", "Turner", "Sparrow", "Hornigold", "Flint", "Rackam", "Vane", "Roberts"};

    // Use this for initialization
    void Start () {

        spawnVisitor.onClick.AddListener(ClickToSpawnVisitor);
        spawnCitizen.onClick.AddListener(ClickToSpawnCitizen);

        StartCoroutine(clockTimer());


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

        //sunRadius += ((360 / minutesPerDay) / 60);
        

        if (fastForwarded == true) {

            sunRadius += 0.010f;


        } else {

            sunRadius += 0.005f;

        }

        if (sunRadius > 360) {

            sunRadius = 0;
            sun.transform.eulerAngles = new Vector3 (sun.transform.eulerAngles.x, sun.transform.eulerAngles.y, 0);

        }

        if (sunRadius == 0) {

            sunRadius = 90;
            sun.transform.eulerAngles = new Vector3 (sun.transform.eulerAngles.x, sun.transform.eulerAngles.y, 90);

        }
  
        if (fastForwarded == true) {

            sun.transform.Rotate(0, 0, 0.010f);


        } else {

            sun.transform.Rotate(0, 0, 0.005f);

        }

    }

    public void pauseGame() {


        if (gamePaused == true) {

            if (fastForwarded == true) {

                Time.timeScale = standardTimeScale * 2;
                gamePaused = false;

            } else {

                Time.timeScale = standardTimeScale;
                gamePaused = false;

            }

            

        } else if (gamePaused == false) {

            Time.timeScale = 0f;
            gamePaused = true;
        }

    }

    
    public void fastForward() {


        if (fastForwarded == false) {

            Time.timeScale = standardTimeScale * 2;
            fastForwarded = true;
 
        } else if (fastForwarded == true) {

            Time.timeScale = standardTimeScale;
            fastForwarded = false;

        }

    }

    


    void ClickToSpawnVisitor ()
    {
        var newVisitor = Instantiate(visitorPrefab, gameObject.transform.position, gameObject.transform.rotation);

        int ranF = Random.Range(0, firstName.Length - 1);
        int ranL = Random.Range(0, lastName.Length - 1);
        string givenName = firstName[ranF] + " " + lastName[ranL];
        newVisitor.name = givenName;
        visitors += 1;

    }

        void ClickToSpawnCitizen ()
    {
        int ranF = Random.Range(0, firstName.Length - 1);
        int ranL = Random.Range(0, lastName.Length - 1);
        string givenName = firstName[ranF] + " " + lastName[ranL];

        /*GameObject newCitizen = */ 
        var newCitizen = Instantiate(citizenPrefab, gameObject.transform.position, gameObject.transform.rotation);
        newCitizen.name = givenName;
        population += 1;
        availableWorkers +=1;
    }

    IEnumerator clockTimer() {

        yield return new WaitForSeconds(minutesPerDay / 24f);

        if (currentHourTime == 24) {

            if (currentDay > 30) {

                if (currentMonth > 10) {

                    currentMonth = 0;
                    currentYear += 1;
                    currentDay = 1;
                    currentHourTime = 0;

                } else {

                    currentDay = 1;
                    currentMonth += 1;
                    currentHourTime = 0;

                }



            } else {
                currentDay += 1;
                currentHourTime = 0;
            }

        }

        if (currentMinuteTime == 60) {

            currentHourTime += 1;
            currentMinuteTime = 0;

        } else {

            currentMinuteTime += 1;

            if (currentMinuteTime < 10) {

                if (currentHourTime > 9) {
                    clockText.text = currentHourTime.ToString() + ":0" + currentMinuteTime.ToString();
                } else {
                    clockText.text = "0" + currentHourTime.ToString() + ":0" + currentMinuteTime.ToString();
                }
            } else if (currentMinuteTime > 9) {
                if (currentHourTime < 10) {
                    clockText.text = "0" + currentHourTime.ToString() + ":" + currentMinuteTime.ToString();
                } else {
                    clockText.text = currentHourTime.ToString() + ":" + currentMinuteTime.ToString();
                }
            }

            dayText.text = currentDay.ToString();
            monthText.text = month[currentMonth];
            yearText.text = currentYear.ToString();

        }

        StartCoroutine(clockTimer());

        

        
//        dayText.text = "Day " + currentDay.ToString();

    }
}
