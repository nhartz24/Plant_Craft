using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Unity.VisualScripting;
using TMPro;
using System;

public class Merge : MonoBehaviour
{

    public Plant parent0;         //the two plants being merged 
    public Plant parent1;
    private PlantDict plantsDict;       // the class holding all plants 

    public Transform plants;

    private GameObject newPlant;

    private GameObject slot;

    private int numOfSlots;

    private string strings;

    public Plant plant;

    private DialogueTrigger trigger;

    public CrossingUIController crossing0;
    public CrossingUIController crossing1;
    private string plantName;

     private string parent0Name;

    private string parent1Name;

    private string biomeTrait;

    private string attackTrait;

    private int nutrientAmount ;
    
    private float price; 

    private Sprite pic ;

    private GameObject template;

    private float powerLvl;

    private string plantDescription;

    private string fakeAssName;

    public DialogueTrigger inDict;

    public DialogueTrigger cantCross;

    public DialogueTrigger samePlant;
    public DialogueTrigger maxedMerge;
    private bool leveledUpPlant;

    private float productionSpeed;

    public List<string> nameCombos = new List<string>();


    private bool singles = false;


    

    public void setParent0(GameObject parent){
        plantsDict = FindObjectOfType<PlantDict>();

        parent0Name = plantsDict.ConvertAnyNameToKey(parent.name);
        
        // locating the scriptable object attached to object we dropped in the slot
        if (plantsDict.SO_Plants.ContainsKey(parent0Name)){
            parent0 = plantsDict.SO_Plants[parent0Name];
        }

         for(int j = 0; j < plantsDict.keys.Count; j++){
            if(plantsDict.keys[j] == parent0Name){

               fakeAssName = plantsDict.newNames[j];


                break;
            }
        }

        crossing0.SetVals(parent0, fakeAssName);
    }

    public void setParent1(GameObject parent){
        plantsDict = FindObjectOfType<PlantDict>();

        parent1Name = plantsDict.ConvertAnyNameToKey(parent.name);

        // locating the scriptable object attached to object we dropped in the slot
        if (plantsDict.SO_Plants.ContainsKey(parent1Name)){
            parent1 = plantsDict.SO_Plants[parent1Name];
        }

         for(int j = 0; j < plantsDict.keys.Count; j++){
            if(plantsDict.keys[j] == parent1Name){
               fakeAssName = plantsDict.newNames[j];


                break;
            }
        }

        crossing1.SetVals(parent1, fakeAssName);
    }

    public void MergeSet(){

        MergeAlg();


        int random = UnityEngine.Random.Range(0, 3);

            if (random == 0){
                powerLvl = parent0.powerLvl;
            }
            else if (random == 1){
                powerLvl = parent1.powerLvl;
            }
            else if (random == 2) {
                powerLvl = parent0.powerLvl + parent1.powerLvl;
            }


        for(int j = 0; j < plantsDict.keys.Count; j++){
            if(plantsDict.keys[j] == plantName){

                pic = plantsDict.pics[j];
                template = plantsDict.prefabs[j];
               plantDescription = plantsDict.descriptions[j];
               fakeAssName = plantsDict.newNames[j];


                break;
            }
        }

        crossing1.MergeSet(pic, fakeAssName, attackTrait, biomeTrait, powerLvl, plantDescription);

        

        
    }


    public void MergeAlg(){

        price = (parent0.price + parent1.price)/2;

        if (plantName.Contains("lily_of_the_valley")){
            attackTrait = "Poisonous";
            price *= 1.50f;

        }
        else {
            if(plantName.Contains("onion")){
                attackTrait = "Stinky";
                price *= 1.25f;

            }
            else{
                if (plantName.Contains("cactus")){
                    attackTrait = "Thorny";
                    price *= 1.25f;
                }
                else{
                    attackTrait = "Carnivorous";
                    price *= 2f;
                }
            }
        }

        if(plantName.Contains("venus") || plantName.Contains("pitcher")){
            biomeTrait = "Swamp";
            price += 10f;

        }
        else{
            if(plantName.Contains("cactus")){
                biomeTrait = "Desert";
                price += 5f;
            }
            else{
                biomeTrait = "Forest";
            }
        }

        if (plantName.Contains("pea")){
            price *= 1.25f;
        }


    }



