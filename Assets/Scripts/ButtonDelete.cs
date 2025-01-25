using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class ButtonDelete : MonoBehaviour
{
    public GameObject plantInfo;

    // Start is called before the first frame update
    void Start()
    {
        try {
            plantInfo = GameObject.Find("NewPlantInfo(Clone)");
        } catch (NullReferenceException ex) {
            Debug.Log("ERROR: "+ex);
            Debug.Log("Could not find the NewPlantInfo screen in the scene. Make sure its name matches NewPlantInfo(Clone).");
        }
    }

    public void DestroyObjs() {
        Destroy(plantInfo);
        Destroy(gameObject);
    }
}
