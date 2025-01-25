using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MasterDict : MonoBehaviour
{
    public Dictionary<string, GameObject> masters = new Dictionary<string, GameObject>();

    public Dictionary<string, GameObject> canvases = new Dictionary<string, GameObject>();

    private GameObject master;
    private GameObject canvas;

    public GameObject loading;


    private void Start(){

        master = GameObject.Find(("MasterMainMenu"));

        canvas = GameObject.Find("CanvasMainMenu");

        masters.Add("MasterMainMenu", master);

        canvases.Add("CanvasMainMenu", canvas);

        StartCoroutine(StartLoad());

    }

    IEnumerator StartLoad(){



        for(int i = 1; i < SceneManager.sceneCountInBuildSettings ; i++){
            
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(i, LoadSceneMode.Additive);
            
            // Wait until the asynchronous scene fully loads
            while (!asyncLoad.isDone)
            {
                yield return null;
            }
            string sceneName = SceneManager.GetSceneByBuildIndex(i).name;

            string masterName = ("Master" + sceneName).ToString();

            string canvasName = ("Canvas" + sceneName).ToString();


            master = GameObject.Find(masterName);

            canvas = GameObject.Find(canvasName);

            

            masters.Add(masterName, master);
            canvases.Add(canvasName, canvas);

            master.SetActive(false);
            canvas.SetActive(false);


        }

        loading.SetActive(false);


    }


}
