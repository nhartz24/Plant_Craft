using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NutrientProduction : MonoBehaviour
{
    public bool automated;
    
    private int nutrientAmount;
    private float productionSpeed;

    private Plant plant;

    private DamagePopup popup; // popup prefab actually stored in nutrientManager; this way you can add nutrientProduction component at runtime
    private GameObject canvas;
    private EndLevel endController;
    private NutrientManager nutrientManager;

    private string sceneName;

    void Start()
    {
        sceneName = SceneManager.GetActiveScene().name;
        canvas = GameObject.Find("Canvas"+sceneName);
        PlantDict plantDict = GameObject.Find("PlantDictObj").GetComponent<PlantDict>();
        plant = plantDict.FindWithObjName(gameObject.name);

        nutrientAmount = plant.nutrientAmount;
        productionSpeed = plant.productionSpeed;

        Debug.Log($"{plant.plantName}'s nutrient amount is {nutrientAmount} and production speed is {productionSpeed}");

        if(plant.attackTrait == "Carnivorous") {
            automated = false;
        } else {
            automated = true;
        }

        endController = canvas.GetComponent<EndLevel>();
        nutrientManager = canvas.GetComponentInChildren<NutrientManager>();

        popup = nutrientManager.nutrientPopupPrefab; 
        
        if(popup == null) { Debug.Log("ERROR: NutrientPopup has not been assigned in the NutrientManager.");}
        
        if (automated) {
            StartCoroutine(AutoProduce());
        }
    }

    IEnumerator AutoProduce(){
        while (!endController.isEnded){
            yield return new WaitForSeconds(1/(productionSpeed/2));
            
            // ((1f/Mathf.Pow(2f,(productionSpeed-1)))*5f);         
            Produce();
        }
    }

    public void Produce() {
        Debug.Log($"producing {nutrientAmount} nutrients from {plant.plantName}");

        Debug.Log(plant.plantName + "HELPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPP");
        if(plant.plantName.Contains("lily") || plant.plantName.Contains("cactus") || plant.plantName.Contains("onion")){
             nutrientManager.Add(nutrientAmount/2);
        }

        else{
            nutrientManager.Add(nutrientAmount);
        }
       
        popup.CreateWithPlus(transform.position, nutrientAmount, Color.green);
    }

    public void upgradePlant() {
        Debug.Log("upgrading nutrient amount!");
        nutrientAmount += nutrientAmount/2;
        Debug.Log($"nutrient amount is now {nutrientAmount}");
    }

    public void Disable() {
        nutrientAmount = 0;
        productionSpeed = 0;
    }

}
