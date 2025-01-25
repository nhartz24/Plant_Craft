using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{

    private bool uno;

    private bool dos;

    private bool cuatro;

    public GameObject pauseScreen;

    public GameObject button;


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
        button.SetActive(true);
        gameObject.SetActive(false);
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
        
        gameObject.SetActive(true);
        button.SetActive(false);
        
        
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
}
