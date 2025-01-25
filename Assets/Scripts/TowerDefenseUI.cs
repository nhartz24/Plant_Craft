using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Canvas))] 

public class TowerDefenseUI : PlantUIController
{
    // inherited fields: Plant plant, GameObject PlantUIPanel, Camera camera
    
    private PlantDict plantDict;
    private NutrientManager nutrientManager;

    public float upgradePrice; 
    public float deletePrice;

    public GameObject plantNameObj;
    public GameObject upgradeButton;
    public GameObject upgradeText;
    public GameObject deleteText;

    private string sceneName;

    protected override void Start()
    {   
        base.Start();
        
        sceneName = SceneManager.GetActiveScene().name;
        // nutrient manager
        try {
            nutrientManager = GameObject.Find("Canvas"+sceneName).GetComponentInChildren<NutrientManager>();
        } catch (NullReferenceException ex) {
            Debug.Log("ERROR: "+ex);
            Debug.Log("Could not find the nutrient manager to attach to the canvas nested under a plant. Make sure the nutrient manager is nested under the main canvas, named Canvas.");
        }

        // plantdict
        try {
            plantDict = GameObject.FindObjectOfType<PlantDict>().GetComponent<PlantDict>();
        } catch (NullReferenceException ex) {
            Debug.Log("ERROR: "+ex);
            Debug.Log("Could not find the plantDict to attach to the canvas nested under a plant.");
        }
        
        
        // set up name
        string name = plant.plantName;

        if(plant.plantName.Contains("+")) {
            int nameIndex = -1;
            
            for (int i = 0; i < plantDict.keys.Count; i++)
            {
                if(plantDict.keys[i] == plant.plantName) {
                    nameIndex = i;
                }
            }

            if(nameIndex != -1) {
                name = plantDict.newNames[nameIndex];
            }
            else {
                Debug.Log($"ERROR: could not find the plant.plantName {plant.plantName} in PlantDict.keys.");
            }
        }
        

        if(name.Length > 12) {
            name = name.Substring(0, 12);
            name += "...";
        }
        plantNameObj.GetComponent<TMP_Text>().text = name;

        // add special case for pea plants' price
        upgradePrice = Mathf.Round(plant.price*1.5f);

        deletePrice = Mathf.Round(plant.price*0.75f);

        upgradeText.GetComponent<TMP_Text>().text = "Upgrade ("+upgradePrice.ToString()+")";
        deleteText.GetComponent<TMP_Text>().text = "Delete (+"+deletePrice.ToString()+")";
    }

    void Update()
    {
        if(upgradeButton != null && upgradeButton.activeInHierarchy && nutrientManager.nutrients < (int)upgradePrice) {
            upgradeButton.GetComponent<Button>().interactable = false;
        } else if (upgradeButton != null && upgradeButton.activeInHierarchy) {
            upgradeButton.GetComponent<Button>().interactable = true;
        } 
    }

    public void deletePlant() {
        Debug.Log("deleting plant");
        nutrientManager.Add((int)deletePrice);
        base.enableButtonsUnder();
        Destroy(transform.parent.gameObject);
    }

    public void upgradePlant() {
        nutrientManager.Subtract((int)upgradePrice);
        Damage damage = transform.parent.GetComponentInChildren<Damage>();
        NutrientProduction nutrient = transform.parent.GetComponent<NutrientProduction>();
        if(damage != null && damage.damageAmount != 0) {
            damage.upgradePlant();
        } else if (nutrient != null){
            nutrient.upgradePlant();
        } else {
            Debug.Log($"ERROR: Was unable to find the damage nor the nutrient production for {transform.parent.name}, and so could not upgrade the plant.");
        }
        Destroy(upgradeButton);
        transform.Find("PlantPanel/CloseWindow").GetComponent<Button>().onClick.Invoke();
    }

}
