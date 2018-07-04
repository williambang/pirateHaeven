using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
/* 
public class buildingSettings : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {

    public static GameObject itemBeingDragged;

    public GameObject buildingObject;
    public string buildingName;

    private Vector3 mousepos;
    private Vector3 screenPoint;
    private Vector3 offset;


	// Use this for initialization
	void Start () {


	}
	
	// Update is called once per frame
	void Update () {

        buildingName = this.transform.GetChild(2).GetComponent<InputField>().text;
        buildingObject.GetComponent<buildingStats>().buildingName = buildingName;


    }

    public void OnBeginDrag (PointerEventData eventData)
    {


        offset = gameObject.transform.position - Input.mousePosition;

    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);

        Vector3 curPosition = Input.mousePosition + offset;
        transform.position = curPosition;

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        itemBeingDragged = null;


    }

    void OnMouseDown()
    {
        

    }
    
}
*/