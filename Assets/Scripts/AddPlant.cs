using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class AddPlant : MonoBehaviour
{

    public static bool loaded;

    public Plant cactus;

    public Plant venus;

    public Plant pitcher;

    private PlantDict plantDict;



     void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;

        if(loaded == true){
            StartCoroutine(AddPlants());

        }

    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("joined scene");
    }

    IEnumerator AddPlants(){
        
        plantDict = GameObject.Find("PlantDictObj").GetComponent<PlantDict>();


        plantDict.SO_Plants.Add("cactus", cactus);
        
        yield return new WaitForSeconds(0.2f);
        
        
        plantDict.SO_Plants.Add("venus_fly_trap", venus);

                yield return new WaitForSeconds(0.2f);


            plantDict.SO_Plants.Add("pitcher", pitcher);

            loaded = false;
    }

    public void ChangeLoaded(){
        plantDict = GameObject.Find("PlantDictObj").GetComponent<PlantDict>();


        plantDict.Plants.Add(cactus);
        
        
        plantDict.Plants.Add(venus);

        plantDict.Plants.Add(pitcher);
        loaded = true;

    }
}
