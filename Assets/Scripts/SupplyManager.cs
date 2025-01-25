using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

// needs to be on the canvas displaying PlantSupply UI
[RequireComponent(typeof(Canvas))]


public class SupplyManager : MonoBehaviour
{
    private List<GameObject> slots;
    public GameObject slotPrefab;
    private List<Plant> plants; 

    public GameObject placeableAreaTilemap;

    private RectTransform rectTransform;
    private PlantDict plantDict;
    private NutrientManager nutrientManager;

    private Transform scrollViewObj;
    private Transform scrollContentObj;

    public float verticalSpacing = 50;
    public float horizontalSpacing = 70;

    public float spaceFromBottom = 30;

    private Vector2 startingLocation;

    private int oldNutrients;
    public bool canBuy; // need to add to update so that if this is false and nutrient.canbuy is true, update the supply and reset canbuy in here
    
    public bool inPurchase;

    // set up to only load in the plantsupply once, at start - does not update live
    void Start()
    {
       // try to find the PlantDictObj in the scene
       GameObject plantDictObj = GameObject.Find("PlantDictObj");
        if(plantDictObj == null) { 
            Debug.Log("PlantDictObj could not be found; check its name, and that it exists in the scene.");
            // note that GameObject.Find searches across all active scenes, according to the documentation
        } else {
            plantDict = plantDictObj.GetComponent<PlantDict>();
        }

        // check for placeableAreaTilemap
        if(placeableAreaTilemap == null) { 
            Debug.Log("ERROR: PlaceableAreaTilemap was not assigned in the Supply Manager in the Canvas object.");
        } 

        // try to find the NutrientManager in UI on this canvas
        nutrientManager = GetComponentInChildren<NutrientManager>();
        try {
            int n = nutrientManager.nutrients;
        } catch (NullReferenceException ex) {
            Debug.Log("ERROR: "+ex);
            Debug.Log("Couldn't find NutrientManager component in children of the Canvas.");
        }

        // try to find the ScrollView and Content
        try {
            scrollViewObj = GetComponentInChildren<ScrollRect>().transform;
        } catch (NullReferenceException ex) {
            Debug.Log("ERROR: "+ex);
            Debug.Log("Couldn't find ScrollRect component in children of the Canvas.");
        }
        try {
            scrollContentObj = scrollViewObj.GetChild(0).GetChild(0);
        } catch (NullReferenceException ex) {
            Debug.Log("ERROR: "+ex);
            Debug.Log("Couldn't find the Content object as a child of the Scroll View.");
        }

        // make sure canvas is connected to camera
        Canvas canvas = GetComponent<Canvas>();
        if (canvas.worldCamera == null) {
            Debug.Log("Canvas has not been connected to the main camera - automatically connecting now.");
            try {
                Camera mainCam = GameObject.Find("Main Camera").GetComponent<Camera>();
                canvas.worldCamera = mainCam;
            } catch (NullReferenceException ex) {
                Debug.Log("ERROR: "+ex);
                Debug.Log("Could not find the main camera. Check if its name has changed from Main Camera. UI will not scale appropriately.");
            }
        }

        rectTransform = GetComponent<RectTransform>();

        int contentHeight = ScrollViewController.getHeight();
        startingLocation = new Vector2(20,(contentHeight/2)-50);
        Debug.Log("The starting location is "+startingLocation.y);

        if(slotPrefab == null){ Debug.Log("ERROR: Slot prefab is not set in Supply Manager.");}

        inPurchase = false;


        // Make Slots:
        slots = new List<GameObject>();

        plants = new List<Plant>();
        //plants = plantDict.Plants;
        foreach(Plant p in plantDict.Plants) {
            if(p.plantName != "Magic Flower"){
                plants.Add(p);
                createSlot(p);
            }
        }
        Debug.Log("List of plants in supply manager: "+plants.Count+", and list of plants in PlantDict: "+plantDict.Plants.Count);
    }

    void Update()
    {
        if(!canBuy && nutrientManager.canBuy) {
            UpdateAvailableSupply();
            canBuy = true;
        }



    }


    public void createSlot(Plant plant) {
        Debug.Log("Making slot for "+plant.plantName);

        // note that all calculations are made in LOCAL position, not world
        if(slots.Count == 0) { // for the first slot
            Vector2 firstLoc = startingLocation;

            GameObject firstSlot = Instantiate(slotPrefab, scrollContentObj); 

            firstSlot.GetComponent<RectTransform>().anchoredPosition = firstLoc;

            firstSlot.GetComponent<SupplySlot>().AddPlant(plant);
            slots.Add(firstSlot);
            return;
        }
        
        //last item in slots, get transform, place new slot below, if beyond screen height, start new row
        Vector2 lastLoc = slots[slots.Count-1].GetComponent<RectTransform>().anchoredPosition;
        float newY = lastLoc.y-verticalSpacing;

        Vector2 loc = new Vector2(lastLoc.x,newY);
        GameObject slotObj = Instantiate(slotPrefab, scrollContentObj);

        slotObj.GetComponent<RectTransform>().anchoredPosition = loc;
        slotObj.GetComponent<SupplySlot>().AddPlant(plant);
        slots.Add(slotObj);
    }



    public void StartPurchase(int price){
        nutrientManager.Subtract(price);
        placeableAreaTilemap.SetActive(true);
        inPurchase = true;
        disableAllBuying();
    }

    public void EndPurchase() {
        enableAllBuying();
        placeableAreaTilemap.SetActive(false);
        inPurchase = false;
        UpdateAvailableSupply();
    }



    public void UpdateAvailableSupply(){
        if(inPurchase) {
            return;
        } 
        else 
        {
            foreach (GameObject slot in slots)
            {
                if(slot.GetComponent<SupplySlot>().plant.price > nutrientManager.nutrients){
                    slot.GetComponent<Button>().interactable = false;
                } else {
                    slot.GetComponent<Button>().interactable = true;
                }
            }
        }
    }



    public void disableAllBuying() {
        foreach (GameObject slot in slots)
        {
            slot.GetComponent<Button>().interactable = false;
        }
    }

    public void enableAllBuying() {
        foreach (GameObject slot in slots)
        {
            slot.GetComponent<Button>().interactable = true;
        }
    }
    
}




