using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Plant ScriptableObject")]

public class Plant : ScriptableObject
{

    [Header("Plant")]
    public string plantName; 

    public Sprite menuImage; 

    public GameObject templateObject;

    public string attackTrait;

    public string biomeTrait;

    public float powerLvl;

    public int price; 

    public int nutrientAmount; //this is how much pea plants produce

    public float productionSpeed;



    public void init(string name, Sprite newPic, GameObject template, 
                    string traitAttack, string traitBiome, float pwrLvl, int cost, int amount, float speed) {

        plantName = name;

        menuImage = newPic;

        attackTrait = traitAttack;
        biomeTrait = traitBiome;

        powerLvl = pwrLvl;

        templateObject = template;

        price = cost;

        nutrientAmount = amount;

        productionSpeed = speed;

    }

    public static Plant CreatePlant(string name, Sprite newPic, GameObject template, 
                    string traitAttack, string traitBiome, float pwrLvl, int cost, int amount, float speed) 
    {
        var plant = ScriptableObject.CreateInstance<Plant>();

        plant.init(name, newPic, template, traitAttack, traitBiome, pwrLvl, cost, amount, speed);
        return plant;
    }

}
