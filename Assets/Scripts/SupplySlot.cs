using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class SupplySlot : MonoBehaviour
{
    public Plant plant;
    public GameObject plantTemplate;

    private SupplyManager supplyManager;
    public TMP_Text cost;

    public Sprite forest;
    public Sprite desert;
    public Sprite swamp;

    void Start()
    {
        supplyManager = GetComponentInParent<SupplyManager>();
        //cost = GetComponentInChildren<TMP_Text>();
    }


    public void AddPlant(Plant p){
        plant = p;
        plantTemplate = plant.templateObject;
        transform.GetChild(0).gameObject.GetComponent<UnityEngine.UI.Image>().sprite = plant.menuImage;

        if(plant.biomeTrait == "Forest"){
            gameObject.GetComponent<UnityEngine.UI.Image>().sprite = forest;

        }

        else if (plant.biomeTrait == "Desert"){
            gameObject.GetComponent<UnityEngine.UI.Image>().sprite = desert;

        }

        else if (plant.biomeTrait == "Swamp"){
            gameObject.GetComponent<UnityEngine.UI.Image>().sprite = swamp;

        }

        Debug.Log("cost's text: "+cost.text);

        cost.text = plant.price.ToString();
        
    }

    public void spawnTemplate(){
        TemplatePlacer templatePlacer = plantTemplate.GetComponent<TemplatePlacer>();
        templatePlacer.slot = GetComponent<SupplySlot>(); // this component

        Instantiate(plantTemplate);
        supplyManager.StartPurchase(plant.price);
    }

    /* public void isPlaced(){
        supplyManager.EndPurchase();
    } */
}
