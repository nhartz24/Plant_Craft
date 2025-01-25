using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(fileName = "Noah Plant", menuName = "Noah Plant")]

public class NoahPlantClass : ScriptableObject
{

    [Header("Plant")]
    public string plantName; 

    public GameObject pic;

    public float attackSpeed;

    public float damageAmount;

    public float boxSizeX;

    public float boxSizeY;

    public void init(string name, GameObject newPic, float speed, float damage, float sizeX, float sizeY){
        Debug.Log("Warning: Creating NoahPlantClass - defunct.");

        plantName = name;

        pic = newPic;

        attackSpeed = speed;

        damageAmount = damage;

        boxSizeX = sizeX;

        boxSizeY = sizeY;

    }

    public static NoahPlantClass CreatePlant(string name, GameObject newPic, float speed, float damage, float sizeX, float sizeY)
    {
        var plant = ScriptableObject.CreateInstance<NoahPlantClass>();

        plant.init(name, newPic, speed, damage, sizeX, sizeY);
        return plant;
    }



    // public void init(string name, GameObject image){

    //     PlantName = name;

    //     pic = image;

    //     dict.SO_Plants.Add(PlantName, pic);

    //     Debug.Log(pic.name);

    // }

    // public static NoahPlantClass CreatePlant(string name, GameObject pic)
    // {
    //     var plant = ScriptableObject.CreateInstance<NoahPlantClass>();

    //     plant.init(name, pic);
    //     return plant;
    // }
}
