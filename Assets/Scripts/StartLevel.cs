using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Button))] 
// not necessary, just a reminder that this script is for the start button

public class StartLevel : MonoBehaviour
{
    private Transform canvas;
    private Transform[] children;
    
    void Start()
    {
        canvas = transform.parent;
        Debug.Log("canvas: "+canvas.name);

        // these things should be true in the scene before start is pressed:
        // wavespawner component on the GameManager is disabled
        // NutrientLabel and SpeedButtons are disabled
        // SupplyManager component on Canvas is disabled
    }

    public void startLevel() {
        try {
            FindObjectOfType<SoundManager>().Play("level_start"); //play sound for starting level
        } catch (NullReferenceException) {
            Debug.Log("ERROR: Could not find SoundManager.");
        }

        Time.timeScale = 1;

        //if (SceneManager.GetActiveScene().name != "TutorialLevel") // added for testing in extra levels - tell me if this breaks!
        if (SceneSwitch.gameLevel != 0) //this stuff is handled in specifics by the tutorial level, in other levels doesn't matter
        {
            Debug.Log("This scene is not the tutorial level - if it is, Luca's code messed it up and you should uncomment the line above this. Thanks.");
            canvas.Find("NutrientLabel").gameObject.SetActive(true); // enable specifically the nutrient label, so it can be used in supply manager
            canvas.GetComponent<SupplyManager>().enabled = true;

            children = canvas.GetComponentsInChildren<Transform>(true);
            foreach (Transform obj in children)
            {
                if (obj.name != "StartTutorial" && obj.name != "ProgressBar" && obj.name != "UnPause" && obj.name != "UnPauseText")
                {
                    obj.gameObject.SetActive(true);
                }  
            }
        }

        GameObject.Find("magic_flower").GetComponent<Health>().enabled = true;

        try
        {
            GameObject.Find("Warning_Canvas").transform.GetChild(0).gameObject.SetActive(true); //set the warning dialogue to be true
        }
        catch (NullReferenceException ex){
            Debug.Log("Error: " + ex);
            Debug.Log("Check to see that the warning dialogue is under warning canvas");
        }

        Debug.Log("Got to game manager");

        try {
            GameObject.Find("GameManager").GetComponent<WaveSpawner>().enabled = true;
        }
        catch (NullReferenceException ex) {
            Debug.Log("Error: "+ex);
            Debug.Log("Check that the game manager contains the wave spawner, and is called"+
            " GameManager, since this code is looking it up by name to enable on start button press.");
        }

        //addendum by Jude
        // FindObjectOfType<SoundManager>().Play("button_press");


        Destroy(gameObject);
    }

}
