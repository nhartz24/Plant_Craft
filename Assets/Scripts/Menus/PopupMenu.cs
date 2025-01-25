using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PopupMenu : MonoBehaviour
{
    [Header("Level Pointers")]
    [SerializeField] private string levelPointer;

    [SerializeField] private SoundManager soundManager; //references the sound manager so we can play multiple sounds easy

    private void Start()
    {
        soundManager.Play("screen_theme");
    }
    public void LoadGarden()
    {
        soundManager.Play("menu_press");
        StartCoroutine(GardenStart());
    }

    IEnumerator GardenStart()
    {
        Debug.Log("garden started at: " + Time.deltaTime);
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(levelPointer);
    }
    public void Options()
    {
        Debug.Log("options pressed");
        soundManager.Play("button_press");
    }
}