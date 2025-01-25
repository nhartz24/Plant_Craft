using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    private Slider slider;

    public WaveSpawner waveSpawner;

    private float size;
    private float posY;
    private float posX;

    private float amount;

    private float total;

    public GameObject progressMark;

    private List<float> amounts = new List<float>();

    public GameObject fill;

    private int i = 0;

    void Start()
    {
        slider = gameObject.GetComponent<Slider>();

        size = gameObject.GetComponent<RectTransform>().rect.width;


        for(int i = 0; i < waveSpawner.waves.Length; i++){

            amount += waveSpawner.waves[i].amount0 *  waveSpawner.waves[i].rate0;
            amount += waveSpawner.waves[i].amount1 * waveSpawner.waves[i].rate1;
            amount += waveSpawner.waves[i].amount2 * waveSpawner.waves[i].rate2;

            total += amount;
            
            amounts.Add(amount);

            amount = 0;

            
        }

        for (int j = 0; j < amounts.Count - 1; j++){


            float newAmount = amounts[j] / total;



            float x = (-size/2 + (newAmount * size) + amount);


            GameObject mark = Instantiate(progressMark);
            mark.transform.SetParent(gameObject.transform);

            mark.GetComponent<UnityEngine.UI.Image>().rectTransform.localScale = new Vector3(1,1,1);

            mark.GetComponent<RectTransform>().localPosition = new Vector3(x, posY, 0);

            // , new Vector2(posX, posY), Quaternion.identity, gameObject.transform);                    
            
            amount += newAmount * size;
        }

        slider.value = 0;

        slider.maxValue = size;
    }

    public void WaveStart(){

        Debug.Log("AHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHH the wave is starting");
        gameObject.SetActive(true);

        StartCoroutine(Show());

    }


    IEnumerator Show(){

        yield return new WaitForSeconds(5f);

        InvokeRepeating("ShowDatBar", 0f, 0.0625f);
    }

    void ShowDatBar(){

        if (i < total*16){
            i += 1;
            slider.value = i * (size/total)/16 ;
        }

        

    }

    // public void setMaxHealth(int health) {
    //     slider.maxValue = health;
        
    // }

    // public void setHealth(int health) {
    //     slider.value = health;
    // }
}
