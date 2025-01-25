using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System;

// using JetBrains.Annotations;
// using UnityEngine.EventSystems; //import to allow handlers

public class DialogueManager : MonoBehaviour
{

    public Image actorImage;
    public TextMeshProUGUI actorName;
    public TextMeshProUGUI messageText;
    public RectTransform backgroundBox;
    public float textSpeed;
    // public string[] lines;
    // private int index;



    Message[] currentMessages;
    Actor[] currentActors;
    public int activeMessage = 0;
    private bool isActive = false;

     private bool uno;

    private bool dos;

    private bool cuatro;

    public void StartDialogue(Message[] messages, Actor[] actors){
        messageText.text = string.Empty;
        currentMessages = messages;
        currentActors = actors;
        activeMessage = 0;
        


        Debug.Log("Started conversation! " + messages.Length);
        messageText.text = string.Empty; //why is it here twice?
        // if(isActive != false){
            StartCoroutine(ShowDialogue());
        // }
        isActive = true;


    } 

    public void Active(Message[] messages, Actor[] actors) {
        if(isActive != true){
             if (Time.timeScale == 1.0f){
            uno = true;
        }
        else if (Time.timeScale == 2){
            dos = true;
        }
        else if (Time.timeScale == 4){
            cuatro = true;
        }

       StartDialogue(messages, actors);
        Time.timeScale= 0.00000001f;
        }
        
    }

    public void Unpause(){
        if (uno == true){
            Time.timeScale = 1;
        }
        else if (dos == true){
            Time.timeScale = 2;        
        }
        else if (cuatro == true){
            Time.timeScale = 4;
        }
        
        uno = false;
        dos = false;
        cuatro = false;
    }

    IEnumerator ShowDialogue() {
        Message messageToDisplay = currentMessages[activeMessage];

        Actor actorToDisplay = currentActors[messageToDisplay.actorId];
        actorName.text = actorToDisplay.name;
        actorImage.sprite = actorToDisplay.sprite;
        
        backgroundBox.LeanScale(Vector3.one, 0.00000001f).setEaseInOutExpo();
        yield return new WaitForSeconds(0.00000001f);
        StartCoroutine(TypeLine());

    }

    IEnumerator TypeLine() {

        Message messageToDisplay = currentMessages[activeMessage];

        Actor actorToDisplay = currentActors[messageToDisplay.actorId];
        actorName.text = actorToDisplay.name;
        actorImage.sprite = actorToDisplay.sprite;

        if (options.voiceOver == true) //the options script contains a bool deciding whether to show the voice over. in the future, this will be in a seperate script of global options variables
        {
            try
            {
                FindObjectOfType<VoiceOverManager>().Play(messageToDisplay.soundeffect); //gets the soundeffect from next message and plays it. yes its taxing, no i dont give an f
            }
            catch (NullReferenceException ex)
            {
                Debug.Log("ERROR: " + ex);
                Debug.Log("Voice Over Manager not found. Find it!");
            }
        }

        foreach (char c in messageToDisplay.message.ToCharArray()){
            messageText.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    public void NextMessage() {

        FindObjectOfType<VoiceOverManager>().StopAll(); //stops playing all dialogue

        if(activeMessage < currentMessages.Length - 1){
            activeMessage++;
            messageText.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else{
            //when dialogue ends
            backgroundBox.LeanScale(Vector3.zero, 1f).setEaseInOutExpo();
            Debug.Log("Conversation ended");
            //special tutorial level stuff
            
            if (TutorialManager.tutorialStage == 1)
            {
                FindObjectOfType<TutorialManager>().placingTutorial(); //moves back to start tutorial script
                Unpause();
            }
            else if (TutorialManager.tutorialStage == 8)
            {
                FindObjectOfType<TutorialManager>().giveDesertPlant();
                Unpause();
            }

            else if (TutorialManager.tutorialStage == 7)
            {
                FindObjectOfType<TutorialManager>().endLilyTutorial();
                Unpause();
            }
            else if (TutorialManager.tutorialStage == 6)
            {
                FindObjectOfType<TutorialManager>().endOnionTutorial();
                Unpause();
            } 
            else if (TutorialManager.tutorialStage == 9)
            {
                FindObjectOfType<TutorialManager>().endPeaTutorial();
                Unpause();
            }

            else if (TutorialManager.tutorialStage == 12)
            {
                if (SceneManager.GetActiveScene().name == "Crossing")
                {
                FindObjectOfType<CrossTutorialManager>().whereToCross();
                Unpause();
                }
                
            }
            else
            {
                Unpause();
                isActive = false;
            }

            isActive = false;
            
        }

    }


    // Start is called before the first frame update
    void Start()
    {
        backgroundBox.transform.localScale = Vector3.zero;
    }

    // Update is called once per frame
    public void Update() 
    {
        if (Input.GetMouseButtonDown(0) && isActive == true)
        {
            Debug.Log(isActive + "AHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHH");
            if (messageText.text == currentMessages[activeMessage].message) {
                NextMessage();
            }
            else {
                StopAllCoroutines();
                Message messageToDisplay = currentMessages[activeMessage];
                messageText.text = messageToDisplay.message;
            }
        }
    }

}

