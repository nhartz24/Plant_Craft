/* using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;

//output to PlantDict
[RequireComponent(typeof(PlantDict))]

public class AssetManager : MonoBehaviour
{
    
    private PlantDict plantDict;

    // will correct assets folder at game start to only contain these plants
    string[] startingPlants = {"Lily of the Valley", "Onion", "Pea Plant", "Magic Flower"};

    
    // awake ensures that this runs before anything else
    void Awake() 
    {
        plantDict = GetComponent<PlantDict>();
        
        FixStartingAssets();
        UpdateDictWithAssets();
    }

    public void FixStartingAssets() {
        List<Plant> plantsInAssets = getAssets();
        bool isStartingPlant;        

        foreach (Plant plant in plantsInAssets)
        {
            isStartingPlant = false;
            foreach (string name in startingPlants)
            {
                if(plant.plantName == name) {
                    isStartingPlant = true;
                }
            }

            if (!isStartingPlant) {
                string plantKey = plantDict.ConvertAnyNameToKey(plant.plantName); 
                FileUtil.MoveFileOrDirectory("Assets/Resources/ActivePlants/"+plantKey+".asset", 
                    "Assets/Resources/InactivePlants/"+plantKey+".asset");
                FileUtil.MoveFileOrDirectory("Assets/Resources/ActivePlants/"+plantKey+".asset.meta", 
                    "Assets/Resources/InactivePlants/"+plantKey+".asset.meta");
                Debug.Log($"{plant.plantName} was found in the assets folder, but is not listed as a starting plant in the Asset Manager. It has been moved to the inactive folder.");
            }
        }
    }

    // Update PlantDict based on assets, only adding plants that are not already in the dict.
    // This should not affect non-asset plants created at runtime that have been added to the dict.
    public void UpdateDictWithAssets() {
        List<Plant> plantsInAssets = getAssets();

        foreach (Plant plant in plantsInAssets)
        {
            string keyPlantName = plantDict.ConvertAnyNameToKey(plant.plantName);
            if(!plantDict.SO_Plants.ContainsKey(keyPlantName)) {
                plantDict.Add(keyPlantName, plant);
                Debug.Log(keyPlantName+" has been put in PlantDict");
            }
        }
    }

    public void AddAsset(string plantName) {
        string plantKey = plantDict.ConvertAnyNameToKey(plantName); // name can be given as the plantName or object name

        FileUtil.MoveFileOrDirectory("Assets/Resources/InactivePlants/"+plantKey+".asset", 
            "Assets/Resources/ActivePlants/"+plantKey+".asset");
        FileUtil.MoveFileOrDirectory("Assets/Resources/InactivePlants/"+plantKey+".asset.meta", 
            "Assets/Resources/ActivePlants/"+plantKey+".asset.meta");
        
        Debug.Log($"moved {plantKey} to active plants");

        UpdateDictWithAssets();
    }

    public List<Plant> getAssets(){
        AssetDatabase.Refresh();

        List<Plant> p = new List<Plant>();

        Plant[] activePlants = Resources.LoadAll<Plant>("ActivePlants");
        foreach (Plant plant in activePlants)
        {
            p.Add(plant);
        }

        return p;
    }

} */