    public void SplitNames(bool cero, bool uno, string firstName0, string secondName0, string firstName1, string secondName1){


        string newName = "";
        if(cero == true && uno == true){
            List<string> tmp = new List<string>();

            if (firstName0 != firstName1){ 
                tmp.Add(firstName0);

                tmp.Add(firstName1);
                tmp.Sort();

                newName = (tmp[0] + "+" + tmp[1]).ToString();

                nameCombos.Add(newName);

                Debug.Log("help1");

                tmp.Clear();
            }
            
            if (firstName0 != secondName1){
                tmp.Add(firstName0);

                tmp.Add(secondName1);
                tmp.Sort();

                newName = (tmp[0] + "+" + tmp[1]).ToString();

                nameCombos.Add(newName);

                Debug.Log(newName + "help2");
                tmp.Clear();
            }
            
            if (secondName0 != firstName1){ 
                
                tmp.Add(secondName0);

                tmp.Add(firstName1);
                

                tmp.Sort();

                newName = (tmp[0] + "+" + tmp[1]).ToString();

                nameCombos.Add(newName);
                Debug.Log(newName + "help3");
                tmp.Clear();

            }
            
            if (secondName0 != secondName1){ 

                tmp.Add(secondName0);

                tmp.Add(secondName1);
                tmp.Sort();

                newName = (tmp[0] + "+" + tmp[1]).ToString();

                nameCombos.Add(newName);
                Debug.Log(newName + "help4");
                tmp.Clear();
            }
            
        }
        else if (cero == true && uno == false){

            List<string> tmp = new List<string>();

            if (firstName0 != parent1Name){ 

                tmp.Add(firstName0);

                tmp.Add(parent1Name);
                tmp.Sort();

                newName = (tmp[0] + "+" + tmp[1]).ToString();

                nameCombos.Add(newName);
                tmp.Clear();
            }

            if (secondName0 != parent1Name){ 

                tmp.Add(secondName0);

                tmp.Add(parent1Name);
                tmp.Sort();

                newName = (tmp[0] + "+" + tmp[1]).ToString();

                nameCombos.Add(newName);

                tmp.Clear();

            }
        }

        else if (cero == false && uno == true){

            List<string> tmp = new List<string>();


            if (firstName1 != parent0Name){ 
                tmp.Add(firstName1);

                tmp.Add(parent0Name);
                tmp.Sort();

                newName = (tmp[0] + "+" + tmp[1]).ToString();

                nameCombos.Add(newName);
                tmp.Clear();
            }

            if (secondName1 != parent0Name){ 

                tmp.Add(secondName1);

                tmp.Add(parent0Name);
                tmp.Sort();

                newName = (tmp[0] + "+" + tmp[1]).ToString();

                nameCombos.Add(newName);
                tmp.Clear();

            }
        }

        else{
            if(parent0Name == parent1Name){
                newName = parent0Name;
                nameCombos.Add(newName);

            }

            else {
            List<string> tmp = new List<string>();

            tmp.Add(parent0Name);

            tmp.Add(parent1Name);
            tmp.Sort();

            newName = (tmp[0] + "+" + tmp[1]).ToString();

            nameCombos.Add(newName);
            tmp.Clear();

            }

            singles = true;


        }


        if (nameCombos.Count != 0){

            List<string> remove = new List<string>();


            foreach (string name in nameCombos){
                if(plantsDict.SO_Plants.ContainsKey(name)){
                    remove.Add(name);
                }

            }

            foreach (string name in remove){
                nameCombos.Remove(name);

            }

            // for (int j = 0; j < nameCombos.Count-1; j++){
            //     if(plantsDict.SO_Plants.ContainsKey(nameCombos[j])){

            //     nameCombos.Remove(nameCombos[j]);
            //     }
            // }

            foreach (string name in nameCombos){
                Debug.Log(name + "HELPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPP");

                Debug.Log("help me please");
            }

            int random = UnityEngine.Random.Range(0, nameCombos.Count);

            for (int i = 0; i < nameCombos.Count; i++){
                if(i == random){
                    plantName = nameCombos[i];
                }
            }            
        }
        else{
            plantName = "";
            }

    }

