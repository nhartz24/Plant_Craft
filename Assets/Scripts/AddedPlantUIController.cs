using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AddedPlantUIController : MonoBehaviour
{
    public Plant plant; // filled by EndLevel
    public Image PlantPic;
    public TMP_Text PlantName;
    public TMP_Text PlantAttack;
    public TMP_Text PlantBiome;
    public TMP_Text PlantLvl;
    public TMP_Text PlantDescription;

    private PlantDict plantDict;

    void Start()
    {
        plantDict = GameObject.FindObjectOfType<PlantDict>().GetComponent<PlantDict>();

        string plantName = plant.plantName;


        PlantPic.sprite = plant.menuImage;
        PlantName.text = "Name: "+plantName;
        PlantAttack.text = "Attack Trait: "+plant.attackTrait;
        PlantBiome.text = "Biome: "+plant.biomeTrait;
        PlantLvl.text = "Power Level: "+plant.powerLvl;

        if (plantName == "Cactus"){
            PlantDescription.text = plantDict.descriptions[3];
            
        }
        else if (plantName == "Venus Fly Trap"){
            PlantDescription.text = plantDict.descriptions[4];
            
        }
        else if (plantName == "Pitcher"){
            PlantDescription.text = plantDict.descriptions[5];
            
        }

        // add plant to plantDict
        plantDict.Add(plant);
    }

}
