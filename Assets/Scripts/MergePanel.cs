using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MergePanel : MonoBehaviour
{
    // first, move PlantDictObj to this scene/keep it enabled
    // find PlantDictObj and load all current plants into the slots

    //public PlantDict plantDict;
    public bool cactusInPanel;
    public bool venusInPanel;
    public bool pitcherInPanel;

    void Start()
    {
        //plantDict = GameObject.Find("PlantDictObj").GetComponent<PlantDict>();
        //Debug.Log(plantDict.Plants[3]);
        cactusInPanel = false;
        venusInPanel = false;
        pitcherInPanel = false;
    }

    public void addedCactus(){
        cactusInPanel = true;
    }

    public void addedVenus(){
        venusInPanel = true;
    }

    public void addedPitcher(){
        pitcherInPanel = true;
    }
}
