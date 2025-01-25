using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using System.Runtime.CompilerServices;
using System.Data;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{
    private Transform canvas;
    private Transform[] children;

    [SerializeField]
    private GameObject dialogueBox; //connects to Noahs Dialogue Code, drag and drop to be speciffic


    [SerializeField]
    private GameObject peaDialogueBox; //pea dialogue trigger
    private bool peatriggered;
    [SerializeField]
    private GameObject stinkDialogueBox;
    private bool stinktriggered;
    [SerializeField]
    private GameObject posionDialogueBox;
    private bool poisontriggered;

   
    public static int tutorialStage = 0;

    public GameObject button;

    public GameObject magicPlant;

    public Health magicFlower;

    public WaveSpawner waveSpawner; 

    public GameObject warningDialogueBox;
    
    public GameObject endDialogueBox;

    public GameObject[] pointers; //store all the UI elements that give tutorials
    

    void Start()
    {
        GameObject canvasObject = GameObject.Find("CanvasTutorialLevel");
        canvas = canvasObject.transform;

        peatriggered = false; stinktriggered = false; poisontriggered = false;

        // dialogueBox.SetActive(false);

        // peaDialogueBox.SetActive(false);

        // stinkDialogueBox.SetActive(false);

        // posionDialogueBox.SetActive(false);

        // warningDialogueBox.SetActive(false);

        // endDialogueBox.SetActive(false);        


        //SceneManager.SetActiveScene(SceneManager.GetSceneByName("TutorialLevel"));
        //SoundManager soundManager = GameObject.Find("SoundManagerMain").GetComponent<SoundManager>();

        // these things should be true in the scene before start is pressed:
        // wavespawner component on the GameManager is disabled
        // NutrientLabel and SpeedButtons are disabled
        // SupplyManager component on Canvas is disabled
    }

    public void DestroyPlants(){
        foreach(GameObject plant in GameObject.FindGameObjectsWithTag("Plant")){
                Destroy(plant);
            }

    }

    public void DestroyPopups(){
        foreach(GameObject popup in GameObject.FindGameObjectsWithTag("Popup")){
                Destroy(popup);
            }

    }

    public void DestroyBugs(){
        foreach(GameObject bug in GameObject.FindGameObjectsWithTag("Enemy")){
                Destroy(bug);
            }

    }
    public void Reset(){

        SceneManager.UnloadSceneAsync("TutorialLevel");

        // DestroyPlants();
        // DestroyBugs();
        // DestroyPopups();

        // magicFlower.ResetHealth();
        // tutorialStage = 0;

        // children = canvas.GetComponentsInChildren<Transform>(true);

        // foreach (Transform obj in children)
        // {
        //     if(obj.name == "StartTutorial" || obj.name == "StartTutorialText" ){
        //         Debug.Log("AHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHH");
        //         obj.gameObject.SetActive(true);
        //     }
        //     if (obj.name != "StartTutorial" && obj.name != "PauseButton" && obj.name != "StartTutorialText" && obj.name != "CanvasTutorialLevel" && obj.name != "HealthBar" && obj.name != "Background" && obj.name != "Fill" && obj.name != "NutrientLabel" && obj.name != "NutrientNum")
        //     {
        //         obj.gameObject.SetActive(false);
        //     }

           
        //     //obj.gameObject.SetActive(true);
        // }

        // magicPlant.SetActive(true);

        // Transform pointer = transform.GetChild(0).GetChild(0);
        // pointer.gameObject.SetActive(false);

        // Transform pointer2 = transform.GetChild(0).GetChild(1);
        // pointer2.gameObject.SetActive(false);

        // waveSpawner.Reset();

        // Time.timeScale = 1;


        // Transform startbutton = canvas.Find("StartButton"); //YOU CAN NOW PLAY LEVEL
        // startbutton.gameObject.SetActive(false);
        
    }

    public void Warning(){
        warningDialogueBox.SetActive(true);
    }

    public void beginTutorial() {

        // dialogueBox.SetActive(true);


        //FindObjectOfType<SoundManager>().Play("level_start"); //play sound for starting level
        //soundManager.Play("level_start");

        tutorialStage = 1;
        //triggerpea.enabled = false;
        //triggerpoison.enabled = false;
        //triggerstink.enabled = false;

       DialogueTrigger trigger = dialogueBox.GetComponent<DialogueTrigger>();
       
    //    tigger.enabled = true;

        //trigger = FindAnyObjectByType<DialogueTrigger>();

        trigger.StartDialogue();

        button.SetActive(false); //hides the button



        //gameObject.GetComponent(UnityEngine.UI.Image).enable(false); //hide the button so no shenanigans
        // dialogueProgress = trigger.GetComponent<DialogueManager>()
    }

    public void placingTutorial()
    {
        dialogueBox.SetActive(false);

        //trigger.gameObject.SetActive(false); //turn off dialogue so no conflicts

        tutorialStage = 2; //should be 2 for this stage

        canvas.Find("NutrientLabel").gameObject.SetActive(true); // enable specifically the nutrient label, so it can be used in supply manager
        canvas.GetComponent<SupplyManager>().enabled = true;

        children = canvas.GetComponentsInChildren<Transform>(true);

        foreach (Transform obj in children)
        {
            if (obj.name != "StartButton" && obj.name != "StartTutorial" && obj.name != "ProgressBar" && obj.name != "UnPause" && obj.name != "UnPauseText") //keep start button hidden
            {
                obj.gameObject.SetActive(true);
            }
            //obj.gameObject.SetActive(true);
        }
        
        pointers[0].SetActive(true);
        pointers[1].SetActive(true);
        pointers[2].SetActive(true);
        //Transform pointer = transform.GetChild(0).GetChild(0);
        //pointer.gameObject.SetActive(true);
        

    }

    public void startTutorialLevel()
    {
        //if (peatriggered == true && stinktriggered == true && poisontriggered == true)
        //{
            tutorialStage = 3; //already been placed, now able to start the level

            Transform pointer = transform.GetChild(0).GetChild(0);
            pointer.gameObject.SetActive(false);

            Transform startbutton = canvas.Find("StartButton"); //YOU CAN NOW PLAY LEVEL
            //startbutton.gameObject.SetActive(true);
            startbutton.GetComponent<StartLevel>().startLevel();
            Transform progressBar = canvas.Find("ProgressBar");
            progressBar.GetComponent<ProgressBar>().WaveStart();
        //}
        StartCoroutine(pauseButtonHelp());

        
    }

    public void peaPlantTutorial()
    {
        
        if (peatriggered == false)
        {
            peaDialogueBox.SetActive(true);
            //triggerpea.gameObject.SetActive(true);
            tutorialStage = 9; //this stage is just to tell something to dialogue manager

            peaDialogueBox.GetComponent<DialogueTrigger>().StartDialogue();

            pointers[3].SetActive(true);
        }
        peatriggered = true;

    }

    public void endPeaTutorial()
    {

        peaDialogueBox.SetActive(false);

        //IGNOREEEEEEEEEE
        //triggerpea.gameObject.SetActive(false); //no more pea :((
        //Transform pointer2 = transform.GetChild(0).GetChild(1);
        //pointer2.gameObject.SetActive(false);
        //transform.GetChild(0).GetChild(0).GetChild(gameObject.SetActive(false);

        pointers[3].SetActive(false); //this one is the nutrients
        pointers[2].SetActive(false);
        tutorialStage = 3; //returns to normal after dialogue played



        startTutorialLevel();

        GameObject.Find("NutrientLabel").GetComponentInChildren<NutrientManager>().UpdateNutrientsTutorial();


        


    }
    

    public void onionTutorial()
    {
        
        if (stinktriggered == false)
        {
            pointers[1].SetActive(false);
            stinkDialogueBox.SetActive(true);
            //triggerstink.gameObject.SetActive(true);

            tutorialStage = 6;
            stinkDialogueBox.GetComponent<DialogueTrigger>().StartDialogue();
        }
        stinktriggered = true;
    }

    public void endOnionTutorial()
    {
        stinkDialogueBox.SetActive(false);

        
        //triggerstink.gameObject.SetActive(false);
        tutorialStage = 3;
    }
    public void lilyTutorial()
    {
        if (poisontriggered == false)
        {
            posionDialogueBox.SetActive(true);
            pointers[0].SetActive(false);
            //triggerpoison.gameObject.SetActive(true);
            tutorialStage = 7;
            posionDialogueBox.GetComponent<DialogueTrigger>().StartDialogue();
            
        }
        poisontriggered = true;
        //triggerpoison.gameObject.SetActive(false);
    }
    public void endLilyTutorial()
    {
        posionDialogueBox.SetActive(false);
        //triggerpoison.gameObject.SetActive(false);
        tutorialStage = 3;
    }
    IEnumerator pauseButtonHelp()
    {
        pointers[4].SetActive(true);
        yield return new WaitForSeconds(3f);
        pointers[4].SetActive(false);
    }

    public void wonFirstLevel()
    {
        Debug.Log("you won now dialogue time");
        tutorialStage = 10;
        // endDialogueBox.GetComponent<DialogueTrigger>().StartDialogue();
        //heres where we can put ok yay you won your level dialogue
    }
    public void giveDesertPlant()
    {
        endDialogueBox.SetActive(false);
        Debug.Log("yipppeee kayaee");
        FindObjectOfType<EndLevel>().GetPlant();
        tutorialStage = 10;
    }

}
