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

public class TutorialManagerForest : MonoBehaviour
{
    private Transform canvas;
    private Transform[] children;

    [SerializeField]
    private GameObject dialogueBox; //connects to Noahs Dialogue Code, drag and drop to be speciffic

    public GameObject button;


    public static bool upgradetutorial;

    void Start()
    {
        GameObject canvasObject = GameObject.Find("CanvasForestLevel");
        canvas = canvasObject.transform;

        upgradetutorial = false;

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

        SceneManager.UnloadSceneAsync("ForestLevel");

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

    public void beginUpgradeTutorial() {

        // dialogueBox.SetActive(true);


        FindObjectOfType<SoundManager>().Play("level_start"); //play sound for starting level
        //soundManager.Play("level_start");
        //triggerpea.enabled = false;
        //triggerpoison.enabled = false;
        //triggerstink.enabled = false;

        button.SetActive(false); //hides the button

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

        Transform startbutton = canvas.Find("StartButton"); //YOU CAN NOW PLAY LEVEL
                                                            //startbutton.gameObject.SetActive(true);
        startbutton.GetComponent<StartLevel>().startLevel();
        Transform progressBar = canvas.Find("ProgressBar");
        progressBar.GetComponent<ProgressBar>().WaveStart();

        if (upgradetutorial == false)
        {
            StartCoroutine(upgradingDialogue());
        }
        
    }

    IEnumerator upgradingDialogue()
    {
        yield return new WaitForSeconds(3f);

        DialogueTrigger trigger = dialogueBox.GetComponent<DialogueTrigger>();

        //tigger.enabled = true;

        //trigger = FindAnyObjectByType<DialogueTrigger>();

        trigger.StartDialogue();

        upgradetutorial = true;
    }
        

    


}
