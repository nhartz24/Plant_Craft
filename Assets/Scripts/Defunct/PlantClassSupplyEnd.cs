using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(fileName = "New Plant", menuName = "New Plant")]

public class PlantClassSupplyEnd : ScriptableObject
{

    [Header("Plant")]
    public string PlantName;
    public string headPic;
    public string stemPic;
    public string rootPic;

    public string attackTrait;

    public string biomeTrait;

    public float powerLvl;

    public GameObject templateObject;

    public Sprite menuImage; 

    public float price; 



    public void init(string name, string head, string stem, string root, string attack, string  biome, float power, GameObject template, Sprite image, float cost){
        Debug.Log("Warning: Creating PlantClassSupplyEnd - defunct and incompatible.");

        PlantName = name;
        headPic = head;
        stemPic = stem;
        rootPic = root;
        attackTrait = attack;
        biomeTrait = biome;
        powerLvl = power;
        templateObject = template;
        menuImage = image;
        price = cost;
    }

    public static PlantClassSupplyEnd CreatePlant(string name, string head, string stem, string root, string attack, string  biome, float power, GameObject template, Sprite image, float cost)
    {
        var plant = ScriptableObject.CreateInstance<PlantClassSupplyEnd>();

        plant.init(name, head, stem, root, attack, biome, power, template, image, cost);
        return plant;
    }
}
