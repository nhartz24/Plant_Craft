using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReloadScene : MonoBehaviour
{


    private GameObject master;

    private bool tutorial;

    private bool desert;

    private bool marsh;



    //  public void DestroyPlants(){
    //     foreach(GameObject plant in GameObject.FindGameObjectsWithTag("Plant")){
    //             Destroy(plant);
    //         }

    // }

    public void Start(){
        master = GameObject.Find("Master");
    }

    public void NewReload(){

        string sceneName = SceneManager.GetActiveScene().name;

        // DestroyPlants();

        Time.timeScale = 1;

        Debug.Log(SceneSwitch.gameLevel);

        if(sceneName == "TutorialLevel"){
            if (SceneSwitch.gameLevel == 0) //basically, if we're playing the level again, we'll just go to the garden if we want to quit so it doesn't cause issues
            {
                master.GetComponent<SceneSwitch>().LoseGame();
            }
            else if (SceneSwitch.gameLevel == 1){
                master.GetComponent<SceneSwitch>().ReturnFromTutorial(); 

            }
            else
            {
                master.GetComponent<SceneSwitch>().TutorialSwitch(); 
            }
        }
        else if (sceneName == "ForestLevel"){

            master.GetComponent<SceneSwitch>().ForestSwitch();
            
        }

        else if (sceneName == "DesertLevel"){

            master.GetComponent<SceneSwitch>().DesertSwitch();
        }
        else if (sceneName == "DesertLevel2"){

            master.GetComponent<SceneSwitch>().Desert2Switch();
        }
        else if (sceneName == "MarshLevel"){

            master.GetComponent<SceneSwitch>().MarshSwitch();
            
        }
        else if (sceneName == "MarshLevel2"){

            master.GetComponent<SceneSwitch>().Marsh2Switch();
            
        }
        
        
        master.GetComponent<SceneSwitch>().LoseLoad(sceneName);       

    }

    // public void NewLoad(){

    //    string sceneName = SceneManager.GetActiveScene().name;

    //     // DestroyPlants();

    //     Time.timeScale = 1;

    //     if(sceneName == "TutorialLevel"){
            
    //         master.GetComponent<SceneSwitch>().winLevelTransition();
    //         master.GetComponent<SceneSwitch>().LoseLoad(sceneName);
    //         master.GetComponent<SceneSwitch>().LoseLoad("DesertLevel"); 

    //     }

    //     else if (sceneName == "DesertLevel"){

    //         master.GetComponent<SceneSwitch>().AfterDesert();
    //         master.GetComponent<SceneSwitch>().LoseLoad(sceneName);
    //         master.GetComponent<SceneSwitch>().LoseLoad("MarshLevel");


    //     }
    //     else if (sceneName == "MarshLevel"){

    //         master.GetComponent<SceneSwitch>().AfterMarsh();
    //         master.GetComponent<SceneSwitch>().LoseLoad(sceneName);
    //         // master.GetComponent<SceneSwitch>().LoseLoad("DesertLevel");
            
    //     }
    // }


    // public void MarshReload()
    // {

    //     master = GameObject.Find("Master");


    //     master.GetComponent<SceneSwitch>().winLevelTransition();


    //     master.GetComponent<SceneSwitch>().MarshLoad();

    // }
}
