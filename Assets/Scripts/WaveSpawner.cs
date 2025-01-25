using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using TMPro;

public class WaveSpawner : MonoBehaviour
{

    public Transform[] spawnPoint;        //place that enemies appear from

    public Transform enemies;       // holds all the enemy objects

    [SerializeField]
    private GameObject wavealert;

    [System.Serializable]
    public class Wave {             //creating a new list type that holds all these values 
        public string name;
        public Transform enemy0;       //enemy prefab
        public Transform enemy1;
        public Transform enemy2;

        public int amount0;           // how many of each type of enemy per wave
        public int amount1;
        public int amount2;

        public float rate0;         //how often do the enemies drop wach wave 
        public float rate1;
        public float rate2;


    }

    public Wave[] waves;            //create a new wave list
    private int nextWave = 0;       //the number of the next wave 

    public float timeBetweenWaves = 5f;     // how long do we wait until the next wave comes 
    private float countdown;

    private string sceneName;

    void Start() {
        countdown = timeBetweenWaves;           //start waiting for so many seconds until beging the next wave 
        Debug.Log(spawnPoint[0]);
        // Debug.Log(spawnPoint[spawnPoint.Length]);
    }

    void Update() {
        if (countdown <= 0f && nextWave < waves.Length){        //makes sure we are calling the new wave only when the earlier wave has finished and 
                                                                // makes sure we are not calling a wave that does not exist
            StartCoroutine(SpawnWave0(waves[nextWave]));        //start spawnign the first kinds of enemy of the wave
        }
        else if (countdown <= 0f && nextWave == waves.Length && enemies.childCount == 0){
            // all waves done - win!
            sceneName = SceneManager.GetActiveScene().name;
            GameObject.Find("Canvas"+sceneName).GetComponent<EndLevel>().win();
            enabled = false;
        }

        countdown -= Time.deltaTime;    //lowering the count every second 


    }

    public void Reset(){
        nextWave = 0;
        StopAllCoroutines();
    }

    IEnumerator SpawnWave0(Wave _wave){

        // wait until the entire wave has been dropped before starting the next one
        timeBetweenWaves = ((_wave.rate0*_wave.amount0) + (_wave.rate1*_wave.amount1) + (_wave.rate2*_wave.amount2));
        countdown = timeBetweenWaves;

        Debug.Log("SpawnWave0");

        //jude additions for cool and awesome polish
        //FindObjectOfType<SoundManager>().Play("wave_start"); NOW JUST A PLAY ON AWAKE

        StartCoroutine(WavePopups(_wave));

        //drop all of the first kind of enemies
        for (int i = 0; i < _wave.amount0; i++){
                SpawnEnemy(_wave.enemy0);
                yield return new WaitForSeconds(_wave.rate0);           //the rate at which they are dropping 
        }
        // yield return new WaitForSeconds(_wave.rate0*_wave.amount0);
        StartCoroutine(SpawnWave1(waves[nextWave]));                        //wait till all the first enemies drop till the second kind comes 
    }

    IEnumerator SpawnWave1(Wave _wave){

        //drop all of the second kind of enemies
        for (int i = 0; i < _wave.amount1; i++){
                SpawnEnemy(_wave.enemy1);
                yield return new WaitForSeconds(_wave.rate1);           //the rate at which they are dropping 
        }
        // yield return new WaitForSeconds(_wave.rate0*_wave.amount0);
        StartCoroutine(SpawnWave2(waves[nextWave]));                        //wait till all the secind enemies come until the third starts
    }

    IEnumerator SpawnWave2(Wave _wave){

        //now when we call a wave of enemies it is the next wave
        if(nextWave < waves.Length){
            nextWave++;
        }
        Debug.Log(nextWave);

        //drop all of the second kind of enemies
        for (int i = 0; i < _wave.amount2; i++){
                SpawnEnemy(_wave.enemy2); 
                yield return new WaitForSeconds(_wave.rate2);       //the rate at which they are dropping     
        }
    }

    IEnumerator WavePopups(Wave _wave)
    {
        GameObject newWavePopup = Instantiate(wavealert, new Vector3(0,0,0), Quaternion.identity);
        newWavePopup.transform.GetChild(0).transform.GetComponent<TMP_Text>().text = _wave.name;
        yield return new WaitForSeconds(2.5f);
        Destroy(newWavePopup);
    }
    void SpawnEnemy(Transform _enemy){
        // Debug.Log("Spawning Enemy: " + _enemy.name);
        int spawnindex = Random.Range(0, spawnPoint.Length);
        Transform currentspawn = spawnPoint[Random.Range(0,spawnPoint.Length)];         //can randomly come from a list of spawn points for some very simple variation
        Debug.Log(currentspawn);
        Debug.Log("SPAWN INDEX" + spawnindex);
        Instantiate(_enemy, spawnPoint[spawnindex].position, spawnPoint[spawnindex].rotation, enemies);   //create another prefab of the enemy you want 
        

    }


}
