using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(Canvas))] 

public class PlantUIController : MonoBehaviour
{
    protected Camera cam;
    protected GameObject PlantUIPanel;

    protected Plant plant;

    protected List<GameObject> buttonsUnder;

    protected virtual void Start()
    {   
        GetComponent<Canvas>().sortingLayerName = "PlantUI 1";
        
        // plant
        // Debug.Log("plantName: "+transform.parent.gameObject.name);
        string plantName = transform.parent.gameObject.name.Replace("(Clone)", "");  
        plantName = plantName.ToLower();
        plantName = plantName.Replace(" ", "_");
        
        try {
            plant = GameObject.Find("PlantDictObj").GetComponent<PlantDict>().SO_Plants[plantName];
        } catch (KeyNotFoundException ex) {
            Debug.Log("CAUGHT BUG: "+ex);
            Debug.Log("Make sure the prefab/object is named in accordance with the scriptable object. It should match the plantName in "+
            "the scriptable object, with spaces or underscores between words.");
        }
        
        // cam
        try {
            cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        } catch (NullReferenceException ex) {
            Debug.Log("ERROR: "+ex);
            Debug.Log("Could not find the cam to attach to the canvas nested under a plant. Make sure the cam is named Main Camera.");
        }

        GetComponent<Canvas>().worldCamera = cam;

        // plant panel
        try {
            PlantUIPanel = transform.Find("PlantPanel").gameObject; 
        } catch (NullReferenceException ex) {
            Debug.Log("ERROR: "+ex);
            Debug.Log("Could not find the PlantPanel under the PlantCanvas. Check its name.");
        }

        buttonsUnder = findButtonsUnder();
    }

    public void openPanel() {
        //PlantUIPanel.SetActive(true);
        foreach (Transform item in PlantUIPanel.transform)
        {
            item.gameObject.SetActive(true);
        }

        buttonsUnder = findButtonsUnder();
        disableButtonsUnder();
    }

    public void closePanel() {
        //PlantUIPanel.SetActive(false);
        foreach (Transform item in PlantUIPanel.transform)
        {
            if(item.name != "PlantButton") {
                item.gameObject.SetActive(false);
            }
        }

        enableButtonsUnder();
    }

    public List<GameObject> findButtonsUnder() {
        RectTransform rect = GetComponent<RectTransform>();
        Vector2 point = new Vector2(transform.position.x, transform.position.y);
        Vector2 size = new Vector2(rect.sizeDelta.x+0.5f, rect.sizeDelta.y+0.5f);
        
        List<GameObject> plants = new List<GameObject>(); 

        Collider2D[] colliders = Physics2D.OverlapBoxAll(point, size, 0f);
        foreach (Collider2D collider in colliders)
        {
            if(collider.tag == "Plant") {
                //Debug.Log($"{collider.name} is under {transform.parent.name}");
                plants.Add(collider.gameObject);
            }
        }
        
        List<GameObject> plantButtons = new List<GameObject>();
        foreach (GameObject plant in plants)
        {
            GameObject button = null;
            button = plant.transform.Find("PlantCanvas/PlantPanel/PlantButton").gameObject;
            if(button == null) {Debug.Log("ERROR: Could not find button to disable on a plant that is under another plant's plantUI.");}
            else {plantButtons.Add(button);}
        }

        return plantButtons;
    }
    
    public void disableButtonsUnder() {
        //Debug.Log("disabling under");
        foreach (GameObject plantButton in buttonsUnder)
        {
            //Debug.Log($"disabling {plantButton.transform.parent.parent.parent.name}'s button");
            plantButton.SetActive(false);
        }
    }

    public void enableButtonsUnder() {
        //Debug.Log("enabling under");
        foreach (GameObject plantButton in buttonsUnder)
        {
            //Debug.Log($"enabling {plantButton.transform.parent.parent.parent.name}'s button");
            plantButton.SetActive(true);
        }
    }

}
