using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeedControl : MonoBehaviour
{
    public Button one;
    public Button two;
    public Button four;

     private bool uno;

    private bool dos;

    private bool cuatro;

    private Button selected;

    public GameObject pauseButt;
    public GameObject unpauseButt;

    public GameObject pauseScreen;

    public void oneSpeed() {
        uno = true;
        Time.timeScale = 1;
        selected = one;

        dos = false;
        cuatro = false;
        pauseButt.SetActive(true);
        unpauseButt.SetActive(false);
    }

    public void twoSpeed() {
        dos = true;
        Time.timeScale = 2;
        selected = two;

        uno = false;
        cuatro = false;
        pauseButt.SetActive(true);
        unpauseButt.SetActive(false);
    }

    public void fourSpeed() {
        cuatro = true;
        Time.timeScale = 4;
        selected = four;

        uno = false;
        dos = false;
        pauseButt.SetActive(true);
        unpauseButt.SetActive(false);
    }

    public void PauseLvl(){
        if (Time.timeScale == 1.0f){
            uno = true;
        }
        else if (Time.timeScale == 2){
            dos = true;
        }
        else if (Time.timeScale == 4){
            cuatro = true;
        }

        Time.timeScale = 0;
        unpauseButt.SetActive(true);
        pauseButt.SetActive(false);
    }

    public void UnPauseLvl(){

        if (uno == true){
            Time.timeScale = 1;
        }
        else if (dos == true){
            Time.timeScale = 2;        
        }
        else if (cuatro == true){
            Time.timeScale = 4;
        }
        
        uno = false;
        dos = false;
        cuatro = false;
        
        pauseButt.SetActive(true);
        unpauseButt.SetActive(false);
        
        
    }


    public void PauseGame(){

        if (Time.timeScale == 1.0f){
            uno = true;
        }
        else if (Time.timeScale == 2){
            dos = true;
        }
        else if (Time.timeScale == 4){
            cuatro = true;
        }

        Time.timeScale = 0;
        pauseScreen.SetActive(true);
    }

    public void UnPause(){

    Debug.Log(uno);


        if (uno == true){
            Time.timeScale = 1;
        }
        else if (dos == true){
            Time.timeScale = 2;        
        }
        else if (cuatro == true){
            Time.timeScale = 4;
        }
        
        uno = false;
        dos = false;
        cuatro = false;
        pauseScreen.SetActive(false);
        
    }

    void Start()
    {
        selected = one;
    }

    void Update()
    {
        selected.Select();
    }

}
