using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;     //import to allow handlers
using UnityEngine.UI;

[RequireComponent(typeof(Button))]

public class InventorySlot : MonoBehaviour 
{
    public Merge merge;

    public MergePanel mergePanel;

    public PlantDict plantsDict;

    Dictionary<string, Plant> dict = new Dictionary<string, Plant>();

    public CrossingUIController crossing;

    public GameObject draggablePlantPrefab;

    private bool tutorial = false;
    private bool desert = false;
    private bool swamp = false;

    private Transform selectedItemContainer;     


    void Start()
    {
        plantsDict = GameObject.FindObjectOfType<PlantDict>().GetComponent<PlantDict>();
        selectedItemContainer = GameObject.Find("SelectedItem").transform;
        mergePanel = GameObject.FindObjectOfType<MergePanel>().GetComponent<MergePanel>();
        dict = plantsDict.SO_Plants;
    }

    void Update(){

        if (gameObject.name == "Slot0" && gameObject.transform.childCount == 0){
            crossing.UnSetVals();
        }

        if (gameObject.name == "Slot1" && gameObject.transform.childCount == 0){
            crossing.UnSetVals();
        }

        for (int i = 2; i < dict.Count+1; i++){
            if(gameObject.name == ("Slot" + i).ToString()){
                gameObject.GetComponent<UnityEngine.UI.Image>().enabled = true;
                gameObject.GetComponent<Button>().enabled = true;
            }
        }
    
        if((gameObject.name == ("Slot" + dict.Count).ToString()) 
                && gameObject.transform.childCount == 0 
                && selectedItemContainer.childCount == 0
                && GameObject.Find("Slot0").transform.childCount == 0
                && GameObject.Find("Slot1").transform.childCount == 0) {
            if(tutorial == false && dict.ContainsKey("cactus") && !mergePanel.cactusInPanel){
                tutorial = true;
                makePlant("cactus");
                mergePanel.addedCactus();
            }
            else if(desert == false && dict.ContainsKey("venus_fly_trap") && !mergePanel.venusInPanel){
                desert = true;
                makePlant("venus_fly_trap");
                mergePanel.addedVenus();
            }
            else if(swamp == false && dict.ContainsKey("pitcher") && !mergePanel.pitcherInPanel){
                swamp = true;
                makePlant("pitcher");
                mergePanel.addedPitcher();
            }
        }
    }

    private void makePlant(string key) {
        GameObject newPlant = Instantiate(draggablePlantPrefab); 
        newPlant.transform.SetParent(gameObject.transform);
        newPlant.name = key;
        newPlant.GetComponent<UnityEngine.UI.Image>().sprite = plantsDict.SO_Plants[key].menuImage;
        newPlant.GetComponent<UnityEngine.UI.Image>().rectTransform.localScale = new Vector3(1.4f,1.4f,0);
    }


    public void placeItem() {
        Debug.Log("attempting placing");
        int numSelectedItems = selectedItemContainer.transform.childCount;
        if(numSelectedItems > 0) {
            Transform item = selectedItemContainer.GetChild(0);
            item.SetParent(transform);
            item.localPosition = Vector3.zero;
            item.gameObject.GetComponent<DragableItem>().isPlaced();
            Debug.Log($"In {gameObject.name}, just added {item.gameObject.name} as the child.");
        }
    }

    public void resetSlot() {
        GetComponent<Button>().onClick.RemoveAllListeners();
    }

}
