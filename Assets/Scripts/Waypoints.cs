using UnityEngine;
using UnityEngine.SceneManagement;

public class Waypoints : MonoBehaviour {

	public static Transform[] pointsTutorial;

    public static Transform[] pointsForest;

    public static Transform[] pointsDesert;

    public static Transform[] pointsDesert2;

    public static Transform[] pointsMarsh;

    public static Transform[] pointsMarsh20;
    public static Transform[] pointsMarsh21;

    private bool isLoaded = false;

    public string sceneName;

    // void OnEnable()
    // {
    //     SceneManager.sceneLoaded += OnSceneLoaded;
    // }

    void OnDisable()
    {
        // SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnEnable()
    {
        // here you can use scene.buildIndex or scene.name to check which scene was loaded
        // if (scene.name == "TutorialLevel")
        // {

            //Debug.Log( SceneManager.GetActiveScene().name + "AHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHH");
            
            // Destroy the gameobject this script is attached to
            //Destroy(gameObject);
        // }
        // else if (scene.name == "DesertLevel"){
        //     points = new Transform[transform.childCount];
        //     for (int i = 0; i < points.Length; i++)
        //     {
        //         points[i] = transform.GetChild(i);
        //     }

        // }
    }

    public void Update(){

        // while(sceneName == "MainMenu"){
        //     Debug.Log("AHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHH");
        //     sceneName = SceneManager.GetActiveScene().name;
        // }

        if (isLoaded == false){
            sceneName = SceneManager.GetActiveScene().name;

            if(sceneName == "TutorialLevel"){
                Debug.Log("TutorialLevel + AHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHH");
                pointsTutorial = getPoints();
            }

            else if(sceneName == "ForestLevel"){
                Debug.Log("Getting waypoints for ForestLevel");
                pointsForest = getPoints();
            }

            else if (sceneName == "DesertLevel"){
                pointsDesert = getPoints();
            }
            else if (sceneName == "DesertLevel2"){
                pointsDesert2 = getPoints();
            }

            else if (sceneName == "MarshLevel"){
                pointsMarsh = getPoints();
            }

            else if (sceneName == "MarshLevel2"){
                pointsMarsh = getPoints();
            }


            else {
				Debug.Log("ERROR: Scene name does not match any of the scenes listed in Waypoints to record their waypoints.");
            }
        }

    }

    private Transform[] getPoints() {
        Transform[] points = new Transform[transform.childCount];
        for (int i = 0; i < points.Length; i++)
        {
            points[i] = transform.GetChild(i);
        }
        isLoaded = true;
        return points;
    }

}