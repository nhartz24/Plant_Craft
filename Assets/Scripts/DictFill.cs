using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DictFill : MonoBehaviour
{

    public Plant parent0;

    public Plant parent1;

    public PlantDict plantsDict;

    //public Damage damage;

    public void fillDict(){

        plantsDict.SO_Plants.Add(parent0.plantName, parent0);      

        plantsDict.SO_Plants.Add(parent1.plantName, parent1);     

        //damage.updatePlant();   
  


    }

}