    public void CreateName(){


        bool cero = false;
        bool uno = false;

        string firstName1 = "";

        string secondName1 = "";

        string firstName0 = "";

        string secondName0 = "";


        if(parent0Name.Contains("+")){
            int position0 = parent0Name.IndexOf("+");

            for(int i = 0; i < parent0Name.Length; i++){
                if(i < position0){
                    firstName0 += parent0Name[i].ToString();                    
                }
                else if (i > position0){
                    secondName0 += parent0Name[i].ToString();
                }

                
                
            }

            leveledUpPlant = true;
            
            cero = true;

        }


        if(parent1Name.Contains("+")){

            int position1 = parent1Name.IndexOf("+");

            for(int i = 0; i < parent1Name.Length; i++){
                if(i < position1){
                    firstName1 += parent1Name[i].ToString();                    
                }
                else if (i > position1){
                    secondName1 += parent1Name[i].ToString();
                }

            }

            leveledUpPlant = true;
            uno = true;

        }

        SplitNames(cero, uno, firstName0, secondName0, firstName1, secondName1);

    }


    public void ResetParents(){
        GameObject slot0 = GameObject.Find("CanvasCrossing/Grid/Slot0");
        GameObject slot1 = GameObject.Find("CanvasCrossing/Grid/Slot1");
                    bool set = false;

                    plantsDict = FindObjectOfType<PlantDict>();

                    numOfSlots = plantsDict.SO_Plants.Count;



                    
                    for (int j = 2; j < numOfSlots+2; j++){
                        Debug.Log("AHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHH");

                        string newStrings = "CanvasCrossing/MergePanel/Scroll View/Viewport/SlotGrid/Slot" + j;
                    
                        GameObject newSlot = GameObject.Find(newStrings);

                        if (slot0.transform.childCount == 0 ){
                            set = true;
                        }

                        if(newSlot.transform.childCount == 0 && set == false && slot0.transform.childCount != 0){
                            slot0.transform.GetChild(0).SetParent(GameObject.Find("SelectedItem").transform);
                            slot0.GetComponent<InventorySlot>().resetSlot();
                            newSlot.GetComponent<InventorySlot>().placeItem();
                            
                            set = true;
                        }
                        else if(newSlot.transform.childCount == 0 && set == true && slot1.transform.childCount != 0){
                            slot1.transform.GetChild(0).SetParent(GameObject.Find("SelectedItem").transform);
                            slot1.GetComponent<InventorySlot>().resetSlot();
                            newSlot.GetComponent<InventorySlot>().placeItem();
                            break;
                        }
                    }

    }


