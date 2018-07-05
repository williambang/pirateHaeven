using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class guiManager : MonoBehaviour {

	    
    [System.Serializable]
    public class buildingUI {

        public Text nameText;
        public Image constructionBar;


        public Image housingIcon;
        public Text livingText;

        public Image attractionIcon;
        public Text visitorText;


        public Button addWorkerButton;
        public Button removeWorkerButton;
        public Text workerText;
        public Image workerIcon;

        public Button upgradeBuildingButton;
        public Button deleteBuildingButton;

    }

    public Sprite[] taskIcons;
    public Sprite[] backGround;
    public Sprite[] typeIcons;

    public GameObject hoverCanvas;
    public buildingUI hoverCanvasElements;

    public Sprite pause_icon;
    public Sprite play_icon;
    public Sprite fastForward_icon;
    public Sprite fastForwarded_icon;

    public Button pauseButton;
    public Button fastForwardButton;

    public Camera MainCamera;

    public GameObject gm;


	// Use this for initialization
	void Start () {

        gm = GameObject.FindGameObjectWithTag("gameManager");

	    //pauseButton.onClick.AddListener(ClickToPause);
        //fastForwardButton.onClick.AddListener(ClickToFastForward);

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void ClickToPause () {

        gm.GetComponent<gameManagerScript>().pauseGame();

        if (gm.GetComponent<gameManagerScript>().gamePaused == true) {

            pauseButton.GetComponent<Image>().sprite = play_icon;
            

        } else {

            pauseButton.GetComponent<Image>().sprite = pause_icon;

        }

    }

    void ClickToFastForward () {


        gm.GetComponent<gameManagerScript>().fastForward();

        if (gm.GetComponent<gameManagerScript>().fastForwarded == false) {

            fastForwardButton.GetComponent<Image>().sprite = fastForwarded_icon;
            

        } else {

            fastForwardButton.GetComponent<Image>().sprite = fastForward_icon;

        }

    }
}
