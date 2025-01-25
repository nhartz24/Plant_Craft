using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GardenManager : MonoBehaviour
{
    //THIS SCRIPT CONTROLS THE GARDEN SCENE SCENE MANAGER
    //It controls buttons and other functions of this scene
    //Written by Jude (probably copied)

    public Transform[] popups;
    bool popupdisplayed = false;
    public Image[] pots; //MUST HAVE THE SAME LIST ORDER
    public Button[] buttons;
    public Transform specialpot;

    //[SerializeField]
    //public int scenesunlocked = 1;

    // Start is called before the first frame update
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        foreach (Image pot in pots)
        {
            pot.enabled = false; //all images turned off
        }
        foreach (Button button in buttons)
        {
            button.enabled = false; //all buttons turned off
        }

        //int gardenplants = 2;      
        int gardenplants = SceneSwitch.gameLevel + 2; //tutorial level is 2, forest 3, desert 4, desert2 5, marsh 6, marsh2 7
/*        if (SceneSwitch.gameLevel < 2)
        {
            gardenplants = 2;
        }
        else if (SceneSwitch.gameLevel == 2 || SceneSwitch.gameLevel == 3)
        {
            gardenplants = 3;
        }
        else if (SceneSwitch.gameLevel >= 4)
        {
            gardenplants = SceneSwitch.gameLevel;
        }*/
        if (gardenplants >= 6)
        {
            specialpot.gameObject.SetActive(true);
        }

        if (gardenplants < 8){ //normally 5
        while (gardenplants >= 0)
        {
            
                pots[gardenplants].enabled = true;
                buttons[gardenplants].enabled = true;
                gardenplants--;
            }

        }
        else{

            for(int i = 0; i <= 7; i++){
                pots[i].enabled = true;
                buttons[i].enabled = true;
            }

                


            }
    }
    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("joined scene");
    }

    public void GiveInformation(int p)
    {
        if (popupdisplayed == false)
        {
            popups[p].gameObject.SetActive(true);
            popupdisplayed = true;
        }
    }
/*    public void LilyInformation()
    {
        if (popupdisplayed == false) {
            popups[1].gameObject.SetActive(true);
            popupdisplayed = true;
        }
        
    }*/
    public void EndPopup()
    {
        foreach (Transform p in popups)
        {
            p.gameObject.SetActive(false);
        }
        popupdisplayed = false;
    }
}
