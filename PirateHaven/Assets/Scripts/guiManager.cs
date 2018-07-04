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

    public Camera MainCamera;


	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
