using UnityEngine;
using System.Collections;

public class uiOnBuilding : MonoBehaviour {
    public Transform target;
    public Camera camera;
    
    void Start() {
    	camera = GameObject.FindGameObjectWithTag("gameManager").GetComponent<guiManager>().MainCamera;;
    }
    
    void Update() {
        Vector3 screenPos = camera.WorldToScreenPoint(target.position);
        this.GetComponent<RectTransform>().position = screenPos;
    }
}