    public void mergePlants(){

        numOfSlots = plantsDict.SO_Plants.Count;



       CreateName();

        for (int i = numOfSlots+1; i < numOfSlots+2; i++){
            strings = "CanvasCrossing/MergePanel/Scroll View/Viewport/SlotGrid/Slot" + i;
            slot = GameObject.Find(strings);

            if (slot.transform.childCount == 0){                
                // tmp.Add(parent0.name);
                // tmp.Add(parent1.name);
                // tmp.Sort();

                 //name = (tmp[0] + "+" + tmp[1]).ToString();

                if (SceneSwitch.gameLevel < 2 && plantsDict.SO_Plants.Count == 7){
                    maxedMerge.StartDialogue();

                }
                else if (SceneSwitch.gameLevel < 3 && plantsDict.SO_Plants.Count == 11){
                    maxedMerge.StartDialogue();
                }
                else if (SceneSwitch.gameLevel < 5 && plantsDict.SO_Plants.Count == 15){
                    maxedMerge.StartDialogue();
                }

                else if (SceneSwitch.gameLevel > 5 && plantsDict.SO_Plants.Count == 20){
                    maxedMerge.StartDialogue();

                }

                else if (nameCombos.Count == 0 && singles == true){

                    inDict.StartDialogue();
                    // samePlant.StartDialogue();

                }


                 else if (nameCombos.Count == 0 && leveledUpPlant == true){

                                        inDict.StartDialogue();

                    // samePlant.StartDialogue();
                    // leveledUpPlant = false;


                }
                 
                // else if (plantsDict.SO_Plants.ContainsKey(plantName) && leveledUpPlant == true){
                //     samePlant.StartDialogue();
                //     leveledUpPlant = false;

                // }

                else if ((plantName.Contains("venus") || plantName.Contains("pitcher")) && plantName.Contains("pea")){
                    
                    
                    
                //     (
                    
                // parent0.name.Contains("pea") && parent1.biomeTrait == "Swamp") ||
                //     (parent1.name.Contains("pea") && parent0.biomeTrait == "Swamp") ||
                //     (SceneSwitch.gameLevel == 49)){
                    cantCross.StartDialogue();

                }
                else if(plantsDict.SO_Plants.ContainsKey(plantName)){            
                    inDict.StartDialogue();
                }


                //if(plantsDict.SO_Plants.ContainsKey(plantName))
                //{            
                    
                //    inDict.StartDialogue();
                //    leveledUpPlant = false;
                //}

                else{
                    
                    MergeSet();


                    // biomeTrait = "";

                    // attackTrait = "";

                    // nutrientAmount = 0;
                    // //string peaTrait = "";

                    // price = 10f; //start at randomi parent price

                    // pic = plantsDict.pics[17];

                    // template = plantsDict.prefabs[17];

                    //PARENTS BIOMES READ FROM TAGS
                    //Debug.Log(parent0.biomeTrait);
                    //Debug.Log(parent1.biomeTrait);

                    newPlant = Instantiate(plants.gameObject);
                    newPlant.transform.SetParent(slot.transform);
                    newPlant.name = plantName;

                    if (!plantName.Contains("pea") && attackTrait != "Carnivorous"){
                        nutrientAmount = 0;                 // pea trait depending on Luca's code
                    }
                    else{
                         nutrientAmount = parent0.nutrientAmount + parent1.nutrientAmount;
                    }

                    // if (plantName.Contains("pea")){
                        productionSpeed = parent0.productionSpeed + parent1.productionSpeed;
                    // }

                    //epic sound effect
                    try
                    {
                        GameObject.Find("SoundManagerMain").GetComponent<SoundManager>().Play("new_plant");

                    }
                    catch (NullReferenceException ex)
                    {
                        Debug.Log("ERROR: " + ex);
                        Debug.Log("Main Menu Sound Manager not found. Not a problem if not running from main menu");
                    }


                    Debug.Log("new plant created: " + plantName + pic + "AHHHHH" +template + attackTrait + biomeTrait + "price " + price + "nutrient " + nutrientAmount + "speed" +productionSpeed);

                    plant = Plant.CreatePlant(plantName, pic, template, attackTrait, 
                                    biomeTrait, powerLvl, (int)price, nutrientAmount, productionSpeed);        // created new scriptable object plant 


                    plantsDict.Add(plant);           //adds the new scriptable object into a dictioanry with its name as a key

                    newPlant.GetComponent<UnityEngine.UI.Image>().sprite = plantsDict.SO_Plants[plantName].menuImage;

                    newPlant.GetComponent<UnityEngine.UI.Image>().rectTransform.localScale = new Vector3(1.4f,1.4f,0);

                    ResetParents();

                    

                    


                }

                nameCombos.Clear();

                leveledUpPlant = false;

                //  newPlant.GetComponent<UnityEngine.UI.Image>().sprite = plant.menuImage;

                break;

                // if either parent has biome trait : swamp then child has biome trait swamp 

                // if swamp cannot cross with pea 

                // if swamp then trait should not be carnivorous

                // if desert trait should not be spikey 

                //


            }

        }

        //Debug.Log("FindSlot");
    }
}
