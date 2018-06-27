using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class buildingSettings : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {

    public static GameObject itemBeingDragged;

    public GameObject buildingObject;
    public Text buildingName;


	// Use this for initialization
	void Start () {
		


	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnBeginDrag (PointerEventData eventData)
    {
        itemBeingDragged = gameObject;
        gameObject.transform.position = Input.mousePosition;

    }

    public void OnDrag(PointerEventData eventData)
    {

        gameObject.transform.position = Input.mousePosition;

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        itemBeingDragged = null;
        gameObject.transform.position = gameObject.transform.position;

    }
}
