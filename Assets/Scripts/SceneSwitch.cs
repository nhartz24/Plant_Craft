using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitch : MonoBehaviour
{

    [Header("Levels to Load")]
    
    private string levelToLoad;
    private GameObject master;

    private GameObject canvas;

    private Scene scene;

    private Dictionary<string, GameObject> dict;

    private Dictionary<string, GameObject> dictCanvas;

    private bool loaded = false;

    private string destination;

    private string departing;

    public static int gameLevel = 0;

    public GameObject pauseScreen;

    private bool cross;

    private bool tutorial;

    private bool desert;

    private bool marsh;



    [SerializeField] private SoundManager soundManager; //references the sound manager so we can play multiple sounds easy
    //[SerializeField] private GameObject no

    private void Start()
    {
        soundManager = GameObject.Find("SoundManagerMain").GetComponent<SoundManager>();

        dict = FindObjectOfType<MasterDict>().masters;
        dictCanvas = FindObjectOfType<MasterDict>().canvases;


        // soundManager.Play("main_theme");
    }

    // public void LoadGameStarted()
    // {
    //     if (PlayerPrefs.HasKey("SavedLevel"))
    //     {
    //         soundManager.Play("new_game");
    //         levelToLoad = PlayerPrefs.GetString("SavedLevel");
    //         // SceneManager.LoadScene(levelToLoad);
    //     }
    //     else
    //     {
    //         Debug.Log("no saved level");
    //     }
    // }
    // public void Options()
    // {
    //     Debug.Log("options pressed");
    //     soundManager.Play("button_press");
    // }
    // public void QuitGame()
    // {
    //     Application.Quit();
    // }


    public void DestroyPlants(){
        foreach(GameObject plant in GameObject.FindGameObjectsWithTag("Plant")){
                Destroy(plant);
            }

    }

    public void ButtonPress(string departing, string destination){

        soundManager.Play("switch_scene");              

            // soundManager.Play("new_game");

            GameStart(departing, destination);

    }

     public void NewGame()
    {

        Debug.Log("HELPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPP");

        soundManager.Play("new_game");
        StartCoroutine(NewestGame());

       
    }

    public void LoseLoad(string sceneName){

        string masterName = ("Master" + sceneName).ToString();
        string canvasName = ("Canvas" + sceneName).ToString();
        

        if(dict.ContainsKey(masterName)){
            SceneManager.UnloadSceneAsync(sceneName); 
            dict.Remove(masterName);
            dictCanvas.Remove(canvasName);

        }
        // SceneManager.GetSceneByBuildIndex(1).name;

        StartCoroutine(Load(sceneName, masterName, canvasName));

    }

    // public void DesertLoad(){
        
    //     string sceneName = "DesertLevel";

    //     string masterName = ("Master" + sceneName).ToString();

    //     string canvasName = ("Canvas" + sceneName).ToString();

    //     StartCoroutine(Load(sceneName, masterName, canvasName));

    // }

    // public void MarshLoad()
    // {
    //     string sceneName = "MarshLevel";

    //     string masterName = ("Master" + sceneName).ToString();

    //     string canvasName = ("Canvas" + sceneName).ToString();

    //     StartCoroutine(Load(sceneName, masterName, canvasName));

    // }
    public IEnumerator Load(string sceneName, string masterName, string canvasName){

            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
            
            // Wait until the asynchronous scene fully loads
            while (!asyncLoad.isDone)
            {
                yield return null;
            }
            
            master = GameObject.Find(masterName);

            canvas = GameObject.Find(canvasName);
           
            dict.Add(masterName, master);
            dictCanvas.Add(canvasName, canvas);

            

            master.SetActive(false);
            canvas.SetActive(false);

    }

    IEnumerator NewestGame(){
        Debug.Log("AHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHH");

        yield return new WaitForSeconds(0.1f);
        ButtonPress("MainMenu", "TutorialLevel"); 


    }

    public void LoadGame()

    {
        //TESTING THIS IS FOR TESTING PORPOISES
        gameLevel = 5;
        ButtonPress("MainMenu", "Garden");
    }

     public void LoseGame()

    {
        //TESTING THIS IS FOR TESTING PORPOISES
        ButtonPress("TutorialLevel", "MainMenu");
    }
    public void ReturnFromTutorial()
    {
        ButtonPress("TutorialLevel", "Crossing");
    }    


    public void CrossPlant(){
        ButtonPress("Garden", "Crossing");
        cross = true;
    }

    public void BattleInsects(){
        ButtonPress("Garden", "LevelSelect");
    }
    public void ReturnToGarden()
    {
        ButtonPress("LevelSelect", "Garden");
    }
    public void ReturnToGarden2()
    {
        ButtonPress("LevelSelect2", "Garden");
    }
    public void startTutorialLevel()
    {
        ButtonPress("LevelSelect", "TutorialLevel");
    }
    public void startForestLevel()
    {
        ButtonPress("LevelSelect", "ForestLevel");
    }
    public void startDesertLevel()
    {
        ButtonPress("LevelSelect", "DesertLevel");
    }
    public void startDesertLevel2()
    {
        ButtonPress("LevelSelect2", "DesertLevel2");
    }
    public void startMarshLevel()
    {
        ButtonPress("LevelSelect2", "MarshLevel");
    }
    public void startMarshLevel2()
    {
        ButtonPress("LevelSelect2", "MarshLevel2");
    }
    // public void startFinalLevel()
    // {
    //     ButtonPress("LevelSelect", "ForestLevel");
    // }



    public void ToGarden()
    {
        ButtonPress("Crossing", "Garden");
    }
     public void MainMenu()
    {
        // DestroyPlants();
        
        Time.timeScale = 1;
        // GameObject newPauseScreen = GameObject.Find("MasterTutorialLevel/TutorialManager/PauseScreen");
        pauseScreen.SetActive(false);
        GameObject newMaster =  GameObject.Find("Master");
        newMaster.GetComponent<SceneSwitch>().GoToMainMenu();

        
    }


    public void ChangeSelect(){
        ButtonPress("LevelSelect", "LevelSelect2");
    }

    public void ChangeSelectBack(){
        ButtonPress("LevelSelect2", "LevelSelect");
    }


    public void GoToMainMenu(){
        ButtonPress("TutorialLevel", "Garden");
    }

    public void TutorialSwitch(){
  
        ButtonPress("TutorialLevel", "Garden");
    }

    public void ForestSwitch(){

        ButtonPress("ForestLevel", "Garden");
    }

    public void DesertSwitch(){
  
        ButtonPress("DesertLevel", "Garden");
    }
    public void Desert2Switch(){
  
        ButtonPress("DesertLevel2", "Garden");
    }

    public void MarshSwitch(){

        ButtonPress("MarshLevel", "Garden");
    }

    public void Marsh2Switch(){

        ButtonPress("MarshLevel2", "Garden");
    }

    public void backtoMainMenu()
    {
        ButtonPress("Garden", "MainMenu");
    }

    public void winLevelTransition(){ //need to change because this is win condition, ONLY USE WIN WON NEW LEVEL

        if (gameLevel < 1) //max is three
        {
            gameLevel++; //sign to go to the next stage ! !  ! !  !
        }

        DestroyPlants();

        if (TutorialManager.tutorialStage == 10){ //special conditions for tutorial after first level CHANGE TO 10 IF ADD DIALGOU
            
            TutorialManager.tutorialStage = 10;
            ButtonPress("TutorialLevel", "Crossing");
            //FindObjectOfType<TutorialManager>().wonFirstLevel();
            
        }
        else
        {

            ButtonPress("TutorialLevel", "Garden");
            
        }
    }


    public void AfterFinal()
    {
        ButtonPress("ForestLevel", "Garden");
    }


    private void GameStart(string departing, string destination)
    {
        
        master = dict[("Master" + destination).ToString()];
        master.SetActive(true);
        
        canvas = dictCanvas[("Canvas" + destination).ToString()];
        canvas.SetActive(true);

        SceneManager.SetActiveScene(SceneManager.GetSceneByName(destination));

            
        canvas = dictCanvas[("Canvas" + departing).ToString()];
        canvas.SetActive(false);


        master = dict[("Master" + departing).ToString()];
        master.SetActive(false);
    }
    

    public void buttonQuit()
    {
        Application.Quit();
    }

}
