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

public class CrossTutorialManager : MonoBehaviour
{
    private Transform canvas;
    private Transform[] children;

    [SerializeField]
    private DialogueTrigger trigger1; //connects to Noahs Dialogue Code, drag and drop to be speciffic
    [SerializeField]
    private DialogueTrigger trigger2; //second dialogue code

    [SerializeField]
    private Transform pointer1; //these are UI elements that show what to do, starting with pointer1

    private DialogueManager dialogueProgress;

    void Start()
    {
        


        GameObject canvasObject = GameObject.Find("CanvasCrossing");
        canvas = canvasObject.transform;

        // these things should be true in the scene before start is pressed:
        // wavespawner component on the GameManager is disabled
        // NutrientLabel and SpeedButtons are disabled
        // SupplyManager component on Canvas is disabled
    }
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        Debug.Log("[CTM] crossing tutorial loaded!");
        Debug.Log("[CTM] Tutorial Stage is...." + TutorialManager.tutorialStage);
        //STAGE 10  means move to crossing scene ok
        if (TutorialManager.tutorialStage == 10) //when the scene is loaded, if at right spot in tutorial, start the dialogue 
        {
            Debug.Log("[CTM] tutorial crossing starts! ! !  !");
            StartCoroutine(waitingTime());
        }
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // here you can use scene.buildIndex or scene.name to check which scene was loaded
        if (scene.name == "Crossing")
        {
            

        }
    }
    IEnumerator waitingTime()
    {
        yield return new WaitForSeconds(1f);
        startCrossingDialogue();
    }
    public void startCrossingDialogue()
    {
        Debug.Log("crossingSceneGo!");
        TutorialManager.tutorialStage = 12;
        trigger1.StartDialogue();

    }

    public void whereToCross()
    {
        Debug.Log("turnonpointer");
        trigger1.enabled = false; //TURN THINGS OFF YIPEEE
        TutorialManager.tutorialStage = 13;
        pointer1.gameObject.SetActive(true);
        pointer1.GetChild(0).gameObject.SetActive(true);
        pointer1.GetChild(1).gameObject.SetActive(true);
    }

    public void crossFound() //called by allow merge after both plants placed. default condition = 13
    {
        Debug.Log("boom");
        TutorialManager.tutorialStage = 20; //signalizes DONE with the thang
        pointer1.gameObject.SetActive(false);
        // trigger2.StartDialogue();
        //Transform pointer = transform.Find("pointer3");
        //pointer.gameObject.SetActive(false);
        //GameObject.Find("pointer3").SetActive(false);
    }
}
