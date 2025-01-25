using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelectManager2 : MonoBehaviour
{
    public Image[] images; //MUST HAVE THE SAME LIST ORDER
    public Button[] buttons;

    //[SerializeField]
    //public int scenesunlocked = 1;

    // Start is called before the first frame update
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        foreach (Image image in images)
        {
            image.enabled = true; //images correspond with button off
        }
        foreach (Button button in buttons)
        {
            button.enabled = false; //buttons off
        }

        int scenesunlocked = SceneSwitch.gameLevel;



        while (scenesunlocked >= 0)
        {

            if (scenesunlocked > 5){
                scenesunlocked = 5;
            }

            if(scenesunlocked >= 3){
                images[scenesunlocked-3].enabled = false;
                buttons[scenesunlocked-3].enabled = true;

            }
            scenesunlocked--;

            
        }
    }
    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("hello");
    }
}
