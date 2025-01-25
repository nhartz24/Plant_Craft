using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantController : MonoBehaviour
{
    private Plant plant;
    private SupplyManager supplyManager;

    private bool nutrientsDisabled;
    
    // Start is called before the first frame update
    void Start()
    {
        supplyManager = GameObject.FindObjectOfType<SupplyManager>().GetComponent<SupplyManager>();
        supplyManager.EndPurchase();

        // fix layers
        gameObject.layer = LayerMask.NameToLayer("Plants");
        transform.GetChild(0).gameObject.layer = LayerMask.NameToLayer("Hitbox");

        // add components
        plant = GameObject.Find("PlantDictObj").GetComponent<PlantDict>().FindWithObjName(gameObject.name);

        //transform.Find("Hitbox").gameObject.AddComponent(typeof(Damage));
        
        if(plant.nutrientAmount != 0 && !nutrientsDisabled) {
            gameObject.AddComponent(typeof(NutrientProduction));
        }
    }

    public void disableNutrients() {
        nutrientsDisabled = true;
    }

}
