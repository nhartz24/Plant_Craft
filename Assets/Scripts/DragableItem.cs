using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems; //import to allow handlers
using UnityEngine.UI;


// need to add a grid layout group to each inventory slot and have child placement be center

public class DragableItem : MonoBehaviour
{
    private Merge merge;

    private bool isSelected;

    public Transform slot;

    private Image image;
    public Vector3 smallSize = new Vector3(1.4f,2.1f,0);
    public Vector3 largeSize = new Vector3(2.3f,3.15f ,0);

    private Canvas canvas;
    
    void Start()
    {
        image = GetComponent<Image>();

        try {
            merge = GameObject.Find("MergeButton").GetComponent<Merge>();
        } catch (NullReferenceException ex) {
            Debug.Log("ERROR: "+ex);
            Debug.Log("MergeButton's merge script could not be found by this slot."+
                "Check the name of the object and that it has the merge component.");
        }

        try {
            canvas = GameObject.Find("CanvasCrossing").GetComponent<Canvas>();
        } catch (NullReferenceException ex) {
            Debug.Log("ERROR: "+ex);
            Debug.Log("CanvasCrossing's canvas component could not be found by this slot."+
                "Check the name of the object.");
        }

        slot = transform.parent;
        slot.GetComponent<Button>().onClick.AddListener(pickUp);

        isSelected = false;
    }

    void Update()
    {
        if(isSelected) {
            if(image.rectTransform.localScale != smallSize) {
                image.rectTransform.localScale = smallSize;
            }

            // follow mouse
            Vector2 movePos;

            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                canvas.transform as RectTransform,
                Input.mousePosition, canvas.worldCamera,
                out movePos);

            transform.position = canvas.transform.TransformPoint(movePos);

        } 
        else  // not selected
        {
            if (slot.gameObject.name == "Slot0"){
                merge.setParent0(gameObject);          
                image.rectTransform.localScale = largeSize;
            }
            else if(slot.gameObject.name == "Slot1"){
                merge.setParent1(gameObject);
                image.rectTransform.localScale = largeSize;
            }
            else {
                image.rectTransform.localScale = smallSize;
            }
        }

    }

    // run from the slot, after placing
    public void isPlaced() {
        isSelected = false;
        slot = transform.parent;
        Debug.Log($"In {gameObject.name}, the new slot is {slot.gameObject.name}");
        try
        {
            GameObject.Find("SoundManagerMain").GetComponent<SoundManager>().Play("plant_plant");
        }
        catch (NullReferenceException ex)
        {
            Debug.Log("ERROR: " + ex);
            Debug.Log("Main Menu Sound Manager not found. Not a problem if not running from main menu");
        }
        slot.GetComponent<Button>().onClick.AddListener(pickUp);
    }

    // runs from the listening slot, when its button is clicked
    public void pickUp () { 
        Debug.Log(gameObject.name+" is attempting pick up");
        Transform selectedItemCont = GameObject.Find("SelectedItem").transform;
        if(selectedItemCont.childCount == 0 && !isSelected) {
            transform.SetParent(selectedItemCont);
            slot.GetComponent<Button>().onClick.RemoveAllListeners();
            if (slot.childCount == 1) {
                slot.GetChild(0).GetComponent<DragableItem>().addListenerToMySlot();
                Debug.Log($"another child found in {slot.gameObject.name}, so listener added again.");
            }
            
            slot = null;
            isSelected = true;
            Debug.Log(gameObject.name+" is selected!");
            //add sound here? 
            try
            {
                GameObject.Find("SoundManagerMain").GetComponent<SoundManager>().Play("plant_pick");
            }
            catch (NullReferenceException ex)
            {
                Debug.Log("ERROR: " + ex);
                Debug.Log("Main Menu Sound Manager not found. Not a problem if not running from main menu");
            }
        } else {
            Debug.Log(gameObject.name+" is not picked up");
        }
    }

    public void addListenerToMySlot() {
        slot.GetComponent<Button>().onClick.AddListener(pickUp);
    }

}
