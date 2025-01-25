using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


[RequireComponent(typeof(Canvas))]
[RequireComponent(typeof(SupplyManager))]

public class EndLevel : MonoBehaviour
{
    public RectTransform loseScreen;
    public RectTransform winScreen;
    public RectTransform returnScreen;
    public RectTransform newPlantScreen;

    public Plant newPlantAwarded; // needs to be assigned in editor for each level

    public GameObject newPlantButton;

    private GameObject winObj;

    public bool isEnded;
    private List<string> sceneNames;
    
    void Start()
    {
        sceneNames = GameObject.Find("Master").GetComponentInChildren<PlantDict>().sceneNames;

        isEnded = false;
        if(loseScreen == null || winScreen == null) {
            Debug.Log("LoseScreen and/or WinText have not been assigned for ending "
            +"this level in the Canvas. Please drag their prefabs into the EndLevel component.");
        }
        if(newPlantAwarded == null) {
            Debug.Log("ERROR: The EndLevel component on the main canvas of this level is missing the plant to "
                +"award at the end of the level.");
        }
    }


    public void win() {
        Debug.Log("winning!!");

        // scales the winScreen to the Canvas height + width
        winScreen.anchorMin = new Vector2(0,0);
        winScreen.anchorMax = new Vector2(1,1);

        

        /* string sceneName = SceneManager.GetActiveScene().name;
        if(sceneName == "TutorialLevel") {

        } 
        else if (sceneName == "DesertLevel") {

        } 
        else if (sceneName == "MarshLevel") {

        } */



        DestroyPopups();

            winObj = Instantiate(winScreen.gameObject, transform);
        

        Time.timeScale = 1; 
        GetComponent<SupplyManager>().disableAllBuying();
        isEnded = true;

        // start dialogue for end
        if (FindObjectOfType<TutorialManager>() != null)
        {
            FindObjectOfType<TutorialManager>().wonFirstLevel();
                        StartCoroutine(WaitAndGetPlant());


        }
        else {
            StartCoroutine(WaitAndGetPlant());
        }

    }
    
    //change this so that dialogue triggers this as a function
    public void GetPlant() {
        //yield return new WaitForSeconds(3);
        winObj.transform.Find("WinText").gameObject.SetActive(false);
        
        returnScreen.anchorMin = new Vector2(0,0);
        returnScreen.anchorMax = new Vector2(1,1);
        Instantiate(returnScreen.gameObject, transform);

        if(newPlantScreen != null) {
            newPlantScreen.anchorMin = new Vector2(0,0);
            newPlantScreen.anchorMax = new Vector2(1,1);
            newPlantScreen.gameObject.GetComponent<AddedPlantUIController>().plant = newPlantAwarded;
            Instantiate(newPlantScreen.gameObject, transform);
            Instantiate(newPlantButton, transform);
        }
    }

    IEnumerator WaitAndGetPlant() {
        yield return new WaitForSeconds(1);
        
        if (SceneManager.GetActiveScene().name != "MarshLevel2"){
            winObj.transform.Find("WinText").gameObject.SetActive(false);
        }


             string sceneName = SceneManager.GetActiveScene().name;
        


        returnScreen.anchorMin = new Vector2(0,0);
        returnScreen.anchorMax = new Vector2(1,1);
        Instantiate(returnScreen.gameObject, transform);

        if(newPlantScreen != null && !sceneNames.Contains(sceneName)) {
            newPlantScreen.anchorMin = new Vector2(0,0);
            newPlantScreen.anchorMax = new Vector2(1,1);
            newPlantScreen.gameObject.GetComponent<AddedPlantUIController>().plant = newPlantAwarded;
            Instantiate(newPlantScreen.gameObject, transform);
            Instantiate(newPlantButton, transform);
        }

        if (!sceneNames.Contains(sceneName)){
            SceneSwitch.gameLevel++;
            sceneNames.Add(sceneName);
        }
    }


    
    
    public void DestroyPopups(){
        foreach(GameObject popup in GameObject.FindGameObjectsWithTag("Popup")){
                Destroy(popup);
            }

    }

    public void lose() {
        Debug.Log("losing!");

        // scales the loseScreen to the Canvas height + width
        loseScreen.anchorMin = new Vector2(0,0);
        loseScreen.anchorMax = new Vector2(1,1);

        DestroyPopups();

        Instantiate(loseScreen.gameObject, transform);

        DestroyPopups();
        Time.timeScale = 0; 
        //GetComponent<SupplyManager>().disableAllBuying();
        Button[] buttons = GetComponentsInChildren<Button>();
        foreach (Button button in buttons)
        {
            if(button.gameObject.name != "RestartButton" && button.gameObject.name != "PauseButton"){
                button.enabled = false;
            }
        }

        // find all nutrientProduction components and stop their coroutines

        isEnded = true;
        
        // add case for if holding a not-placed plant
    }
}
