using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AllowMerge : MonoBehaviour
{
    public GameObject slot0;        // the two merge slots 
    public GameObject slot1;
    public GameObject button;    // the button that merges


    [HideInInspector] public int count0;          // amount of children gameobjects of each slot
    [HideInInspector] public int count1;

    private Vector3 position = new Vector3();

    public Merge merge;

    private bool active = false;

    private bool isActive = false;


    void Update() {
        count0 = slot0.transform.childCount;     
        count1 = slot1.transform.childCount;


        //if each of the merge slots have an element in them then we can show the button to allow a merge

        if (count0 == 1 && count1 == 1){
            // gameObject.transform.position = new Vector3(-30, 180, 0);
            // int showButtonLayer = LayerMask.NameToLayer("Show Button");
            // gameObject.layer = showButtonLayer;
            if (TutorialManager.tutorialStage == 13)
            {
                FindObjectOfType<CrossTutorialManager>().crossFound();
            }

            gameObject.GetComponent<Image>().enabled = true; 

            // if (active == false){
            //     active = true;
            //     merge.Set();
                
            // }

            // if(isActive == false){
            //     isActive = true;
            //     merge.MergeSet();

            // }

            



        }
        else{
            // gameObject.transform.position = new Vector3(-10000, 10000, 0);
            // int hideButtonLayer = LayerMask.NameToLayer("Hide Button");
            // gameObject.layer = hideButtonLayer;
            gameObject.GetComponent<Image>().enabled = false;  
            
            // if (active == true){
            //     active = false;
            //     merge.UnSet();
                
            // }

            
            //     merge.MergeUnSet();

            

        }

    }

}
