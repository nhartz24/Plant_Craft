using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [Header("Levels to Load")]
    public string _newGameLevel;
    private string levelToLoad;

    [SerializeField] private SoundManager soundManager; //references the sound manager so we can play multiple sounds easy
    //[SerializeField] private GameObject no

    private void Start()
    {
        //soundManager.Play("main_theme");
    }
    public void NewGameStarted()
    {
        soundManager.Play("new_game");
        StartCoroutine(GameStart());
        //SceneManager.LoadScene(_newGameLevel);
    }    

    IEnumerator GameStart()
    {
        Debug.Log("game started at: " + Time.deltaTime);
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(_newGameLevel);
    }
    public void LoadGameStarted()
    {
        if (PlayerPrefs.HasKey("SavedLevel"))
        {
            soundManager.Play("new_game");
            levelToLoad = PlayerPrefs.GetString("SavedLevel");
            SceneManager.LoadScene(levelToLoad);
        }
        else
        {
            Debug.Log("no saved level");
        }
    }
    public void Options()
    {
        Debug.Log("options pressed");
        soundManager.Play("button_press");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}