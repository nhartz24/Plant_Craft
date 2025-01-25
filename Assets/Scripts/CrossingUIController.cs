using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(Canvas))] 
public class CrossingUIController : MonoBehaviour
{
    private GameObject PlantUIPanel;

    public GameObject parentName;
    public GameObject attackText;
    public GameObject biomeText;

    public GameObject powerText;


    private string attackTrait;

    private string plantName;
    
    private string biomeTrait;

    private float powerLvl;

    public GameObject plantPic;



    public GameObject mergeName;
    public GameObject mergeAttack;
    public GameObject mergeBiome;

    public GameObject mergePower;


    public GameObject mergedInfo;
    public GameObject mergedName;
    public GameObject mergedAttack;
    public GameObject mergedBiome;

    public GameObject mergedPower;

    public GameObject mergedDescription;

    private bool merged;

    public void Update(){
        if (Input.GetMouseButtonDown(0) && merged == true){
            mergedInfo.SetActive(false);
            merged = false;
        }
    }
    public void SetVals(Plant parent, string fakeAssName){

        plantName =  fakeAssName;

        attackTrait = parent.attackTrait;

        biomeTrait = parent.biomeTrait;

        powerLvl = parent.powerLvl;

        parentName.GetComponent<TMP_Text>().text = "Plant Name: " + plantName.ToString();

        attackText.GetComponent<TMP_Text>().text = "Atack Trait: "+attackTrait.ToString();
        biomeText.GetComponent<TMP_Text>().text = "Biome Trait: "+biomeTrait.ToString();

        powerText.GetComponent<TMP_Text>().text = "Power Lvl: "+powerLvl.ToString();

    }

    public void UnSetVals(){

        parentName.GetComponent<TMP_Text>().text = "Plant Name: ";

        attackText.GetComponent<TMP_Text>().text = "Atack Trait: ";
        biomeText.GetComponent<TMP_Text>().text = "Biome Trait: ";

        powerText.GetComponent<TMP_Text>().text = "Power Lvl: ";

    }

    public void MergeSet(Sprite pic, string plantName, string attackTrait, string biomeTrait, float powerLvl, string description){
        //runs all the visuals for mergedinfo in crossing scene

        mergedInfo.SetActive(true);

        mergedName.GetComponent<TMP_Text>().text = "Name: " + plantName.ToString();

        mergedAttack.GetComponent<TMP_Text>().text = "Atack Trait: "+attackTrait.ToString();
        mergedBiome.GetComponent<TMP_Text>().text = "Biome: "+biomeTrait.ToString();

        mergedPower.GetComponent<TMP_Text>().text = "Power Level: "+powerLvl.ToString();
        
        mergedDescription.GetComponent<TMP_Text>().text = description.ToString();


        plantPic.GetComponent<Image>().sprite = pic;

        merged = true;

    }

    public void MergeUnSet(){

        mergedInfo.SetActive(false);
        plantPic.GetComponent<Image>().sprite = null;

        mergedName.GetComponent<TMP_Text>().text = "Plant Name: " ;

        mergedAttack.GetComponent<TMP_Text>().text = "Atack Trait: ";
        mergedBiome.GetComponent<TMP_Text>().text = "Biome Trait: ";

        mergedPower.GetComponent<TMP_Text>().text = "Power Lvl: ";

        mergedDescription.GetComponent<TMP_Text>().text = "Plant Info: ";
    }


    // public void Set(string plantName, string attackTrait, string biomeTrait, float powerLvl){

    //     mergeName.GetComponent<TMP_Text>().text = "Plant Name: " + plantName.ToString();

    //     mergeAttack.GetComponent<TMP_Text>().text = "Atack Trait: "+attackTrait.ToString();
    //     mergeBiome.GetComponent<TMP_Text>().text = "Biome Trait: "+biomeTrait.ToString();

    //     mergePower.GetComponent<TMP_Text>().text = "Power Lvl: "+powerLvl.ToString();


    // }

    // public void UnSet(){
    //     mergeName.GetComponent<TMP_Text>().text = "Plant Name: " ;

    //     mergeAttack.GetComponent<TMP_Text>().text = "Atack Trait: ";
    //     mergeBiome.GetComponent<TMP_Text>().text = "Biome Trait: ";

    //     mergePower.GetComponent<TMP_Text>().text = "Power Lvl: ";

    // }
    



    public void openPanel() {
        PlantUIPanel.SetActive(true);
    }

    public void closePanel() {
        PlantUIPanel.SetActive(false);
    }
}
